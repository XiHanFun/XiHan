#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ApplicationSetup
// Guid:b216dd59-346e-44c1-bdef-1830ebca6d0f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-28 下午 08:00:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using XiHan.Commons.Apps.Services;
using XiHan.Subscriptions.Hubs;
using XiHan.Utils.Consoles;
using XiHan.Web.Setups.Application;

namespace XiHan.Web.Setups;

/// <summary>
/// ApplicationSetup
/// </summary>
public static class ApplicationSetup
{
    /// <summary>
    /// 应用扩展 配置中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="streamHtml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseApplicationSetup(this IApplicationBuilder app, IWebHostEnvironment env, Func<Stream> streamHtml)
    {
        "XiHan Application Start……".WriteLineInfo();
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        // 初始化数据库
        app.InitDatabase();
        // Http
        app.UseHttpSetup(env);
        // MiniProfiler
        app.UseMiniProfilerSetup();
        // Swagger
        app.UseSwaggerSetup(streamHtml);
        // 使用静态文件
        app.UseStaticFiles();
        // 路由
        app.UseRouting();
        // 跨域
        app.UseCorsSetup();
        // 鉴权授权
        app.UseAuthentication();
        app.UseAuthorization();
        // 恢复或启动任务
        app.UseTaskSchedulers();

        app.UseEndpoints(endpoints =>
        {
            // 不对约定路由做任何假设，也就是不使用约定路由，依赖用户的特性路由
            endpoints.MapControllers();
            // 健康检查
            endpoints.MapHealthChecks("/Health");
            // 即时通讯
            endpoints.MapHub<ChatHub>("/Chathub");
        });

        // 注入全局服务
        AppServiceManager.ServiceProvider = app.ApplicationServices;

        "XiHan Application Started Successfully！".WriteLineSuccess();
        return app;
    }
}