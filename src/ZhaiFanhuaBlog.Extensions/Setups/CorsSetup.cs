#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CorsSetup
// Guid:031b8d2e-2f06-4b1c-af6d-7a4a0fde77ef
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-03 下午 03:13:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using ZhaiFanhuaBlog.Infrastructure.Apps.Setting;

namespace ZhaiFanhuaBlog.Extensions.Setups;

/// <summary>
/// CorsSetup
/// </summary>
public static class CorsSetup
{
    /// <summary>
    /// Cors 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCorsSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var isEnabledCors = AppSettings.Cors.IsEnabled.Get();
        if (!isEnabledCors) return services;
        services.AddCors(options =>
        {
            // 策略名称
            var policyName = AppSettings.Cors.PolicyName.Get();
            // 支持多个域名端口，端口号后不可带/符号
            string[] origins = AppSettings.Cors.Origins.GetSection();
            // 添加指定策略
            options.AddPolicy(name: policyName, policy =>
            {
                // 配置允许访问的域名
                policy.WithOrigins(origins)
                    // 是否允许同源时匹配配置的通配符域
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    // 允许任何请求头
                    .AllowAnyHeader()
                    // 允许任何方法
                    .AllowAnyMethod()
                    // 允许凭据
                    .AllowCredentials()
                    // 允许请求头
                    .WithExposedHeaders("X-Pagination");
            });
        });
        return services;
    }
}