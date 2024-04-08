#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CommonSharedModuleInitializer
// Guid:73242e9e-1500-4f28-9077-2ba02206ac9f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/30 3:40:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Common.Core.Modules.Abstracts;
using XiHan.Common.Shared.Services;

namespace XiHan.Common.Utilities;

/// <summary>
/// 公共共享模块初始化
/// </summary>
public class CommonSharedModuleInitializer : IModuleInitializer
{
    /// <summary>
    /// 初始化服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // 属性或字段自动注入服务
        services.AddSingleton<AutowiredServiceHandler>();
    }

    /// <summary>
    /// 配置应用程序
    /// </summary>
    public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
    }
}