// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomAutoMapperExtension
// Guid:4960fc12-c08b-426e-abf1-efaf35db4d9f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:57:37
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.ViewModels.Users;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomAutoMapperExtension
/// </summary>
public static class CustomAutoMapperExtension
{
    /// <summary>
    /// AutoMapper扩展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services, IConfiguration config)
    {
        // 创建具体的映射对象
        services.AddAutoMapper(mapper =>
        {
            // User
            mapper.CreateMap<UserAuthority, UserAuthorityDto>().ReverseMap();
        });
        return services;
    }
}