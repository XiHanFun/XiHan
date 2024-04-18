#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SerilogModule
// Guid:8c73f865-4f51-43e0-ae46-5cc2b4058f7f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 3:49:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Text;

namespace XiHan.Infrastructure.Core.Modules;

/// <summary>
/// SerilogModule
/// </summary>
public static class SerilogModule
{
    /// <summary>
    /// Serilog 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddSerilogModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        const string debugPath = @"Logs/Debug/.log";
        const string infoPath = @"Logs/Info/.log";
        const string waringPath = @"Logs/Waring/.log";
        const string errorPath = @"Logs/Error/.log";
        const string fatalPath = @"Logs/Fatal/.log";

        const string infoTemplate =
            @"Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Message：{Message}{NewLine}================{NewLine}";
        const string warnTemplate =
            @"Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Source：{SourceContext}{NewLine}Message：{Message}{NewLine}================{NewLine}";
        const string errorTemplate =
            @"Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Source：{SourceContext}{NewLine}Message：{Message}{NewLine}Exception：{Exception}{NewLine}Properties：{Properties}{NewLine}================{NewLine}";

        Log.Logger = new LoggerConfiguration()
            // 日志调用类命名空间如果以 Microsoft 开头的其他日志进行重写，覆盖日志输出最小级别为 Warning
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
#if DEBUG
            // 最小记录级别
            .MinimumLevel.Debug()
#endif
            .MinimumLevel.Information()
            // 记录相关上下文信息
            .Enrich.FromLogContext()
#if DEBUG
            .SinkFileConfig(LogEventLevel.Debug, debugPath, errorTemplate)
#endif
            .SinkFileConfig(LogEventLevel.Information, infoPath, infoTemplate)
            .SinkFileConfig(LogEventLevel.Warning, waringPath, warnTemplate)
            .SinkFileConfig(LogEventLevel.Error, errorPath, errorTemplate)
            .SinkFileConfig(LogEventLevel.Fatal, fatalPath, errorTemplate)
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog();
        });

        return services;
    }

    /// <summary>
    /// 返回写入文件对应级别配置
    /// </summary>
    /// <param name="config"></param>
    /// <param name="level"></param>
    /// <param name="filePath"></param>
    /// <param name="template"></param>
    /// <returns></returns>
    private static LoggerConfiguration SinkFileConfig(this LoggerConfiguration config, LogEventLevel level, string filePath, string template)
    {
        return config.WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == level)
            // 异步输出到文件
            .WriteTo.Async(newConfig => newConfig.File(
                // 配置日志输出到文件，文件输出到当前项目的 logs 目录下，linux 中大写会出错
                Path.Combine(AppContext.BaseDirectory, filePath.ToLowerInvariant()),
                // 生成周期：天
                rollingInterval: RollingInterval.Day,
                // 文件大小：10M，默认1GB
                fileSizeLimitBytes: 1024 * 1024 * 10,
                // 保留最近：60个文件，默认31个，等于null时永远保留文件
                retainedFileCountLimit: 60,
                // 超过大小自动创建新文件
                rollOnFileSizeLimit: true,
                // 最小写入级别
                restrictedToMinimumLevel: level,
                // 写入模板
                outputTemplate: template,
                // 编码
                encoding: Encoding.UTF8)));
    }
}