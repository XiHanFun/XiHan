#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IIntegrationEventHandler
// Guid:2f833685-a239-4f41-b716-ea7db04a47e7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 3:57:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Core.EventBus.Bases.Models;

namespace XiHan.Infrastructure.Core.EventBus.Bases.Abstracts;

/// <summary>
/// 集成事件处理器公共接口，所有的事件处理都要实现该接口
/// </summary>
public interface IIntegrationEventHandler
{
}

/// <summary>
/// 集成事件处理器接口，用于处理特定类型的事件
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    /// <summary>
    /// 事件处理
    /// </summary>
    /// <param name="event">集成事件</param>
    /// <returns></returns>
    Task Handle(TIntegrationEvent @event);
}

/// <summary>
/// 集成事件处理器接口，用于处理动态类型的事件
/// </summary>
public interface IDynamicIntegrationEventHandler
{
    /// <summary>
    /// 动态事件处理
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    Task Handle(dynamic eventData);
}