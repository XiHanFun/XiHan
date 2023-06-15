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
using XiHan.Infrastructures.Apps.Services;
using XiHan.Utils.Extensions;
using XiHan.Application.Setups.Services;

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
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Cache
        services.AddCacheSetup();
        // SqlSugar
        services.AddSqlSugarSetup();
        // Mapster
        services.AddMapsterSetup();
        // Auth
        services.AddAuthSetup();
        // Http上下文
        services.AddHttpPollySetup();
        // Swagger
        services.AddSwaggerSetup();
        // RabbitMQ
        services.AddRabbitMqSetup();
        // 性能分析
        services.AddMiniProfilerSetup();
        // 健康检查
        services.AddHealthChecks();
        // 即时通讯
        services.AddSignalRSetup();
        // 服务注册
        AppServiceManager.RegisterService(services);
        // 计划任务
        services.AddTaskSchedulers();
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