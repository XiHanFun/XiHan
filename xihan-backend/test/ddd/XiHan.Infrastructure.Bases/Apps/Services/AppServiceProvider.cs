#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppServiceProvider
// Guid:bded17a9-b219-467a-b2e8-f8e38a454a04
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-24 上午 01:57:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XiHan.Common.Shared;
using XiHan.Common.Shared.Enums;
using XiHan.Common.Utilities.Extensions;
using XiHan.Infrastructure.Bases.Apps.Services;
using Yitter.IdGenerator;

namespace XiHan.Infrastructures.Bases.Apps.Services;

/// <summary>
/// 全局服务供应器
/// </summary>
public static class AppServiceProvider
{
    /// <summary>
    /// 全局应用服务容器
    /// </summary>
    public static IServiceProvider ServiceProvider { get; set; } = null!;

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterService(IServiceCollection services)
    {
        RegisterBaseService(services);
        RegisterAppService(services);
    }

    /// <summary>
    /// 注册基础服务
    /// </summary>
    /// <param name="services"></param>
    private static void RegisterBaseService(IServiceCollection services)
    {
        // 属性或字段自动注入服务
        _ = services.AddSingleton<AutowiredServiceHandler>();
        // Ip 查询服务
        var dbPath = Path.Combine(AppContext.BaseDirectory, "IpDatabases", "ip2region.xdb");
        _ = services.AddSingleton<ISearcher>(new Searcher(CachePolicy.File, dbPath));
        // 雪花 Id 生成服务
        YitIdHelper.SetIdGenerator(new IdGeneratorOptions()
        {
            WorkerId = 1,
            WorkerIdBitLength = 1,
            SeqBitLength = 6
        });
    }

    /// <summary>
    /// 注册应用服务
    /// </summary>
    /// <remarks>由此启发：<see href="https://www.cnblogs.com/loogn/p/10566510.html"/></remarks>
    /// <param name="services"></param>
    private static void RegisterAppService(IServiceCollection services)
    {
        // 所有涉及服务的组件库
        const string appServiceName = "AppService";

        // 获取所有引用的程序集
        var libraries = Assembly.GetEntryAssembly()?.GetReferencedAssemblies()
            .Where(assembly => assembly.Name!.Contains(appServiceName))
            .Select(assembly => assembly.Name!)
            .ToList();

        if (libraries != null)
        {
            // 根据程序路径反射出所有引用的程序集
            List<Type> referencedTypes = [];
            foreach (var library in libraries)
            {
                try
                {
                    IEnumerable<Type> assemblyTypes = Assembly.Load(library).GetTypes().Where(type => type.GetCustomAttribute<AppServiceAttribute>() != null);
                    referencedTypes.AddRange(assemblyTypes);
                }
                catch (Exception)
                {
                    var errorMsg = $"找不到【{library}】组件库！";
                    errorMsg.WriteLineError();
                }
            }

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
                var infoMsg = $"服务注册({serviceAttribute.ServiceLifetime.GetEnumDescriptionByKey()})：【{serviceType.Name}-{type.Name}】";
                infoMsg.WriteLineSuccess();
            }
        }
    }
}