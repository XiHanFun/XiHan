// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RouteSetup
// Guid:110bb71a-7214-4344-8aea-ce3de16c4cae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:15:11
// ----------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// RouteSetup
/// </summary>
public static class RouteSetup
{
    /// <summary>
    /// Route服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRouteSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddRouting(route =>
        {
            route.LowercaseUrls = true;
            route.LowercaseQueryStrings = true;
            // 路由前加后加斜杠 /
            route.AppendTrailingSlash = false;
        });
        return services;
    }
}