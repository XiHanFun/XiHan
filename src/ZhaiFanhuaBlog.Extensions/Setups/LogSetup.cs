#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:LogSetup
// Guid:17417bc3-81d3-4124-a1e6-efe266d535cb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-13 上午 04:05:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// LogSetup
/// </summary>
public static class LogSetup
{
    /// <summary>
    /// Log 服务扩展
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static ILoggingBuilder AddLogSetup(this ILoggingBuilder builder)
    {
        string infoTemplate = @"================{NewLine}记录时间：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志级别：{Level}{NewLine}请求类名：{SourceContext}{NewLine}消息描述：{Message}{NewLine}================{NewLine}{NewLine}";
        string otherTemplate = @"================{NewLine}记录时间：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志级别：{Level}{NewLine}请求类名：{SourceContext}{NewLine}消息描述：{Message}{NewLine}错误详情：{Exception}{NewLine}================{NewLine}{NewLine}";
        string infoPath = AppContext.BaseDirectory + @"Logs/Info/.log";
        string waringPath = AppContext.BaseDirectory + @"Logs/Waring/.log";
        string errorPath = AppContext.BaseDirectory + @"Logs/Error/.log";
        string fatalPath = AppContext.BaseDirectory + @"Logs/Fatal/.log";
        Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                // 日志调用类命名空间如果以 Microsoft 开头，覆盖日志输出最小级别为 Warning
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                // 输出到控制台
                //.WriteTo.Console()
                .Enrich.FromLogContext()
                // -----------------------------------------Information------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Information)
                // 异步输出到文件
                .WriteTo.Async(congfig => congfig.File(
                    // 配置日志输出到文件，文件输出到当前项目的 logs 目录下，linux 中大写会出错
                    infoPath.ToLower(),
                    // 日记的生成周期为每天
                    rollingInterval: RollingInterval.Day,
                    // 单位字节不配置时，默认1GB
                    fileSizeLimitBytes: 1024 * 1024 * 10,
                    // 保留最近多少个文件，默认31个
                    retainedFileCountLimit: 10,
                    // 超过文件大小时，自动创建新文件
                    rollOnFileSizeLimit: true,
                    shared: true,
                    outputTemplate: infoTemplate)
                ))
                // -----------------------------------------Warning------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Warning)
                .WriteTo.Async(congfig => congfig.File(
                    waringPath.ToLower(),
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 1024 * 1024 * 10,
                    retainedFileCountLimit: 10,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    outputTemplate: otherTemplate)
                ))
                // -----------------------------------------Error------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Error)
                .WriteTo.Async(congfig => congfig.File(
                    errorPath.ToLower(),
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 1024 * 1024 * 10,
                    retainedFileCountLimit: 10,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    outputTemplate: otherTemplate)
                ))
                // -----------------------------------------Fatal------------------------------------------------
                .WriteTo.Logger(log => log.Filter.ByIncludingOnly(lev => lev.Level == LogEventLevel.Fatal)
                .WriteTo.Async(congfig => congfig.File(
                    fatalPath.ToLower(),
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 1024 * 1024 * 10,
                    retainedFileCountLimit: 10,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    outputTemplate: otherTemplate)
                ))
                .CreateLogger();
        builder.AddSerilog();
        return builder;
    }
}