#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InternalServiceCollectionExtensions
// Guid:a3faa3b7-8f04-4d98-a717-340f23cdd428
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 02:33:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using XiHan.Core.Application;
using XiHan.Core.Application.Abstracts;
using XiHan.Core.DependencyInjection;
using XiHan.Core.Logging;
using XiHan.Core.Microsoft.Extensions.Configuration;
using XiHan.Core.Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Modularity;
using XiHan.Core.Reflection;
using XiHan.Core.Reflection.Abstracts;
using XiHan.Core.SimpleStateChecking;
using XiHan.Core.SimpleStateChecking.Abstracts;

namespace XiHan.Core.Internal;

/// <summary>
/// 集成服务集合扩展方法
/// </summary>
internal static class InternalServiceCollectionExtensions
{
    /// <summary>
    /// 添加核心服务
    /// </summary>
    /// <param name="services"></param>
    internal static void AddCoreServices(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddLogging();
        services.AddLocalization();
    }

    /// <summary>
    /// 添加核心服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="Application"></param>
    /// <param name="applicationCreationOptions"></param>
    internal static void AddCoreServices(this IServiceCollection services,
        IXiHanApplication Application,
        XiHanApplicationCreationOptions applicationCreationOptions)
    {
        var moduleLoader = new ModuleLoader();
        var assemblyFinder = new AssemblyFinder(Application);
        var typeFinder = new TypeFinder(assemblyFinder);

        if (!services.IsAdded<IConfiguration>())
        {
            services.ReplaceConfiguration(ConfigurationHelper.BuildConfiguration(applicationCreationOptions.Configuration));
        }

        services.TryAddSingleton<IModuleLoader>(moduleLoader);
        services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
        services.TryAddSingleton<ITypeFinder>(typeFinder);
        services.TryAddSingleton<IInitLoggerFactory>(new DefaultInitLoggerFactory());
        // 属性或字段自动注入服务
        services.AddSingleton<AutowiredServiceHandler>();

        services.AddAssemblyOf<IXiHanApplication>();

        services.AddTransient(typeof(ISimpleStateCheckerManager<>), typeof(SimpleStateCheckerManager<>));

        services.Configure<XiHanModuleLifecycleOptions>(options =>
        {
            options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
            options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
        });
    }
}