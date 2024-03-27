#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DomainCoreModule
// Guid:a982f4a9-d098-431b-a9a7-83e324ac822d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:19:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Domain.Core.Modules.Abstracts;

namespace XiHan.Domain.Core.Modules;

/// <summary>
/// 领域核心模块
/// </summary>
public class DomainCoreModule : IModule
{
    /// <summary>
    /// 应用配置
    /// </summary>
    /// <param name="app"></param>
    public void Configure(IApplicationBuilder app)
    {
    }

    /// <summary>
    /// 服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }
}