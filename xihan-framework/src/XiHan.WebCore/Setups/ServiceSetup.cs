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
    /// 服务扩展 依赖注入
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddServiceSetup(this IServiceCollection services)
    {
        "Services Start……".WriteLineInfo();
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 基础服务注册
        AppServiceProvider.RegisterService(services);

        // 对象映射
        _ = services.AddMapsterSetup();
        // 缓存
        _ = services.AddCacheSetup();
        // SqlSugar
        _ = services.AddSqlSugarSetup();
        // Http上下文
        _ = services.AddHttpPollySetup();
        // 性能分析
        _ = services.AddMiniProfilerSetup();
        // 接口文档
        _ = services.AddSwaggerSetup();
        // 路由
        _ = services.AddRouteSetup();
        // 限流
        _ = services.AddRateLimiterSetup();
        // 跨域
        _ = services.AddCorsSetup();
        // 鉴权授权
        _ = services.AddAuthSetup();
        // Controllers
        _ = services.AddControllersSetup();
        // RabbitMQ
        _ = services.AddRabbitMqSetup();
        // 即时通讯
        _ = services.AddSignalRSetup();
        // 健康检查
        _ = services.AddHealthChecks();
        // 响应缓存
        _ = services.AddResponseCacheSetup();
        // 终端
        _ = services.AddEndpointsApiExplorer();
        // 任务队列
        _ = services.AddJobSetup();

        "Services Started Successfully！".WriteLineSuccess();
        return services;
    }
}