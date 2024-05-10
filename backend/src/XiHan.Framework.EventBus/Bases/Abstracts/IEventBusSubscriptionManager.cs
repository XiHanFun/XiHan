#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IEventBusSubscriptionManager
// Guid:35ab7506-8eb2-4717-bb95-53381de47626
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 4:10:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Core.EventBus.Bases.Models;

namespace XiHan.Infrastructure.Core.EventBus.Bases.Abstracts;

/// <summary>
/// 事件总线订阅管理器接口
/// </summary>
/// <remarks>
/// 提供事件处理的注册和分发，从而使得相同领域事件可被微服务内多个事件处理器接收
/// </remarks>
public interface IEventBusSubscriptionManager
{
    /// <summary>
    /// 事件移除时触发的事件
    /// </summary>
    event EventHandler<string>? OnEventRemoved;

    /// <summary>
    /// 事件是否为空
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// 添加订阅
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName">事件名称</param>
    void AddDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    /// <summary>
    /// 添加订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    /// <summary>
    /// 清空
    /// </summary>
    void Clear();

    /// <summary>
    /// 获取事件名称
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <returns></returns>
    string GetEventKey<T>();

    /// <summary>
    /// 获取事件类型
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns></returns>
    Type GetEventTypeByName(string eventName);

    /// <summary>
    /// 获取事件处理器
    /// </summary>
    /// <typeparam name="T">订阅模型</typeparam>
    /// <returns></returns>
    IEnumerable<Subscription> GetHandlersForEvent<T>()
        where T : IntegrationEvent;

    /// <summary>
    /// 获取事件处理器
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns></returns>
    IEnumerable<Subscription> GetHandlersForEvent(string eventName);

    /// <summary>
    /// 是否存在订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <returns></returns>
    bool HasSubscriptionsForEvent<T>()
        where T : IntegrationEvent;

    /// <summary>
    /// 是否存在订阅
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns></returns>
    bool HasSubscriptionsForEvent(string eventName);

    /// <summary>
    /// 移除订阅
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName">事件名称</param>
    void RemoveDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    /// <summary>
    /// 移除订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    void RemoveSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
}