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
using ZhaiFanhuaBlog.Infrastructure.Apps.Logging;
using ZhaiFanhuaBlog.Utils.Console;

namespace ZhaiFanhuaBlog.Extensions.Setups;

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
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        // 配置日志
        "Log Start……".WriteLineWarning();
        AppLogManager.RegisterLog(builder);
        "Log Started Successfully！".WriteLineSuccess();

        return builder;
    }
}