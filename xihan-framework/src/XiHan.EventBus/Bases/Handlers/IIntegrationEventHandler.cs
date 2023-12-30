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

using XiHan.EventBus.Bases.Models;

namespace XiHan.EventBus.Bases.Handlers;

/// <summary>
/// 集成事件处理器基接口
/// </summary>
public interface IIntegrationEventHandler
{
}

/// <summary>
/// 集成事件处理器泛型接口
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler where TIntegrationEvent : IntegrationEventModel
{
    /// <summary>
    /// 事件处理
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    Task Handle(TIntegrationEvent @event);
}