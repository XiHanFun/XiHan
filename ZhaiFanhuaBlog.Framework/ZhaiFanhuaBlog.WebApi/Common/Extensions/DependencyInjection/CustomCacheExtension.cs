// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomCacheExtension
// Guid:5c45f05d-b77a-4ffa-8975-77aff404eb20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-28 下午 11:29:28
// ----------------------------------------------------------------

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
        if (config.GetValue<bool>("Cache:MemoryCache:IsEnabled"))
        {
            switch (config["Cache:Type"])
            {
                // 内存缓存
                case "MemoryCache":
                    services.AddMemoryCache();
                    break;
                // 分布式缓存
                case "DistributedCache":
                    //services.AddDistributedMemoryCache(options =>
                    //{
                    //    // CSRedisClient实例化的对象注册成全局单例
                    //    var redis = new CSRedis.CSRedisClient(config["Cache:ConnectionString:Redis"]);
                    //    services.AddSingleton(redis);
                    //});
                    break;
                // 响应缓存
                case "ResponseCache":
                    services.AddResponseCaching();
                    break;
                // 默认使用内存缓存
                default:
                    services.AddMemoryCache();
                    break;
            }
        }
        return services;
    }
}