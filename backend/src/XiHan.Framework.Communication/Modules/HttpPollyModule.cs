#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpPollyModule
// Guid:bfd8fcbf-b3fa-406c-8f9e-f2e1c95e7376
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 3:08:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using XiHan.Framework.Communication.Https.Enums;
using XiHan.Framework.Communication.Https.Services;

namespace XiHan.Framework.Communication.Modules;

/// <summary>
/// HttpPollyModule
/// </summary>
/// <remarks>使用 HttpClient 进行 HTTP/3 的 Localhost 测试请求时，需要额外的配置：
/// <see href="https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/servers/kestrel/http3?view=aspnetcore-7.0#localhost-testing"/>
/// </remarks>
public static class HttpPollyModule
{
    /// <summary>
    /// Http 请求策略服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddHttpPollyModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // 注入 Http 相关实例
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IHttpPollyService, HttpPollyService>();

        // 若超时则抛出此异常
        AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync([TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)]);

        // 为每个重试定义超时策略
        AsyncTimeoutPolicy<HttpResponseMessage> timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

        // 远程请求
        services.AddHttpClient(HttpGroupEnum.Remote.ToString(), options =>
        {
            options.DefaultRequestHeaders.Add("Accept", "application/json");
        })
            // 忽略 SSL 不安全检查，或 HTTPS 不安全或 HTTPS 证书有误
            .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            })
            // 设置客户端生存期为 5 分钟
            .SetHandlerLifetime(TimeSpan.FromSeconds(5))
            // 将超时策略放在重试策略之内，每次重试会应用此超时策略
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(timeoutPolicy);

        // 本地请求
        services.AddHttpClient(HttpGroupEnum.Local.ToString(), options =>
        {
            options.BaseAddress = new Uri("http://www.localhost.com");
            options.DefaultRequestHeaders.Add("Accept", "application/json");
            // 需要额外的配置
            options.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        }).AddPolicyHandler(retryPolicy)
        .AddPolicyHandler(timeoutPolicy);

        return services;
    }
}