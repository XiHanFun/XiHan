// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomCacheExtension
// Guid:5c45f05d-b77a-4ffa-8975-77aff404eb20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-28 下午 11:29:28
// ----------------------------------------------------------------

using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using ZhaiFanhuaBlog.Utils.Config;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomCacheExtension
/// </summary>
public static class CustomCacheExtension
{
    /// <summary>
    /// Cache服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomCache(this IServiceCollection services)
    {
        if (ConfigHelper.Configuration.GetValue<bool>("Cache:IsEnabled"))
        {
            services.AddMemoryCache(options => new MemoryCacheOptions
            {
                // 最大缓存个数限制
                SizeLimit = 60
            });
            // 分布式缓存
            if (ConfigHelper.Configuration.GetValue<bool>("Cache:DistributedCache:IsEnabled"))
            {
                // CSRedis
                var connectionString = ConfigHelper.Configuration.GetValue<string>("Cache:DistributedCache:Redis:ConnectionString");
                var instanceName = ConfigHelper.Configuration.GetValue<string>("Cache:DistributedCache:Redis:InstanceName");
                var redisStr = $"{connectionString}, prefix = {instanceName}";
                // 用法一：基于Redis初始化IDistributedCache
                services.AddSingleton(new CSRedisClient(redisStr));
                services.AddSingleton<IDistributedCache>(new CSRedisCache(new CSRedisClient(redisStr)));
                // 用法二：帮助类直接调用
                RedisHelper.Initialization(new CSRedisClient(redisStr));
            }
            // 响应缓存
            if (ConfigHelper.Configuration.GetValue<bool>("Cache:ResponseCache:IsEnabled"))
            {
                services.AddResponseCaching();
            }
        }
        return services;
    }
}