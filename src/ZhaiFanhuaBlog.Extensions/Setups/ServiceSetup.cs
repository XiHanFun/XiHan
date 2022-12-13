#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ServiceSetup
// Guid:2340e05b-ffd7-4a19-84bc-c3f73517b696
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:48:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ZhaiFanhuaBlog.Infrastructure.App.Service;
using ZhaiFanhuaBlog.Infrastructure.Enums;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.IpLocation;
using ZhaiFanhuaBlog.Utils.IpLocation.Ip2region;

namespace ZhaiFanhuaBlog.Extensions.Setups;

/// <summary>
/// ServiceSetup
/// </summary>
public static class ServiceSetup
{
    /// <summary>
    /// 依赖注入服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServiceSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // 注册基础服务
        RegisterBaseService(services);
        // 注册自身服务
        RegisterSelfService(services);

        return services;
    }

    /// <summary>
    /// 注册基础服务
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterBaseService(IServiceCollection services)
    {
        // Ip 查询服务
        services.AddSingleton<ISearcher, Searcher>();
        IpSearchHelper.IpDbPath = Path.Combine(AppContext.BaseDirectory, "configdata", "ip2region.xdb");
    }

    /// <summary>
    /// 注册自身服务
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterSelfService(IServiceCollection services)
    {
        // 所有涉及服务的组件库
        string[] libraries = new string[] { "ZhaiFanhuaBlog.Repositories", "ZhaiFanhuaBlog.Services", "ZhaiFanhuaBlog.Tasks" };
        // 根据程序路径反射出所有引用的程序集
        var referencedTypes = new List<Type>();
        foreach (var library in libraries)
        {
            try
            {
                var assemblyTypes = Assembly.Load(library).GetTypes()
                    .Where(type => type.GetCustomAttribute<AppServiceAttribute>() != null);
                referencedTypes.AddRange(assemblyTypes);
            }
            catch (Exception)
            {
                ConsoleHelper.WriteLineError($"找不到{library}组件库");
            }
        }
        // 批量注入
        foreach (var classType in referencedTypes)
        {
            // 服务周期
            var serviceAttribute = classType.GetCustomAttribute<AppServiceAttribute>();
            if (serviceAttribute != null)
            {
                var interfaceType = serviceAttribute.ServiceType;
                // 判断是否实现了该接口，若是，则注入服务
                if (interfaceType != null && interfaceType.IsAssignableFrom(classType))
                {
                    switch (serviceAttribute.ServiceLifetime)
                    {
                        case LifeTimeEnum.Singleton:
                            services.AddSingleton(interfaceType, classType);
                            break;

                        case LifeTimeEnum.Scoped:
                            services.AddScoped(interfaceType, classType);
                            break;

                        case LifeTimeEnum.Transient:
                            services.AddTransient(interfaceType, classType);
                            break;

                        default:
                            services.AddTransient(interfaceType, classType);
                            break;
                    }
                }
            }
        }
    }
}