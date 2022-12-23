#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppLogManager
// Guid:3cb34283-30a3-45cf-8cdb-599bb722a211
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-24 上午 02:07:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.Infrastructure.Apps.Logging;

/// <summary>
/// AppLogManager
/// </summary>
public static class AppLogManager
{
    /// <summary>
    /// 注册日志
    /// </summary>
    /// <param name="builder"></param>
    public static void RegisterLog(ILoggingBuilder builder)
    {
        var infoTemplate = @"================{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Source：{SourceContext}{NewLine}Message：{Message}{NewLine}================{NewLine}{NewLine}";
        var errorTemplate = @"================{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Source：{SourceContext}{NewLine}Message：{Message}{NewLine}Exception：{Exception}{NewLine}================{NewLine}{NewLine}";
        var infoPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Info/.log";
        var waringPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Waring/.log";
        var errorPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Error/.log";
        var fatalPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Fatal/.log";
        Log.Logger = new LoggerConfiguration()
                // 记录相关上下文信息
                .Enrich.FromLogContext()
#if DEBUG
                // 最小记录级别
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                // 日志调用类命名空间如果以 Microsoft 开头的其他日志进行重写，覆盖日志输出最小级别为 Warning
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                // -----------------------------------------Information------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Information)
                // 异步输出到文件
                .WriteTo.Async(congfig => congfig.File(
                        // 配置日志输出到文件，文件输出到当前项目的 logs 目录下，linux 中大写会出错
                        path: infoPath.ToLowerInvariant(),
                        // 生成周期：天
                        rollingInterval: RollingInterval.Day,
                        // 文件大小：10M，默认1GB
                        fileSizeLimitBytes: 1024 * 1024 * 10,
                        // 保留最近：10个文件，默认31个，等于null时永远保留文件
                        retainedFileCountLimit: 10,
                        // 超过大小自动创建新文件
                        rollOnFileSizeLimit: true,
                        // 最小写入级别
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        // 写入模板
                        outputTemplate: infoTemplate)
                ))
                // -----------------------------------------Warning------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Warning)
                .WriteTo.Async(congfig => congfig.File(
                        path: waringPath.ToLowerInvariant(),
                        rollingInterval: RollingInterval.Day,
                        fileSizeLimitBytes: 1024 * 1024 * 10,
                        retainedFileCountLimit: 10,
                        rollOnFileSizeLimit: true,
                        restrictedToMinimumLevel: LogEventLevel.Warning,
                        outputTemplate: infoTemplate)
                ))
                // ------------------------------------------ Error------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Error)
                .WriteTo.Async(congfig => congfig.File(
                        path: errorPath.ToLowerInvariant(),
                        rollingInterval: RollingInterval.Day,
                        fileSizeLimitBytes: 1024 * 1024 * 10,
                        retainedFileCountLimit: 10,
                        rollOnFileSizeLimit: true,
                        restrictedToMinimumLevel: LogEventLevel.Error,
                        outputTemplate: errorTemplate)
                ))
                // -----------------------------------------Fatal------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Fatal)
                .WriteTo.Async(congfig => congfig.File(
                        path: fatalPath.ToLowerInvariant(),
                        rollingInterval: RollingInterval.Day,
                        fileSizeLimitBytes: 1024 * 1024 * 10,
                        retainedFileCountLimit: 10,
                        rollOnFileSizeLimit: true,
                        restrictedToMinimumLevel: LogEventLevel.Fatal,
                        outputTemplate: errorTemplate)
                ))
                .CreateLogger();
        builder.AddSerilog();
        $"日志注册：{nameof(Log)}".WriteLineSuccess();
    }
}