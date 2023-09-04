#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RateLimiterSetup
// Guid:e03abdbe-8b80-488f-9644-d4ec72075f2b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023/6/16 23:05:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;
using XiHan.Infrastructures.Responses;
using XiHan.Utils.Extensions;

namespace XiHan.WebCore.Setups.Services;

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
        if (services == null) throw new ArgumentNullException(nameof(services));

        _ = services.AddRateLimiter(limiterOptions =>
        {
            // 设置拒绝请求响应码，默认为Status503ServiceUnavailable
            limiterOptions.RejectionStatusCode = 429;
            // 设置拒绝请求信息
            limiterOptions.OnRejected = async (context, token) =>
            {
                // 返回自定义的并发请求过多模型数据
                context.HttpContext.Response.StatusCode = 429;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    await context.HttpContext.Response.WriteAsJsonAsync(
                        ApiResult.TooManyRequests($"请求过多，请在{retryAfter.FormatTimeSpanToString()}后重试。"), token);
                else
                    await context.HttpContext.Response.WriteAsJsonAsync(ApiResult.TooManyRequests("请求过多，请稍后重试。"),
                        token);
            };

            // 自定义限流策略
            _ = limiterOptions.AddPolicy("MyPolicy", _ =>
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