#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceSetup
// Guid:2340e05b-ffd7-4a19-84bc-c3f73517b696
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-30 上午 02:48:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Application.Setups.Services;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Utils.Extensions;

namespace XiHan.Application.Setups;

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
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddServiceSetup(this IServiceCollection services)
    {
        "Services Start……".WriteLineInfo();
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 内存缓存
        services.AddMemoryCacheSetup();
        // 分布式缓存
        services.AddRedisCacheSetup();
        // 响应缓存
        services.AddResponseCacheSetup();
        // SqlSugar
        services.AddSqlSugarSetup();
        // Mapster
        services.AddMapsterSetup();
        // Http上下文
        services.AddHttpPollySetup();
        // Auth，必须在 AddHttpPollySetup 后才能使用
        services.AddAuthSetup();
        // Swagger
        services.AddSwaggerSetup();
        // RabbitMQ
        services.AddRabbitMqSetup();
        // 限流
        services.AddRateLimiterSetup();
        // 性能分析
        services.AddMiniProfilerSetup();
        // 健康检查
        services.AddHealthChecks();
        // 即时通讯
        services.AddSignalRSetup();
        // 计划任务
        services.AddJobs();

        // 服务注册
        AppServiceManager.RegisterService(services);

        // Route
        services.AddRouteSetup();
        // Cors
        services.AddCorsSetup();
        // Controllers
        services.AddControllersSetup();
        // 用于最小API
        services.AddEndpointsApiExplorer();

        "Services Started Successfully！".WriteLineSuccess();
        return services;
    }
}