#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HttpSetup
// Guid:2753f2f6-5e39-4e46-b3fa-ff80af47a49f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-05 上午 04:06:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Utils.Https;

namespace XiHan.Extensions.Setups.Services;

/// <summary>
/// HttpSetup
/// </summary>
public static class HttpSetup
{
    /// <summary>
    /// Http 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddHttpContextSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 注入 Http 请求
        services.AddHttpClient();
        // 注入 Http 相关实例
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IHttpHelper, HttpHelper>();

        return services;
    }
}