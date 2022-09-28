// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CorsMiddleware
// Guid:8015c072-e6b5-4a52-aec5-6bd747cfe0a2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 01:25:14
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using ZhaiFanhuaBlog.Core.AppSettings;

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
        bool isEnabledCors = AppConfig.Configuration.GetValue<bool>("Cors:IsEnabled");
        if (isEnabledCors)
        {
            // 策略名称
            string policyName = AppConfig.Configuration.GetValue<string>("Cors:PolicyName");
            // 对 UseCors 的调用必须放在 UseRouting 之后，但在 UseAuthorization 之前
            app.UseCors(policyName);
        }
        return app;
    }
}