#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IpSearchModule
// Guid:db76a856-33d6-435d-a39b-580b94c8cbac
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 3:21:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;
using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Infrastructure.Core.Modules;

/// <summary>
/// IpSearchModule
/// </summary>
public static class IpSearchModule
{
    /// <summary>
    /// Ip 地址查询服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddIpSearchModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Ip 查询服务
        var dbPath = Path.Combine(AppContext.BaseDirectory, "IpDatabases", "ip2region.xdb");
        services.AddSingleton<ISearcher>(new Searcher(CachePolicy.File, dbPath));

        return services;
    }
}