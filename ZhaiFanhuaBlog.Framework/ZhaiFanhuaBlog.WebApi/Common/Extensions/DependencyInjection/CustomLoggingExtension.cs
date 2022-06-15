// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomLoggingExtension
// Guid:17417bc3-81d3-4124-a1e6-efe266d535cb
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-13 上午 04:05:22
// ----------------------------------------------------------------

using NLog.Extensions.Logging;
using Serilog;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomLoggingExtension
/// </summary>
public static class CustomLoggingExtension
{
    /// <summary>
    /// Logging扩展
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static ILoggingBuilder AddCustomLog(this ILoggingBuilder builder, IConfiguration config)
    {
        string logType = config.GetValue<string>("Logging:Type");
        builder = logType switch
        {
            "Log4Net" => builder.AddLog4Net(@"Common/Config/Log4Net.config"),
            "Serilog" => builder.AddSerilog(),
            "NLog" => builder.AddNLog(@"Common/Config/NLog.config"),
            _ => throw new NotImplementedException(),
        };
        return builder;
    }
}