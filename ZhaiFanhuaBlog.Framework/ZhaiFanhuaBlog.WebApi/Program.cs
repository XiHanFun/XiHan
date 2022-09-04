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

        var config = builder.Configuration;
        ConsoleHelper.WriteLineWarning("Configuration Start……");
        ConfigHelper.Configuration = config;
        ConsoleHelper.WriteLineSuccess("Configuration Started Successfully！");

        var log = builder.Logging;
        ConsoleHelper.WriteLineWarning("Log Start……");
        log.AddCustomLog();
        ConsoleHelper.WriteLineSuccess("Log Started Successfully！");

        var services = builder.Services;
        ConsoleHelper.WriteLineWarning("Services Start……");
        // Cache
        services.AddCustomCache();
        // JWT
        services.AddCustomJWT();
        // Http请求
        services.AddHttpClient();
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

        var app = builder.Build();
        ConsoleHelper.WriteLineWarning("ZhaiFanhuaBlog Application Start……");
        // 环境变量，开发环境
        if (app.Environment.IsDevelopment())
        {
            // 生成异常页面
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // 使用HSTS的中间件，该中间件添加了严格传输安全头
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
        // 身份验证
        app.UseAuthentication();
        // 认证授权
        app.UseAuthorization();

        // 路由映射
        app.UseEndpoints(options =>
        {
            // 不对约定路由做任何假设，也就是不使用约定路由，依赖用户的特性路由， 一般用在WebAPI项目中
            options.MapControllers();
        });
        ConsoleHelper.WriteLineSuccess("ZhaiFanhuaBlog Application Started Successfully！");

        // 启动信息打印
        ConsoleInfo.ConsoleInfos();
        app.Run();
    }
}