#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AutoMapperSetup
// Guid:4960fc12-c08b-426e-abf1-efaf35db4d9f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:57:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using ZhaiFanhuaBlog.Extensions.Common.AutoMapper;

namespace ZhaiFanhuaBlog.Extensions.Setups.Services;

/// <summary>
/// AutoMapperSetup
/// </summary>
public static class AutoMapperSetup
{
    /// <summary>
    /// AutoMapper 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 创建具体的映射对象
        services.AddAutoMapper(typeof(AutoMapperConfig));
        // 注册配置
        AutoMapperConfig.RegisterMappings();
        return services;
    }
}