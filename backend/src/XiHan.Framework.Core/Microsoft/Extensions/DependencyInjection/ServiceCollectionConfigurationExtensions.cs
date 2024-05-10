#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceCollectionConfigurationExtensions
// Guid:39ca11e4-43fc-4e57-a0b3-82834ac9b157
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 21:57:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using XiHan.Framework.Core.Application;

namespace XiHan.Framework.Core.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 服务集合配置扩展
/// </summary>
public static class ServiceCollectionConfigurationExtensions
{
    /// <summary>
    /// 替换配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection ReplaceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Replace(ServiceDescriptor.Singleton<IConfiguration>(configuration));
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="XiHanException"></exception>
    [NotNull]
    public static IConfiguration GetConfiguration(this IServiceCollection services)
    {
        return services.GetConfigurationOrNull() ??
               throw new XiHanException($"在服务集合中找不到{typeof(IConfiguration).AssemblyQualifiedName}的实现。");
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    [CanBeNull]
    public static IConfiguration? GetConfigurationOrNull(this IServiceCollection services)
    {
        var hostBuilderContext = services.GetSingletonInstanceOrNull<HostBuilderContext>();
        if (hostBuilderContext?.Configuration != null)
        {
            return hostBuilderContext.Configuration as IConfigurationRoot;
        }

        return services.GetSingletonInstanceOrNull<IConfiguration>();
    }
}