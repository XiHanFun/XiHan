#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RouteSetup
// Guid:110bb71a-7214-4344-8aea-ce3de16c4cae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:15:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Extensions.Setups.Services;

/// <summary>
/// RouteSetup
/// </summary>
public static class RouteSetup
{
    /// <summary>
    /// Route 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddRouteSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddRouting(route =>
        {
            route.LowercaseUrls = true;
            route.LowercaseQueryStrings = true;
            // 路由前后加斜杠 /
            route.AppendTrailingSlash = false;
        });
        return services;
    }
}