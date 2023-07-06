#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CorsSetup
// Guid:031b8d2e-2f06-4b1c-af6d-7a4a0fde77ef
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-03 下午 03:13:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructures.Apps.Configs;

namespace XiHan.Application.Setups.Services;

/// <summary>
/// CorsSetup
/// </summary>
public static class CorsSetup
{
    /// <summary>
    /// 跨源资源共享服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddCorsSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        var isEnabledCors = AppSettings.Cors.IsEnabled.GetValue();
        if (!isEnabledCors) return services;
        services.AddCors(options =>
        {
            // 策略名称
            var policyName = AppSettings.Cors.PolicyName.GetValue();
            // 支持多个域名端口，端口号后不可带/符号
            var origins = AppSettings.Cors.Origins.GetSection();
            // 支持多个请求头
            var headers = AppSettings.Cors.Headers.GetSection();
            // 添加指定策略
            options.AddPolicy(policyName, policy =>
            {
                // 配置允许访问的域名
                policy.WithOrigins(origins)
                    // 是否允许同源时匹配配置的通配符域
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    // 允许任何请求头
                    .AllowAnyHeader()
                    // 允许任何方法
                    .AllowAnyMethod()
                    // 允许凭据（cookie）
                    .AllowCredentials()
                    // 允许请求头 (SignalR 用此请求头)
                    .WithExposedHeaders(headers);
            });
        });
        return services;
    }
}