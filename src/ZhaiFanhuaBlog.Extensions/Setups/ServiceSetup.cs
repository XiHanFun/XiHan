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
using ZhaiFanhuaBlog.Infrastructure.Apps.Services;
using ZhaiFanhuaBlog.Utils.Console;

namespace ZhaiFanhuaBlog.Extensions.Setups;

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
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 注册服务
        "Services Start……".WriteLineWarning();
        // Cache
        services.AddCacheSetup();
        // Auth
        services.AddAuthJwtSetup();
        // 健康检查
        services.AddHealthChecks();
        // Http
        services.AddHttpSetup();
        // Swagger
        services.AddSwaggerSetup();
        // 性能分析
        services.AddMiniProfilerSetup();
        // SqlSugar
        services.AddSqlSugarSetup();
        // 服务注入
        AppServiceManager.RegisterBaseService(services);
        AppServiceManager.RegisterSelfService(services);
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