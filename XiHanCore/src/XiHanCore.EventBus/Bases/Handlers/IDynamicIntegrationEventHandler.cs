#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IDynamicIntegrationEventHandler
// Guid:96912520-7dee-49eb-b517-e8e742d31f12
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/12/31 4:38:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHanCore.EventBus.Bases.Handlers;

/// <summary>
/// 集成动态事件处理器接口
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