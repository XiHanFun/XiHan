// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Program
// Guid:fccfeb28-624c-41cb-9c5c-0b0652648a6b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-17 下午 04:01:21
// ----------------------------------------------------------------

using Microsoft.AspNetCore.HttpOverrides;
using ZhaiFanhuaBlog.Utils.Config;
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

        ConsoleHelper.WriteLineWarning("Configuration Start……");
        var config = builder.Configuration;
        ConfigHelper.Configuration = config;

        ConsoleHelper.WriteLineSuccess("Configuration Started Successfully！");
        ConsoleHelper.WriteLineWarning("Log Start……");

        var log = builder.Logging;
        log.AddCustomLog();

        ConsoleHelper.WriteLineSuccess("Log Started Successfully！");
        ConsoleHelper.WriteLineWarning("Services Start……");

        var services = builder.Services;
        // Cache
        services.AddCustomCache();
        // JWT
        services.AddCustomJWT();
        // Swagger
        services.AddCustomSwagger();
        // SqlSugar
        services.AddCustomSqlSugar();
        // IOC
        services.AddCustomIOC();
        // AutoMapper
        services.AddCustomAutoMapper();
        // Route
        services.AddCustomRoute();
        // Cors
        services.AddCustomCors();
        // Controllers
        services.AddCustomControllers();

        ConsoleHelper.WriteLineSuccess("Services Started Successfully！");
        ConsoleHelper.WriteLineWarning("ZhaiFanhuaBlog Application Start……");

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // HTTP 严格传输安全
            app.UseHsts();
            // 强制https跳转
            app.UseHttpsRedirection();
        }
        // Nginx 反向代理获取真实IP
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        // Swagger
        app.UseCustomSwagger();
        // 使用静态文件
        app.UseStaticFiles();
        // Serilog请求日志中间件---必须在 UseStaticFiles 和 UseRouting 之间
        //app.UseSerilog();
        // 路由
        app.UseRouting();
        // 跨域
        app.UseCustomCors();
        // 鉴权
        app.UseAuthentication();
        // 授权
        app.UseAuthorization();

        app.MapControllers();
        ConsoleHelper.WriteLineSuccess("ZhaiFanhuaBlog Application Started Successfully！");

        // 打印信息
        ConsoleInfo.ConsoleInfos();
        app.Run();
    }
}