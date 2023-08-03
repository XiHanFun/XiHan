#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:MemoryCacheSetup
// Guid:1cab8fc9-5aa2-495d-8811-5b36401460cc
// Author:Administrator
// Email:me@zhaifanhua.com
// CreatedTime:2023-07-03 上午 02:57:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Infrastructures.Apps.Caches;

namespace XiHan.WebCore.Setups.Services;

/// <summary>
/// MemoryCacheSetup
/// </summary>
public static class MemoryCacheSetup
{
    /// <summary>
    /// 内存缓存 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddMemoryCacheSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 内存缓存(默认开启)
        services.AddMemoryCache();
        services.AddSingleton<IAppCacheService, AppCacheService>();

        return services;
    }
}