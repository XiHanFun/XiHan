#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CacheSetup
// Guid:5c45f05d-b77a-4ffa-8975-77aff404eb20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-28 下午 11:29:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Caches;

namespace XiHan.Web.Setups.Services;

/// <summary>
/// CacheSetup
/// </summary>
public static class CacheSetup
{
    /// <summary>
    /// Cache 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddCacheSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 分布式缓存
        var isEnabledDistributedcache = AppSettings.Cache.Distributedcache.IsEnabled.GetValue();
        if (isEnabledDistributedcache)
        {
            // CSRedis
            var connectionString = AppSettings.Cache.Distributedcache.Redis.ConnectionString.GetValue();
            var instanceName = AppSettings.Cache.Distributedcache.Redis.InstanceName.GetValue();
            var redisStr = $"{connectionString}, prefix = {instanceName}";
            var redisClient = new CSRedisClient(redisStr);
            // 用法一：基于 Redis 初始化 IDistributedCache
            services.AddSingleton(redisClient);
            services.AddSingleton<IDistributedCache>(new CSRedisCache(redisClient));
            // 用法二：帮助类直接调用
            RedisHelper.Initialization(redisClient);
            services.AddDistributedMemoryCache();
        }

        // 内存缓存(默认开启)
        services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
        services.AddSingleton<IAppCacheService, AppMemoryCacheService>();
        services.AddMemoryCache();

        // 响应缓存
        var isEnabledResponseCache = AppSettings.Cache.ResponseCache.IsEnabled.GetValue();
        if (isEnabledResponseCache)
        {
            services.AddResponseCaching();
        }

        return services;
    }
}