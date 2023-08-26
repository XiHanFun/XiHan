#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CacheSetup
// Guid:5c45f05d-b77a-4ffa-8975-77aff404eb20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-28 下午 11:29:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Configs;

namespace XiHan.WebCore.Setups.Services;

/// <summary>
/// CacheSetup
/// </summary>
public static class CacheSetup
{
    /// <summary>
    /// 缓存服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddCacheSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 内存缓存(默认开启)
        services.AddMemoryCache();
        services.AddSingleton<IAppCacheService, AppCacheService>();

        // 分布式缓存
        var isEnabledRedisCache = AppSettings.Cache.RedisCache.IsEnabled.GetValue();
        if (isEnabledRedisCache)
        {
            // CSRedis
            var connectionString = AppSettings.Cache.RedisCache.ConnectionString.GetValue();
            var prefix = AppSettings.Cache.RedisCache.Prefix.GetValue();
            var redisStr = $"{connectionString}, prefix = {prefix}";
            var redisClient = new CSRedisClient(redisStr);
            // 用法一：基于 Redis 初始化 IDistributedCache
            services.AddSingleton(redisClient);
            services.AddSingleton<IDistributedCache>(new CSRedisCache(redisClient));
            // 用法二：帮助类直接调用
            RedisHelper.Initialization(redisClient);
            services.AddDistributedMemoryCache();
        }

        return services;
    }
}