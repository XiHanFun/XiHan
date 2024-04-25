#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ExposedServiceExplorer
// Guid:6b32ae7c-1635-48ec-8c58-d3f7b41112b8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 22:33:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.DependencyInjection.Attributes;
using XiHan.Core.DependencyInjection.Providers;
using XiHan.Core.Microsoft.Extensions.DependencyInjection;
using XiHan.Core.System.Collections.Generic.Extensions;

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 暴露服务探测器
/// </summary>
public class ExposedServiceExplorer
{
    /// <summary>
    /// 默认暴露服务特性
    /// </summary>
    private static readonly ExposeServicesAttribute DefaultExposeServicesAttribute =
        new()
        {
            IncludeDefaults = true,
            IncludeSelf = true
        };

    /// <summary>
    /// 获取暴露的服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetExposedServices(Type type)
    {
        var exposedServiceTypesProviders = type
            .GetCustomAttributes(true)
            .OfType<IExposedServiceTypesProvider>()
            .ToList();

        if (exposedServiceTypesProviders.IsNullOrEmpty() && type.GetCustomAttributes(true).OfType<IExposedKeyedServiceTypesProvider>().Any())
        {
            // 如果有任何 IExposedKeyedServiceTypesProvider，但没有 IExposedServiceTypesProvider，我们将不会公开默认服务
            return [];
        }

        return exposedServiceTypesProviders
            .DefaultIfEmpty(DefaultExposeServicesAttribute)
            .SelectMany(p => p.GetExposedServiceTypes(type))
            .Distinct()
            .ToList();
    }

    /// <summary>
    /// 获取暴露的键值服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<ServiceIdentifier> GetExposedKeyedServices(Type type)
    {
        return type
            .GetCustomAttributes(true)
            .OfType<IExposedKeyedServiceTypesProvider>()
            .SelectMany(p => p.GetExposedServiceTypes(type))
            .Distinct()
            .ToList();
    }
}