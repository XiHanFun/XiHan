#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InfrastructureCoreModuleInitializer
// Guid:5df73c65-b41c-4440-8d23-97d2009a33d8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:21:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Common.Core.Modularity.Abstracts;
using XiHan.Infrastructure.Core.Modules;

namespace XiHan.Infrastructure.Core;

/// <summary>
/// 基础设施核心模块初始化
/// </summary>
public class InfrastructureCoreModuleInitializer : IModuleInitializer
{
    /// <summary>
    /// 初始化服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void InitializeServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilogModule();
        services.AddIpSearchModule();
        services.AddHttpPollyModule();
        services.AddAppServiceModule();
        services.AddCorsModule();
        services.AddSnowflakeIdModule();
        services.AddRateLimiterModule();
        services.AddCacheModule();
    }

    /// <summary>
    /// 配置应用程序
    /// </summary>
    public void Initialize(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
    }
}