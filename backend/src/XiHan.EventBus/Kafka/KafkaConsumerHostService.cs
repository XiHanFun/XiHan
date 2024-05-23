#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:KafkaConsumerHostService
// Guid:79211b43-e6ff-4dbf-8324-2ce220ae42c1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/7 1:48:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XiHan.Infrastructure.Core.EventBus.Bases.Abstracts;

namespace XiHan.EventBus.Kafka;

/// <summary>
/// Kafka 消费者监听服务
/// </summary>
public class KafkaConsumerHostService : BackgroundService
{
    private readonly ILogger<KafkaConsumerHostService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventBusSubscriptionManager _subscriptionManager;
    private readonly KafkaOptions _options;

    private readonly IConsumer<string, byte[]> _consumer;
    private CancellationTokenSource _tokenSource = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="subscriptionManager"></param>
    /// <param name="options"></param>
    public KafkaConsumerHostService(ILogger<KafkaConsumerHostService> logger,
        IServiceProvider serviceProvider,
        IEventBusSubscriptionManager subscriptionManager,
        IOptions<KafkaOptions> options)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _subscriptionManager = subscriptionManager;
        _options = options.Value;
        _consumer = new ConsumerBuilder<string, byte[]>(new ConsumerConfig
        {
            BootstrapServers = _options.Servers,
            GroupId = _options.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true,
            EnableAutoCommit = false,
            LogConnectionClose = false
        }).SetErrorHandler(ConsumerClientError).Build();
    }

    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var result = await FetchTopicAsync();
        if (result)
        {
            _consumer.Subscribe(_options.Topic);
            while (!_tokenSource.Token.IsCancellationRequested)
            {
                var consumerResult = _consumer.Consume(_tokenSource.Token);
                try
                {
                    if (consumerResult.IsPartitionEOF || consumerResult.Message.Value == null) continue;

                    var @event = consumerResult.Message.Value.BinaryDecode();
                    await ProcessEvent(consumerResult.Message.Key, @event);
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"错误发生:{e.Error.Reason}");
                }
                finally
                {
                    _consumer.Commit(consumerResult);
                }
            }
        }
    }

    /// <summary>
    /// 异步停止
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _tokenSource.Cancel();
        _logger.LogInformation("Kafka消费停止和一次性");
        _consumer.Dispose();
        return base.StopAsync(cancellationToken);
    }

    #region 内部方法

    /// <summary>
    /// 检测当前Topic是否存在
    /// </summary>
    /// <returns></returns>
    private async Task<bool> FetchTopicAsync()
    {
        if (string.IsNullOrEmpty(_options.Topic))
            throw new ArgumentNullException(nameof(_options.Topic));

        try
        {
            var config = new AdminClientConfig { BootstrapServers = _options.Servers };
            using var adminClient = new AdminClientBuilder(config).Build();
            await adminClient.CreateTopicsAsync(Enumerable.Range(0, 1).Select(u => new TopicSpecification
            {
                Name = _options.Topic,
                NumPartitions = _options.NumPartitions
            }));
        }
        catch (CreateTopicsException ex) when (ex.Message.Contains("already exists"))
        {
        }
        catch (Exception ex)
        {
            _logger.LogError("自动创建主题时遇到错误！" + ex.Message);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 接收到消息进行处理
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="message">消息内容</param>
    /// <returns></returns>
    private async Task ProcessEvent(string eventName, string message)
    {
        _logger.LogTrace($"处理Kafka事件:{eventName}");

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
                    await (Task)concreteType.GetMethod("Handle")!.Invoke(handler, [integrationEvent])!;
                }
            }
        }
        else
        {
            _logger.LogWarning($"没有订阅{eventName}Kafka事件！");
        }
    }

    /// <summary>
    /// 消费者客户端出错
    /// </summary>
    /// <param name="consumer"></param>
    /// <param name="e"></param>
    private void ConsumerClientError(IConsumer<string, byte[]> consumer, Error e)
    {
        _logger.LogError($"连接kafka时出现错误:{e.Reason}");
    }

    #endregion
}