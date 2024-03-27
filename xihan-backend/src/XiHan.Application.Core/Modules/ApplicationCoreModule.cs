#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationCoreModule
// Guid:75bf7ed1-ea64-4e94-918e-a9c667b1a6f0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:10:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Domain.Core.Modules.Abstracts;

namespace XiHan.Application.Core.Modules;

/// <summary>
/// 配置应用核心模块
/// </summary>
public class ApplicationCoreModule : IModule
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