#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IModule
// Guid:fcd576c5-238c-4d6f-899b-dcff94141e49
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 1:23:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Domain.Core.Modules.Abstracts;

/// <summary>
/// 定义模块配置和服务配置接口
/// </summary>
public interface IModule
{
    /// <summary>
    /// 服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    /// 应用配置
    /// </summary>
    /// <param name="app"></param>
    void Configure(IApplicationBuilder app);
}