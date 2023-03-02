#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ServiceSetup
// Guid:2340e05b-ffd7-4a19-84bc-c3f73517b696
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:48:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Extensions.Setups.Services;
using XiHan.Infrastructure.Apps.Services;
using XiHan.Utils.Console;

namespace XiHan.Extensions.Setups;

/// <summary>
/// ServiceSetup
/// </summary>
public static class ServiceSetup
{
    /// <summary>
    /// 依赖注入服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServiceSetup(this IServiceCollection services)
    {
        "Services Start……".WriteLineWarning();
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Cache
        services.AddCacheSetup();
        // Auth
        services.AddAuthJwtSetup();
        // 健康检查
        services.AddHealthChecks();
        // 即时通讯
        services.AddSignalR();
        // Http
        services.AddHttpSetup();
        // Swagger
        services.AddSwaggerSetup();
        // 性能分析
        services.AddMiniProfilerSetup();
        // SqlSugar
        services.AddSqlSugarSetup();
        // 服务注册
        AppServiceManager.RegisterService(services);
        // AutoMapper
        services.AddAutoMapperSetup();
        // Route
        services.AddRouteSetup();
        // Cors
        services.AddCorsSetup();
        // Controllers
        services.AddControllersSetup();

        "Services Started Successfully！".WriteLineSuccess();
        return services;
    }
}