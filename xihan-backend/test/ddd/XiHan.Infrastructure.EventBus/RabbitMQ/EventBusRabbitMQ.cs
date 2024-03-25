#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:EventBusRabbitMQ
// Guid:86e12961-6e97-4c13-a689-23643a94ed0c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 3:11:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;
using XiHan.Common.Utilities.Extensions;
using XiHan.Common.Utilities.Serializes;
using XiHan.Infrastructure.EventBus.Bases;
using XiHan.Infrastructure.EventBus.Bases.Models;
using XiHan.Infrastructure.EventBus.RabbitMQ.Consts;

namespace XiHan.Infrastructure.EventBus.RabbitMQ;

/// <summary>
/// 基于 RabbitMQ 的事件总线
/// </summary>
public class EventBusRabbitMQ : IEventBus
{
    private readonly ILogger<EventBusRabbitMQ> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventBusSubscriptionManager _subscriptionManager;
    private readonly IRabbitMQConnection _persistentConnection;
    private readonly string _exchangeName;
    private readonly int _retryCount;

    private string _queueName;
    private IModel _consumerChannel;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="subscriptionManager"></param>
    /// <param name="persistentConnection"></param>
    /// <param name="exchangeName"></param>
    /// <param name="retryCount"></param>
    /// <param name="queueName"></param>
    public EventBusRabbitMQ(ILogger<EventBusRabbitMQ> logger,
        IServiceProvider serviceProvider,
        IEventBusSubscriptionManager subscriptionManager,
        IRabbitMQConnection persistentConnection,
        string? exchangeName,
        int? retryCount,
        string? queueName)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _subscriptionManager = subscriptionManager ?? new InMemoryEventBusSubscriptionsManager();
        _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _exchangeName = exchangeName ??= RabbitMQConstants.DefaultExchangeName;
        _retryCount = retryCount ?? RabbitMQConstants.DefaultRetryCount;
        _queueName = queueName ?? RabbitMQConstants.DefaultQueueName;

        _consumerChannel = CreateConsumerChannel();

        _subscriptionManager.OnEventRemoved += SubsManagerOnEventRemoved;
    }

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件模型</param>
    public void Publish(IntegrationEvent @event)
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        var policy = Policy.Handle<BrokerUnreachableException>().Or<SocketException>()
               .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
               {
                   _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
               });

        var eventName = @event.GetType().Name;

        _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventName}", eventName);

        using var channel = _persistentConnection.CreateModel();

        _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventName}", eventName);

        channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

        var body = SerializeHelper.SerializeTo(@event).ToBinary();

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

            channel.BasicPublish(exchange: _exchangeName, routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
        });
    }

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subscriptionManager.GetEventKey<T>();
        DoInternalSubscription(eventName);

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        _subscriptionManager.AddSubscription<T, TH>();

        StartBasicConsume();
    }

    /// <summary>
    /// 订阅动态事件
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName"></param>
    public void SubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler
    {
        _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        DoInternalSubscription(eventName);

        _subscriptionManager.AddDynamicSubscription<TH>(eventName);

        StartBasicConsume();
    }

    /// <summary>
    /// 取消订阅事件
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    public void UnSubscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = _subscriptionManager.GetEventKey<T>();

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _subscriptionManager.RemoveSubscription<T, TH>();
    }

    /// <summary>
    /// 取消订阅动态事件
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName"></param>
    public void UnSubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler
    {
        _logger.LogInformation("Unsubscribing from dynamic event {EventName}", eventName);

        _subscriptionManager.RemoveDynamicSubscription<TH>(eventName);
    }

    #region 内部方法

    /// <summary>
    /// 创造消费通道
    /// </summary>
    /// <returns></returns>
    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _persistentConnection.CreateModel();
        channel.ExchangeDeclare(exchange: RabbitMQConstants.DefaultExchangeName, type: ExchangeType.Direct);
        channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        channel.CallbackException += (sender, ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel.Dispose();
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };

        return channel;
    }

    /// <summary>
    /// 订阅管理器事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventName"></param>
    private void SubsManagerOnEventRemoved(object sender, string eventName)
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        using var channel = _persistentConnection.CreateModel();
        channel.QueueUnbind(queue: _queueName, exchange: RabbitMQConstants.DefaultExchangeName, routingKey: eventName);

        if (_subscriptionManager.IsEmpty)
        {
            _queueName = string.Empty;
            _consumerChannel.Close();
        }
    }

    /// <summary>
    /// 开始基本消费
    /// </summary>
    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel != null)
        {
            var consumer = new EventingBasicConsumer(_consumerChannel);

            consumer.Received += async (sender, eventArgs) => await ConsumerReceived(sender, eventArgs);

            _consumerChannel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }
        else
        {
            _logger.LogError("StartBasicConsume: _consumerChannel is null");
        }
    }

    /// <summary>
    /// 消费者接受到
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    private async Task ConsumerReceived(object? sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        try
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                _logger.LogWarning("Received empty event name");
                return;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                _logger.LogWarning("Received empty message");
                return;
            }

            if (message.Contains("Diagnostic"))
            {
                _logger.LogTrace("----- Received message \"{Message}\"", message);
            }

            if (message.Contains("throw-fake-exception", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
            }

            await ProcessEvent(eventName, message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
        }

        _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }

    /// <summary>
    /// 接收到消息进行处理
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="message">消息内容</param>
    /// <returns></returns>
    private async Task ProcessEvent(string eventName, string message)
    {
        _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        if (_subscriptionManager.HasSubscriptionsForEvent(eventName))
        {
            using var scope = _serviceProvider.CreateScope();
            var subscriptions = _subscriptionManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                if (subscription.IsDynamic)
                {
                    if (scope.ServiceProvider.GetService(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler)
                        continue;

                    dynamic eventData = SerializeHelper.DeserializeTo<dynamic>(message)!;

                    await Task.Yield();
                    await handler.Handle(eventData);
                }
                else
                {
                    var handler = scope.ServiceProvider.GetService(subscription.HandlerType);
                    if (handler == null)
                        continue;

                    var eventType = _subscriptionManager.GetEventTypeByName(eventName);
                    var integrationEvent = SerializeHelper.DeserializeTo(message)!;
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await (Task)concreteType.GetMethod("Handle")!.Invoke(handler, new object[] { integrationEvent })!;
                }
            }
        }
        else
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }

    /// <summary>
    /// 进行内部订阅
    /// </summary>
    /// <param name="eventName"></param>
    private void DoInternalSubscription(string eventName)
    {
        var containsKey = _subscriptionManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using var channel = _persistentConnection.CreateModel();
            channel.QueueBind(queue: _queueName, exchange: RabbitMQConstants.DefaultExchangeName, routingKey: eventName);
        }
    }

    #endregion
}