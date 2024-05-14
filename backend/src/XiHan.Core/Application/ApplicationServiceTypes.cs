#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationServiceTypes
// Guid:32fff088-6c3b-43d4-9d3a-b66e915e1935
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-26 上午 11:17:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Application;

/// <summary>
/// 应用服务类型
/// </summary>
/// <remarks><see cref="FlagsAttribute"/> 是为了方便使用位运算</remarks>
[Flags]
public enum ApplicationServiceTypes : byte
{
    /// <summary>
    /// 仅应用服务，不包含集成服务<see cref="IntegrationServiceAttribute"/>
    /// </summary>
    ApplicationServices = 1,

    /// <summary>
    /// 集成服务<see cref="IntegrationServiceAttribute"/>
    /// </summary>
    IntegrationServices = 2,

    /// <summary>
    /// 所有服务
    /// </summary>
    All = ApplicationServices | IntegrationServices
}