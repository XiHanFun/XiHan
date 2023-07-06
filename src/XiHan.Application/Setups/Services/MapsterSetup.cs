#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AutoMapperSetup
// Guid:4960fc12-c08b-426e-abf1-efaf35db4d9f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-30 上午 02:57:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Application.Common.Mapster;

namespace XiHan.Application.Setups.Services;

/// <summary>
/// MapsterSetup
/// </summary>
public static class MapsterSetup
{
    /// <summary>
    /// AutoMapper 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddMapsterSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 创建具体的映射对象
        services.AddSingleton(MapsterAdaptConfig.InitMapperConfig());
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}