#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CorsMiddleware
// Guid:8015c072-e6b5-4a52-aec5-6bd747cfe0a2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 01:25:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using ZhaiFanhuaBlog.Infrastructure.AppSetting;

namespace ZhaiFanhuaBlog.Extensions.Middlewares;

/// <summary>
/// CorsMiddleware
/// </summary>
public static class CorsMiddleware
{
    /// <summary>
    /// Cors应用扩展
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        bool isEnabledCors = AppSettings.Cors.IsEnabled;
        if (isEnabledCors)
        {
            // 策略名称
            string policyName = AppSettings.Cors.PolicyName;
            // 对 UseCors 的调用必须放在 UseRouting 之后，但在 UseAuthorization 之前
            app.UseCors(policyName);
        }
        return app;
    }
}