#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InMemoryEventBusSubscriptionsManager
// Guid:7935df35-f72f-4811-a06c-8f8591dc4b77
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 9:51:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Core.EventBus.Bases.Models;

namespace XiHan.Infrastructure.Core.EventBus.Bases.Abstracts;

/// <summary>
/// 基于内存的事件总线订阅管理器
/// </summary>
public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionManager
{
    /// <summary>
    /// 定义事件的名称和时间订阅的字典映射(1:N)
    /// </summary>
    private readonly Dictionary<string, List<Subscription>> _handlers;

    /// <summary>
    /// 保存所有事件的处理类型
    /// </summary>
    private readonly List<Type> _eventTypes;

    /// <summary>
    /// 事件移除时触发的事件
    /// </summary>
    public event EventHandler<string>? OnEventRemoved;

    /// <summary>
    /// 构造函数
    /// </summary>
    public InMemoryEventBusSubscriptionsManager()
    {
        _handlers = [];
        _eventTypes = [];
    }

    #region 公共方法

    /// <summary>
    /// 事件是否为空
    /// </summary>
    public bool IsEmpty => _handlers is { Count: 0 };

    /// <summary>
    /// 添加订阅
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName">事件名称</param>
    public void AddDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler
    {
        DoAddSubscription(typeof(TH), eventName, isDynamic: true);
    }

    /// <summary>
    /// 添加订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    public void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();

        DoAddSubscription(typeof(TH), eventName, isDynamic: false);

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear() => _handlers.Clear();

    /// <summary>
    /// 获取事件名称
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <returns></returns>
    public string GetEventKey<T>() => typeof(T).Name;

    /// <summary>
    /// 获取事件类型
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns></returns>
    public Type GetEventTypeByName(string eventName) => _eventTypes.Single(t => t.Name == eventName);

    /// <summary>
    /// 获取事件处理器
    /// </summary>
    /// <typeparam name="T">订阅模型</typeparam>
    /// <returns></returns>
    public IEnumerable<Subscription> GetHandlersForEvent<T>()
        where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return GetHandlersForEvent(key);
    }

    /// <summary>
    /// 获取事件处理器
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns></returns>
    public IEnumerable<Subscription> GetHandlersForEvent(string eventName) => _handlers[eventName];

    /// <summary>
    /// 是否存在订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <returns></returns>
    public bool HasSubscriptionsForEvent<T>()
        where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return HasSubscriptionsForEvent(key);
    }

    /// <summary>
    /// 是否存在订阅
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns></returns>
    public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

    /// <summary>
    /// 移除订阅
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName">事件名称</param>
    public void RemoveDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler
    {
        var handlerToRemove = FindDynamicSubscriptionToRemove<TH>(eventName);
        DoRemoveHandler(eventName, handlerToRemove);
    }

    /// <summary>
    /// 移除订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    public void RemoveSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var handlerToRemove = FindSubscriptionToRemove<T, TH>();
        var eventName = GetEventKey<T>();
        DoRemoveHandler(eventName, handlerToRemove);
    }

    #endregion

    #region 内部方法

    /// <summary>
    /// 添加订阅
    /// </summary>
    /// <param name="handlerType">处理器类型</param>
    /// <param name="eventName">事件名称</param>
    /// <param name="isDynamic">是否为动态</param>
    /// <exception cref="ArgumentException"></exception>
    private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            _handlers.Add(eventName, []);
        }

        if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
        {
            throw new ArgumentException($"处理程序类型{handlerType.Name}已为'{eventName}'注册！", nameof(handlerType));
        }

        if (isDynamic)
        {
            _handlers[eventName].Add(Subscription.SubscriptDynamic(handlerType));
        }
        else
        {
            _handlers[eventName].Add(Subscription.Subscript(handlerType));
        }
    }

    /// <summary>
    /// 查询订阅并移除
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName">事件名称</param>
    /// <returns></returns>
    private Subscription? FindDynamicSubscriptionToRemove<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler => DoFindSubscriptionToRemove(eventName, typeof(TH));

    /// <summary>
    /// 查询订阅并移除
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    /// <returns></returns>
    private Subscription? FindSubscriptionToRemove<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();
        return DoFindSubscriptionToRemove(eventName, typeof(TH));
    }

    private Subscription? DoFindSubscriptionToRemove(string eventName, Type handlerType)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            return null;
        }

        return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
    }

    /// <summary>
    /// 移除处理器
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="subsToRemove">订阅消息</param>
    private void DoRemoveHandler(string eventName, Subscription? subsToRemove)
    {
        if (subsToRemove != null)
        {
            _handlers[eventName].Remove(subsToRemove);
            if (_handlers[eventName].Count == 0)
            {
                _handlers.Remove(eventName);
                var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                if (eventType != null)
                {
                    _eventTypes.Remove(eventType);
                }
                RaiseOnEventRemoved(eventName);
            }
        }
    }

    /// <summary>
    /// 事件移除时触发
    /// </summary>
    /// <param name="eventName"></param>
    private void RaiseOnEventRemoved(string eventName)
    {
        var handler = OnEventRemoved;
        handler?.Invoke(this, eventName);
    }

    #endregion
}