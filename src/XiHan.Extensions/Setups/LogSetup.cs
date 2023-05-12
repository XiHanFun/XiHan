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
using XiHan.Infrastructure.Apps.Logging;
using XiHan.Utils.Consoles;

namespace XiHan.Extensions.Setups;

/// <summary>
/// LogSetup
/// </summary>
public static class LogSetup
{
    /// <summary>
    /// 日志配置扩展
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static ILoggingBuilder AddLogSetup(this ILoggingBuilder builder)
    {
        "Log Start……".WriteLineInfo();
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        // 注册日志
        AppLogManager.RegisterLog(builder);

        "Log Started Successfully！".WriteLineSuccess();
        return builder;
    }
}