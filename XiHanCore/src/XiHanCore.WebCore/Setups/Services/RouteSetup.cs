#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RouteSetup
// Guid:110bb71a-7214-4344-8aea-ce3de16c4cae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-30 上午 03:15:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;

namespace XiHanCore.WebCore.Setups.Services;

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
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddRouting(route =>
        {
            route.LowercaseUrls = false;
            route.LowercaseQueryStrings = false;
            // 路由前后加斜杠 /
            route.AppendTrailingSlash = false;
        });
        return services;
    }
}