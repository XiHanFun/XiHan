#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PresentationWebCoreModule
// Guid:a7e53779-aa84-40ef-ab18-b00f7c4f2535
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:05:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Domain.Core.Modules.Abstracts;

namespace XiHan.Presentation.Web.Core.Modules;

/// <summary>
/// 配置网站核心模块
/// </summary>
public class PresentationWebCoreModule : IModule
{
    /// <summary>
    /// 应用配置
    /// </summary>
    /// <param name="app"></param>
    public void Configure(IApplicationBuilder app)
    {
    }

    ///<summary>
    /// 服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }
}