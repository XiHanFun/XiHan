#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppServiceProvider
// Guid:4bd4bc2a-1c4d-4520-b62a-f8672f5a606a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:33:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.Core.Apps.Services;

/// <summary>
/// 全局服务供应器
/// </summary>
public static class AppServiceProvider
{
    /// <summary>
    /// 全局应用服务容器
    /// </summary>
    public static IServiceProvider ServiceProvider { get; set; } = null!;
}