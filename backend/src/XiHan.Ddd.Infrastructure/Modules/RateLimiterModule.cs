#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RateLimiterModule
// Guid:f8f9e94e-2578-4679-9821-2b524db95908
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 4:10:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;
using XiHan.Common.Utilities.Extensions;
using XiHan.Core.Format;
using XiHan.Infrastructure.Core.Apps.RequestOrResponse;

namespace XiHan.Infrastructure.Core.Modules;

/// <summary>
/// RateLimiterModule
/// </summary>
public static class RateLimiterModule
{
    /// <summary>
    /// 限流服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddRateLimiterModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddRateLimiter(limiterOptions =>
        {
            // 设置拒绝请求响应码，默认为Status503ServiceUnavailable
            limiterOptions.RejectionStatusCode = 429;
            // 设置拒绝请求信息
            limiterOptions.OnRejected = async (context, token) =>
            {
                // 返回自定义的并发请求过多模型数据
                context.HttpContext.Response.StatusCode = 429;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    await context.HttpContext.Response.WriteAsJsonAsync(
                           ResponseBody.TooManyRequests($"请求过多，请在{retryAfter.FormatTimeSpanToString()}后重试。"), token);
                }
                else
                {
                    await context.HttpContext.Response.WriteAsJsonAsync(ResponseBody.TooManyRequests("请求过多，请稍后重试。"), token);
                }
            };

            // 自定义限流策略
            limiterOptions.AddPolicy("MyPolicy", _ =>
                RateLimitPartition.GetFixedWindowLimiter("MyPolicy", _ => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 10,
                    QueueLimit = 2,
                    Window = TimeSpan.FromMinutes(1)
                }));

            // 全局限制器
            //limiterOptions.GlobalLimiter = PartitionedRateLimiter.CreateChained();
        });

        return services;
    }

    #region 并发限制器

    /// <summary>
    /// 并发限制器仅限制并发请求数，不限制一段时间内的请求数。
    /// </summary>
    /// <returns></returns>
    private static ConcurrencyLimiter GetConcurrencyLimiters()
    {
        return new ConcurrencyLimiter(new ConcurrencyLimiterOptions
        {
            PermitLimit = 10,
            QueueLimit = 2,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    }

    #endregion

    #region 令牌池限制器

    /// <summary>
    /// 令牌限制器 限制一个时间段内的最大请求数
    /// </summary>
    /// <returns></returns>
    private static TokenBucketRateLimiter GetTokenBucketRateLimiters()
    {
        return new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
        {
            TokenLimit = 10,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 2,
            ReplenishmentPeriod = TimeSpan.FromMinutes(1),
            TokensPerPeriod = 5,
            AutoReplenishment = true
        });
    }

    #endregion

    #region 滑动限制器

    /// <summary>
    /// 滑动限制器 限制一个时间段内的最大请求数
    /// </summary>
    /// <returns></returns>
    private static SlidingWindowRateLimiter GetSlidingWindowRateLimiters()
    {
        return new SlidingWindowRateLimiter(new SlidingWindowRateLimiterOptions
        {
            PermitLimit = 1,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 1,
            Window = TimeSpan.FromSeconds(1),
            SegmentsPerWindow = 1,
            AutoReplenishment = true
        });
    }

    #endregion

    #region 固定限制器

    /// <summary>
    /// 固定限制器 限制一个时间段内的最大请求数
    /// </summary>
    /// <returns></returns>
    private static FixedWindowRateLimiter GetFixedWindowRateLimiters()
    {
        return new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            AutoReplenishment = true,
            PermitLimit = 10,
            QueueLimit = 2,
            Window = TimeSpan.FromMinutes(1)
        });
    }

    #endregion
}