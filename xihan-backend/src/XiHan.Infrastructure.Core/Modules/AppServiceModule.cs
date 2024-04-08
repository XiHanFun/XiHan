#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppServiceModule
// Guid:099a82a1-7417-4613-80d4-8dd9230c7ac0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/29 4:04:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XiHan.Common.Utilities.Extensions;
using XiHan.Common.Utilities.Reflections;
using XiHan.Infrastructure.Core.Apps.Services;

namespace XiHan.Infrastructure.Core.Modules;

/// <summary>
/// AppServiceModule
/// </summary>
public static class AppServiceModule
{
    /// <summary>
    /// 添加应用服务模块
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddAppServiceModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // 根据 AppServiceAttribute 反射出所有引用的类
        var referencedTypes = ReflectionHelper.GetFilterAttributeTypes<AppServiceAttribute>();

        // 批量注入
        foreach (var type in referencedTypes)
        {
            // 服务周期
            var serviceAttribute = type.GetCustomAttribute<AppServiceAttribute>();
            if (serviceAttribute == null) continue;
            // 如果有值的话，它就是注册服务的类型；如果没有的话，看是否允许从接口中获取服务类型；
            var serviceType = serviceAttribute.ServiceType;

            // 情况1 适用于依赖抽象编程(如果允许，便尝试获取第一个作为服务类型)
            if (serviceType == null && serviceAttribute.IsInterfaceServiceType)
                serviceType = type.GetInterfaces().FirstOrDefault();
            // 情况2 特殊情况下才会指定(如果还没获取到，就把自身的类型作为服务类型)
            serviceType ??= type;

            _ = serviceAttribute.ServiceLifetime switch
            {
                ServiceLifeTimeEnum.Singleton => services.AddSingleton(serviceType, type),
                ServiceLifeTimeEnum.Scoped => services.AddScoped(serviceType, type),
                ServiceLifeTimeEnum.Transient => services.AddTransient(serviceType, type),
                _ => services.AddTransient(serviceType, type)
            };
            var infoMsg = $"服务注册({serviceAttribute.ServiceLifetime.GetEnumDescriptionByKey()})：{serviceType.Name}-{type.Name}";
            infoMsg.WriteLineSuccess();
        }

        return services;
    }
}