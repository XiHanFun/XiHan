#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppEnvironmentProvider
// Guid:0c12544c-db5e-4ae4-adf6-4dfe62bda0ec
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/4 2:15:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Hosting;

namespace XiHan.Infrastructure.Bases.Apps.Environments;

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