// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomCacheExtension
// Guid:5c45f05d-b77a-4ffa-8975-77aff404eb20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-28 下午 11:29:28
// ----------------------------------------------------------------

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomCacheExtension
/// </summary>
public static class CustomCacheExtension
{
    /// <summary>
    /// Cache扩展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomCache(this IServiceCollection services, IConfiguration config)
    {
        if (config.GetValue<bool>("Cache:IsEnabled"))
        {
            if (config.GetValue<bool>("Cache:MemoryCache:IsEnabled"))
            {
                services.AddMemoryCache();
            }
            if (config.GetValue<bool>("Cache:DistributedCache:IsEnabled"))
            {
                //// CSRedis
                //services.AddDistributedMemoryCache(options =>
                //{
                //    // CSRedisClient实例化的对象注册成全局单例
                //    var redis = new CSRedis.CSRedisClient(config["Cache:ConnectionString:Redis"]);
                //    services.AddSingleton(redis);
                //});
                // StackExchangeRedis
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = config.GetValue<string>("Cache:DistributedCache:Redis:ConnectionString");
                    options.InstanceName = "SampleInstance";
                });
                services.AddOptions();
                // services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
                services.AddSingleton<IDistributedCache, RedisCache>();
            }
            if (config.GetValue<bool>("Cache:ResponseCache:IsEnabled"))
            {
                services.AddResponseCaching();
            }
        }
        return services;
    }
}