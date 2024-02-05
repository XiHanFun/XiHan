#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IModule
// Guid:e65b07f4-d407-4ae8-b31f-f3aaf91a6f3e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/5 13:25:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Domain.Bases.SeedWorks;

/// <summary>
/// IModule
/// </summary>
public interface IModule
{
    /// <summary>
    /// 应用配置
    /// </summary>
    /// <param name="app"></param>
    void Configure(IApplicationBuilder app);

    /// <summary>
    /// 服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}