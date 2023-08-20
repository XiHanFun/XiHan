#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpPollySetup
// Guid:2753f2f6-5e39-4e46-b3fa-ff80af47a49f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-05 上午 04:06:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using XiHan.Infrastructures.Requests.Https;

namespace XiHan.WebCore.Setups.Services;

/// <summary>
/// HttpPollySetup
/// </summary>
/// <remarks>使用 HttpClient 进行 HTTP/3 的 Localhost 测试请求时，需要额外的配置：
/// <see href="https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/servers/kestrel/http3?view=aspnetcore-7.0#localhost-testing"/>
/// </remarks>
public static class HttpPollySetup
{
    /// <summary>
    /// Http 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddHttpPollySetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 若超时则抛出此异常
        var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            });
        // 为每个重试定义超时策略
        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

        // 远程请求
        services.AddHttpClient(HttpGroupEnum.Remote.ToString(), options =>
        {
            options.DefaultRequestHeaders.Add("Accept", "application/json");
        })
        // 忽略 SSL 不安全检查，或 HTTPS 不安全或 HTTPS 证书有误
        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
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
        })
        .AddPolicyHandler(retryPolicy)
        .AddPolicyHandler(timeoutPolicy);

        // 注入 Http 相关实例
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IHttpPollyService, HttpPollyService>();

        return services;
    }
}