#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DomainCoreModuleInitializer
// Guid:a982f4a9-d098-431b-a9a7-83e324ac822d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:19:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Common.Core.Modules.Abstracts;

namespace XiHan.Domain.Core;

/// <summary>
/// 领域核心模块初始化
/// </summary>
public class DomainCoreModuleInitializer : IModuleInitializer
{
    /// <summary>
    /// 初始化服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// 配置应用程序
    /// </summary>
    public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
    }
}