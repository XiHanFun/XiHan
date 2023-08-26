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
using XiHan.Infrastructures.Apps.Services;
using XiHan.Utils.Extensions;
using XiHan.WebCore.Setups.Services;

namespace XiHan.WebCore.Setups;

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

        // 服务注册
        AppServiceProvider.RegisterService(services);

        // 缓存
        services.AddCacheSetup();
        // 响应缓存
        services.AddResponseCacheSetup();
        // Mapster
        services.AddMapsterSetup();
        // Swagger
        services.AddSwaggerSetup();
        // 性能分析
        services.AddMiniProfilerSetup();
        // SqlSugar，必须在 AddMiniProfilerSetup 后才能使用
        services.AddSqlSugarSetup();
        // Http上下文
        services.AddHttpPollySetup();
        // Auth，必须在 AddHttpPollySetup 后才能使用
        services.AddAuthSetup();
        // RabbitMQ
        services.AddRabbitMqSetup();
        // 限流
        services.AddRateLimiterSetup();
        // Cors
        services.AddCorsSetup();
        // Controllers
        services.AddControllersSetup();
        // Route
        services.AddRouteSetup();
        // 终端
        services.AddEndpointsApiExplorer();
        // 即时通讯
        services.AddSignalRSetup();
        // 健康检查
        services.AddHealthChecks();
        // 计划任务
        services.AddJobSetup();

        "Services Started Successfully！".WriteLineSuccess();
        return services;
    }
}