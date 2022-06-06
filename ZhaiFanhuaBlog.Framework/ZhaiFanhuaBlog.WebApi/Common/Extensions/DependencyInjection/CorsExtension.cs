// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CorsExtension
// Guid:031b8d2e-2f06-4b1c-af6d-7a4a0fde77ef
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-03 下午 03:13:42
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CorsExtension
/// </summary>
public static class CorsExtension
{
    /// <summary>
    /// Cors扩展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors(options =>
        {
            // 默认策略
            options.AddDefaultPolicy(builder =>
            {
                string[] origins = config.GetSection("Cors:Origins").Get<string[]>();
                // 配置允许访问的域名
                builder.WithOrigins(origins)
                // 允许任何方法
                .AllowAnyMethod()
                // 允许任何请求头
                .AllowAnyHeader()
                // 允许凭据
                .AllowCredentials();
            });
        });
        return services;
    }
}