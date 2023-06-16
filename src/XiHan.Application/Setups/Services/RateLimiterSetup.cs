#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:RateLimiterSetup
// Guid:e03abdbe-8b80-488f-9644-d4ec72075f2b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/6/16 23:05:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;
using XiHan.Infrastructures.Responses.Results;

namespace XiHan.Application.Setups.Services;

/// <summary>
/// RateLimiterSetup
/// </summary>
public static class RateLimiterSetup
{
    /// <summary>
    /// 限流服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddRateLimiterSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddRateLimiter(options =>
        {
            // 全局限流器
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(partitionKey: "fixed", partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 1000,
                    QueueLimit = 0,
                    Window = TimeSpan.FromMinutes(1)
                }));

            // 自定义限流策略
            options.AddPolicy("MyPolicy", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(partitionKey: "MyPolicy", partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 10,
                    QueueLimit = 0,
                    Window = TimeSpan.FromMinutes(1)
                }));

            // 设置拒绝请求响应码，默认为Status503ServiceUnavailable
            options.RejectionStatusCode = 429;
            // 设置拒绝请求信息
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    await context.HttpContext.Response.WriteAsJsonAsync(ResultDto.TooManyRequests($"请求过多，请在{retryAfter.TotalMinutes}分钟后重试。"), token);
                }
                else
                {
                    // 返回自定义的未授权模型数据
                    await context.HttpContext.Response.WriteAsJsonAsync(ResultDto.TooManyRequests("请稍后重试。"), token);
                }
            };
        });
        return services;
    }
}