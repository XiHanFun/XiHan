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
using Serilog;
using Serilog.Events;
using System.Text;
using XiHan.Utils.Consoles;
using XiHan.Utils.Infos;

namespace XiHan.Infrastructure.Apps.Logging;

/// <summary>
/// 全局日志管理器
/// </summary>
public static class AppLogManager
{
    /// <summary>
    /// 注册日志
    /// </summary>
    /// <param name="builder"></param>
    public static void RegisterLog(ILoggingBuilder builder)
    {
        var debugPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Debug/.log";
        var infoPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Info/.log";
        var waringPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Waring/.log";
        var errorPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Error/.log";
        var fatalPath = ApplicationInfoHelper.CurrentDirectory + @"Logs/Fatal/.log";
        var infoTemplate = @"Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Message：{Message}{NewLine}================{NewLine}";
        var warnTemplate = @"Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Source：{SourceContext}{NewLine}Message：{Message}{NewLine}================{NewLine}";
        var errorTemplate = @"Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}Level：{Level}{NewLine}Source：{SourceContext}{NewLine}Message：{Message}{NewLine}Exception：{Exception}{NewLine}Properties：{Properties}{NewLine}================{NewLine}";

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
                .SinkFileConfig(LogEventLevel.Debug, debugPath, infoTemplate)
#endif
                .SinkFileConfig(LogEventLevel.Information, infoPath, infoTemplate)
                .SinkFileConfig(LogEventLevel.Warning, waringPath, warnTemplate)
                .SinkFileConfig(LogEventLevel.Error, errorPath, errorTemplate)
                .SinkFileConfig(LogEventLevel.Fatal, fatalPath, errorTemplate)
                .CreateLogger();
        builder.AddSerilog();
        var infoMsg = $"日志注册：{Log.Logger}";
        Log.Information(infoMsg);
        infoMsg.WriteLineSuccess();
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
                    .WriteTo.Async(congfig => congfig.File(
                            // 配置日志输出到文件，文件输出到当前项目的 logs 目录下，linux 中大写会出错
                            path: filePath.ToLowerInvariant(),
                            // 生成周期：天
                            rollingInterval: RollingInterval.Hour,
                            // 文件大小：10M，默认1GB
                            fileSizeLimitBytes: 1024 * 1024 * 10,
                            // 保留最近：15个文件，默认31个，等于null时永远保留文件
                            retainedFileCountLimit: 15,
                            // 超过大小自动创建新文件
                            rollOnFileSizeLimit: true,
                            // 最小写入级别
                            restrictedToMinimumLevel: level,
                            // 写入模板
                            outputTemplate: template,
                            // 编码
                            encoding: Encoding.UTF8)
                    ));
    }
}