#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CorsModule
// Guid:8c800537-3e07-40ad-a365-f93905611ce1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 3:15:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructure.Core.Apps.Configs;

namespace XiHan.Infrastructure.Core.Modules;

/// <summary>
/// CorsModule
/// </summary>
public static class CorsModule
{
    /// <summary>
    /// 跨源资源共享服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddCorsModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        var isEnabledCors = AppSettings.Cors.IsEnabled.GetValue();
        if (!isEnabledCors) return services;

        services.AddCors(options =>
        {
            // 策略名称
            var policyName = AppSettings.Cors.PolicyName.GetValue();
            // 支持多个域名端口，端口号后不可带/符号
            string[] origins = AppSettings.Cors.Origins.GetSection();
            // 支持多个请求头
            string[] headers = AppSettings.Cors.Headers.GetSection();
            // 添加指定策略
            options.AddPolicy(policyName, policy =>
            {
                // 配置允许访问的域名
                _ = policy.WithOrigins(origins)
                    // 是否允许同源时匹配配置的通配符域
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    // 允许任何请求头
                    .AllowAnyHeader()
                    // 允许任何方法
                    .AllowAnyMethod()
                    // 允许凭据(cookie)
                    .AllowCredentials()
                    // 允许请求头 (SignalR 用此请求头)
                    .WithExposedHeaders(headers);
            });
        });

        return services;
    }
}