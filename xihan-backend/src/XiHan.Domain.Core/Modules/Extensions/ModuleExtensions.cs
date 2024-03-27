#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ModuleExtensions
// Guid:0a45e099-01b7-445e-897a-adad3ce72590
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 1:49:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Domain.Core.Modules.Abstracts;

namespace XiHan.Domain.Core.Modules.Extensions;

/// <summary>
/// 模块配置扩展
/// </summary>
public static class ModuleExtensions
{
    private static IList<IModule>? _modules;

    /// <summary>
    /// 服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="modules"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfigureServicesByModules(this IServiceCollection services, IConfiguration configuration, params IModule[] modules)
    {
        _modules = modules;
        if (_modules == null)
            return services;

        foreach (var module in _modules)
        {
            module.ConfigureServices(services, configuration);
        }
        return services;
    }

    /// <summary>
    /// 应用配置
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseConfigureByModules(this IApplicationBuilder app)
    {
        if (_modules == null)
            return app;

        foreach (var module in _modules)
        {
            module.Configure(app);
        }
        return app;
    }
}