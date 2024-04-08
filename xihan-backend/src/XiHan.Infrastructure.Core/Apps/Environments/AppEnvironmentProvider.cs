#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppEnvironmentProvider
// Guid:7b64d18c-9312-4b31-b59d-68d5e41a5583
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:31:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Hosting;

namespace XiHan.Infrastructure.Core.Apps.Environments;

/// <summary>
/// 全局宿主环境供应器
/// </summary>
public static class AppEnvironmentProvider
{
    /// <summary>
    /// 全局宿主环境
    /// </summary>
    public static IWebHostEnvironment WebHostEnvironment { get; set; } = null!;
}