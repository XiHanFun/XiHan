#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:EventBusKafka
// Guid:8cf49d6e-d5af-4e7f-845f-cb40fe002068
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 3:10:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XiHan.Common.Utilities.Extensions;
using XiHan.Common.Utilities.Serializes;
using XiHan.Infrastructure.EventBus.Bases;
using XiHan.Infrastructure.EventBus.Bases.Models;

namespace XiHan.Infrastructure.EventBus.Kafka;

/// <summary>
/// 基于 Kafka 的事件总线
/// </summary>
public class EventBusKafka : IEventBus
{
    private readonly ILogger<EventBusKafka> _logger;
    private readonly IEventBusSubscriptionManager _subscriptionManager;
    private readonly IKafkaConnectionPool _connectionPool;
    private readonly KafkaOptions _options;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="subscriptionManager"></param>
    /// <param name="connectionPool"></param>
    /// <param name="options"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public EventBusKafka(ILogger<EventBusKafka> logger,
        IEventBusSubscriptionManager subscriptionManager,
        IKafkaConnectionPool connectionPool,
        IOptions<KafkaOptions> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _subscriptionManager = subscriptionManager ?? throw new ArgumentNullException(nameof(subscriptionManager));
        _connectionPool = connectionPool ?? throw new ArgumentNullException(nameof(connectionPool));
        _options = options.Value;
    }

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件模型</param>
    public void Publish(IntegrationEvent @event)
    {
        var producer = _connectionPool.Producer();
        try
        {
            var eventName = @event.GetType().Name;
            var body = SerializeHelper.SerializeTo(@event).ToBinary();
            DeliveryResult<string, byte[]> result = producer.ProduceAsync(_options.Topic, new Message<string, byte[]>
            {
                Key = eventName,
                Value = body
            }).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Could not publish event: {@event.Id:N} ({ex.Message});Message:{SerializeHelper.SerializeTo(@event)}");
        }
        finally
        {
            // 放入连接池中
            _connectionPool.Return(producer);
        }
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

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        _subscriptionManager.AddSubscription<T, TH>();
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

        _subscriptionManager.AddDynamicSubscription<TH>(eventName);
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
        _subscriptionManager.RemoveDynamicSubscription<TH>(eventName);
    }
}