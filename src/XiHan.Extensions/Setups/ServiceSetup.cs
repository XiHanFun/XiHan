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
using XiHan.Utils.Consoles;

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
        "Services Start……".WriteLineInfo();
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Cache
        services.AddCacheSetup();
        // SqlSugar
        services.AddSqlSugarSetup();
        // AutoMapper
        services.AddAutoMapperSetup();
        // Auth
        services.AddAuthSetup();
        // Http上下文
        services.AddHttpContextSetup();
        // Swagger
        services.AddSwaggerSetup();
        // RabbitMQ
        services.AddRabbitMQSetup();
        // 性能分析
        services.AddMiniProfilerSetup();
        // 健康检查
        services.AddHealthChecks();
        // 即时通讯
        services.AddSignalR();
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