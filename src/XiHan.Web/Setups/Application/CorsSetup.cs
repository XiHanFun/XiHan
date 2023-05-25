#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CorsSetup
// Guid:8015c072-e6b5-4a52-aec5-6bd747cfe0a2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 01:25:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using XiHan.Infrastructures.Apps.Configs;

namespace XiHan.Web.Setups.Application;

/// <summary>
/// CorsSetup
/// </summary>
public static class CorsSetup
{
    /// <summary>
    /// Cors 应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IApplicationBuilder UseCorsSetup(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        var isEnabledCors = AppSettings.Cors.IsEnabled.GetValue();
        if (!isEnabledCors)
        {
            return app;
        }
        // 策略名称
        var policyName = AppSettings.Cors.PolicyName.GetValue();
        // 对 UseCors 的调用必须放在 UseRouting 之后，但在 UseAuthorization 之前
        app.UseCors(policyName);
        return app;
    }
}