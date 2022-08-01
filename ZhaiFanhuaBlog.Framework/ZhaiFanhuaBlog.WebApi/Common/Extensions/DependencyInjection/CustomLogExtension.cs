// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomLogExtension
// Guid:17417bc3-81d3-4124-a1e6-efe266d535cb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-13 上午 04:05:22
// ----------------------------------------------------------------

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomLogExtension
/// </summary>
public static class CustomLogExtension
{
    /// <summary>
    /// Log服务扩展
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static ILoggingBuilder AddCustomLog(this ILoggingBuilder builder)
    {
        IConfiguration SerilogConfig = new ConfigurationBuilder()
                        .AddJsonFile(@"Common/Config/Serilog.json")
                        .Build();

        Serilog.Log.Logger = new LoggerConfiguration()
                // 最小的记录等级
                .MinimumLevel.Information()
                // 对其他日志进行重写,除此之外,目前框架只有微软自带的日志组件
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                // 输出到控制台
                .WriteTo.Console(theme: SystemConsoleTheme.Colored)
                // 读取配置文件
                .ReadFrom.Configuration(SerilogConfig)
                .CreateLogger();
        builder.AddSerilog();
        return builder;
    }
}