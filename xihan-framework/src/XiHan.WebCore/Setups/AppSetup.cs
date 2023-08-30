#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppSetup
// Guid:b216dd59-346e-44c1-bdef-1830ebca6d0f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-28 下午 08:00:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using XiHan.Infrastructures.Apps.Environments;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Subscriptions.Hubs;
using XiHan.Utils.Extensions;
using XiHan.WebCore.Middlewares;
using XiHan.WebCore.Setups.Apps;

namespace XiHan.WebCore.Setups;

/// <summary>
/// AppSetup
/// </summary>
public static class AppSetup
{
    /// <summary>
    /// 应用扩展 配置中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseApplicationSetup(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        "XiHan Application Start……".WriteLineInfo();
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        // 注入全局宿主环境
        AppEnvironmentProvider.WebHostEnvironment = env;
        // 注入全局服务
        AppServiceProvider.ServiceProvider = app.ApplicationServices;

        // 数据库初始化
        _ = app.UseSqlSugarSetup();
        // Http
        _ = app.UseHttpSetup(env);
        // 添加静态文件中间件，访问 wwwroot 目录文件，必须在 UseRouting 之前
        _ = app.UseStaticFiles();
        // 添加WebSocket支持，SignalR优先使用WebSocket传输
        _ = app.UseWebSockets();
        // MiniProfiler
        _ = app.UseMiniProfilerSetup();
        // Swagger
        _ = app.UseSwaggerSetup();
        // 添加路由中间件
        _ = app.UseRouting();
        // 限流，若作用于特定路由，必须在 UseRouting 之后
        _ = app.UseRateLimiter();
        // 跨域，要放在 UseEndpoints 前
        _ = app.UseCorsSetup();
        // 添加认证中间件
        _ = app.UseAuthentication();
        // 添加授权中间件
        _ = app.UseAuthorization();
        // 开启响应缓存
        _ = app.UseResponseCaching();
        // 全局日志中间件
        _ = app.UseMiddleware<GlobalLogMiddleware>();
        // 恢复或启动任务
        _ = app.UseTaskSchedulers(env);

        // 添加终端中间件
        _ = app.UseEndpoints(endpoints =>
        {
            // 不对约定路由做任何假设，也就是不使用约定路由，依赖用户的特性路由
            _ = endpoints.MapControllers();
            // 健康检查
            _ = endpoints.MapHealthChecks("/Health");
            // 即时通讯集线器
            _ = endpoints.MapHub<ChatHub>("/ChatHub");
        });

        "XiHan Application Started Successfully！".WriteLineSuccess();
        return app;
    }
}