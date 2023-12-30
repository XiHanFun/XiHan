#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IEventBus
// Guid:54a150ab-1482-4b91-9681-39da6def8e82
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 3:47:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.EventBus.Bases.Handlers;
using XiHan.EventBus.Bases.Models;

namespace XiHan.EventBus.Bases;

/// <summary>
/// 事件总线接口
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// 发布
    /// </summary>
    /// <param name="event">事件模型</param>
    void Publish(IntegrationEventModel @event);

    /// <summary>
    /// 订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    void Subscribe<T, TH>() where T : IntegrationEventModel where TH : IIntegrationEventHandler<T>;

    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <typeparam name="T">事件模型</typeparam>
    /// <typeparam name="TH">事件处理器(事件模型)</typeparam>
    void UnSubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEventModel;

    /// <summary>
    /// 动态订阅
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName"></param>
    void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

    /// <summary>
    /// 取消动态订阅
    /// </summary>
    /// <typeparam name="TH">事件处理器</typeparam>
    /// <param name="eventName"></param>
    void UnSubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;
}