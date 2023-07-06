#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ResponseCacheSetup
// Guid:fc4b87fd-fb6b-4700-be33-7539ee60d355
// Author:Administrator
// Email:me@zhaifanhua.com
// CreatedTime:2023-07-03 上午 03:05:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructures.Apps.Configs;

namespace XiHan.Application.Setups.Services;

/// <summary>
/// ResponseCacheSetup
/// </summary>
public static class ResponseCacheSetup
{
    /// <summary>
    /// 响应缓存 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddResponseCacheSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 响应缓存
        var isEnabledResponseCache = AppSettings.Cache.ResponseCache.IsEnabled.GetValue();
        if (isEnabledResponseCache) services.AddResponseCaching();

        return services;
    }
}