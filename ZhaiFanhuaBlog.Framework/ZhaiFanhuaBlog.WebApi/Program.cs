// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Program
// Guid:fccfeb28-624c-41cb-9c5c-0b0652648a6b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-17 下午 04:01:21
// ----------------------------------------------------------------

using Microsoft.AspNetCore.HttpOverrides;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.Console;
using ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

namespace ZhaiFanhuaBlog.WebApi;

/// <summary>
/// Program
/// </summary>
public class Program
{
    /// <summary>
    /// 入口
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConsoleHelper.WriteInfoLine("Configuration Start……");
        var config = builder.Configuration;
        ConsoleHelper.WriteSuccessLine("Configuration Started Successfully！");
        ConsoleHelper.WriteInfoLine("Log Start……");
        var log = builder.Logging;
        log.AddCustomLog(config);
        ConsoleHelper.WriteSuccessLine("Log Started Successfully！");
        ConsoleHelper.WriteInfoLine("Services Start……");
        var services = builder.Services;
        // Cache
        services.AddCustomCache(config);
        // JWT 必须在AddControllers()之前注册
        services.AddCustomJWT(config);
        // Controllers
        services.AddCustomControllers(config);
        // Swagger
        services.AddCustomSwagger(config);
        // SqlSugar
        services.AddCustomSqlSugar(config);
        // IOC
        services.AddCustomIOC(config);
        // AutoMapper
        services.AddCustomAutoMapper(config);
        // Route
        services.AddCustomRoute(config);
        // Cors
        services.AddCustomCors(config);
        ConsoleHelper.WriteSuccessLine("Services Started Successfully！");
        ConsoleHelper.WriteInfoLine("ZhaiFanhuaBlog Application Start……");

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // 异常页面
            app.UseExceptionHandler("/Home/Error");
            // HTTP 严格传输安全
            app.UseHsts();
        }
        // Nginx 反向代理获取真实IP
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        // Swagger
        app.UseCustomSwagger(config);
        // 跨域
        app.UseCors();
        // 强制https跳转
        app.UseHttpsRedirection();
        // 使用静态文件
        app.UseStaticFiles();
        // Serilog请求日志中间件---必须在 UseStaticFiles 和 UseRouting 之间
        //app.UseSerilog();
        // 路由
        app.UseRouting();
        // 鉴权
        app.UseAuthentication();
        // 授权
        app.UseAuthorization();

        app.MapControllers();
        // 打印信息
        ConsoleInfo.ConsoleInfos();
        app.Run();
    }
}