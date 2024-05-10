#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceCollectionDynamicOptionsManagerExtensions
// Guid:5cf67d00-0b70-4162-9008-b9a2e49dc654
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-28 上午 10:33:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using XiHan.Framework.Core.Options;

namespace XiHan.Framework.Core.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 服务集合动态选项管理器扩展方法
/// </summary>
public static class ServiceCollectionDynamicOptionsManagerExtensions
{
    /// <summary>
    /// 添加动态选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TManager"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddXiHanDynamicOptions<TOptions, TManager>(this IServiceCollection services)
        where TOptions : class
        where TManager : XiHanDynamicOptionsManager<TOptions>
    {
        services.Replace(ServiceDescriptor.Scoped(typeof(IOptions<TOptions>), typeof(TManager)));
        services.Replace(ServiceDescriptor.Scoped(typeof(IOptionsSnapshot<TOptions>), typeof(TManager)));

        return services;
    }
}