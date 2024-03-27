#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InfrastructureCoreModule
// Guid:5df73c65-b41c-4440-8d23-97d2009a33d8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:21:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Domain.Core.Modules.Abstracts;

namespace XiHan.Infrastructure.Core.Modules;

/// <summary>
/// 基础设施核心模块
/// </summary>
public class InfrastructureCoreModule : IModule
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