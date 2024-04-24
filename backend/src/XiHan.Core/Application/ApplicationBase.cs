#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationBase
// Guid:eb838cc8-ff1d-4bd5-ae74-fcdc93e34a00
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 11:38:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using XiHan.Core.Application.Abstracts;
using XiHan.Core.Application.Contexts;
using XiHan.Core.DependencyInjection;
using XiHan.Core.Exceptions;
using XiHan.Core.Internal;
using XiHan.Core.Logging;
using XiHan.Core.Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Modularity.Abstracts;
using XiHan.Core.Modularity.Contexts;
using XiHan.Core.System.Extensions;
using XiHan.Core.Verification;
using IHostEnvironment = XiHan.Core.Application.Abstracts.IHostEnvironment;

namespace XiHan.Core.Application;

/// <summary>
/// 应用基类
/// </summary>
public class ApplicationBase : IApplication
{
    /// <summary>
    /// 应用程序启动(入口)模块的类型
    /// </summary>
    [NotNull]
    public Type StartupModuleType { get; }

    /// <summary>
    /// 所有服务注册的列表
    /// 应用程序初始化后，不能向这个集合添加新的服务
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// 应用程序根服务提供器
    /// 在初始化应用程序之前不能使用
    /// </summary>
    public IServiceProvider ServiceProvider { get; private set; } = default!;

    /// <summary>
    /// 模块
    /// </summary>
    public IReadOnlyList<IModuleDescriptor> Modules { get; }

    /// <summary>
    /// 应用名称
    /// </summary>
    public string? ApplicationName { get; }

    /// <summary>
    /// 实例 ID
    /// </summary>
    public string InstanceId { get; } = Guid.NewGuid().ToString();

    private bool _configuredServices;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    internal ApplicationBase(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<ApplicationCreationOptions>? optionsAction)
    {
        CheckHelper.NotNull(startupModuleType, nameof(startupModuleType));
        CheckHelper.NotNull(services, nameof(services));

        StartupModuleType = startupModuleType;
        Services = services;

        services.TryAddObjectAccessor<IServiceProvider>();

        var options = new ApplicationCreationOptions(services);
        optionsAction?.Invoke(options);

        ApplicationName = GetApplicationName(options);

        services.AddSingleton<IApplication>(this);
        services.AddSingleton<IApplicationInfoAccessor>(this);
        services.AddSingleton<IModuleContainer>(this);
        services.AddSingleton<IHostEnvironment>(new HostEnvironment()
        {
            EnvironmentName = options.Environment
        });

        services.AddCoreServices();
        services.AddCoreServices(this, options);

        Modules = LoadModules(services, options);

        if (!options.SkipConfigureServices)
        {
            ConfigureServices();
        }
    }

    #region 关闭应用程序

    /// <summary>
    /// 关闭应用程序和所有模块
    /// </summary>
    public virtual async Task ShutdownAsync()
    {
        using var scope = ServiceProvider.CreateScope();
        await scope.ServiceProvider
            .GetRequiredService<IModuleManager>()
            .ShutdownModulesAsync(new ApplicationShutdownContext(scope.ServiceProvider));
    }

    /// <summary>
    /// 关闭应用程序和所有模块
    /// </summary>
    public virtual void Shutdown()
    {
        using var scope = ServiceProvider.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<IModuleManager>()
            .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
    }

    /// <summary>
    /// 释放
    /// </summary>
    public virtual void Dispose()
    {
        //TODO: 如果之前没有完成，就进行关闭?
    }

    #endregion

    /// <summary>
    /// 设置服务提供器
    /// </summary>
    /// <param name="serviceProvider"></param>
    protected virtual void SetServiceProvider(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>>().Value = ServiceProvider;
    }

    #region 初始化模块

    /// <summary>
    /// 初始化模块，异步
    /// </summary>
    /// <returns></returns>
    protected virtual async Task InitializeModulesAsync()
    {
        using var scope = ServiceProvider.CreateScope();
        WriteInitLogs(scope.ServiceProvider);
        await scope.ServiceProvider
            .GetRequiredService<IModuleManager>()
            .InitializeModulesAsync(new ApplicationInitializationContext(scope.ServiceProvider));
    }

    /// <summary>
    /// 初始化模块
    /// </summary>
    protected virtual void InitializeModules()
    {
        using var scope = ServiceProvider.CreateScope();
        WriteInitLogs(scope.ServiceProvider);
        scope.ServiceProvider
            .GetRequiredService<IModuleManager>()
            .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
    }

    #endregion

    /// <summary>
    /// 记录初始化日志
    /// </summary>
    /// <param name="serviceProvider"></param>
    protected virtual void WriteInitLogs(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetService<ILogger<ApplicationBase>>();
        if (logger == null)
        {
            return;
        }

        var initLogger = serviceProvider.GetRequiredService<IInitLoggerFactory>().Create<ApplicationBase>();

        foreach (var entry in initLogger.Entries)
        {
            logger.Log(entry.LogLevel, entry.EventId, entry.State, entry.Exception, entry.Formatter);
        }

        initLogger.Entries.Clear();
    }

    /// <summary>
    /// 加载模块
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected virtual IReadOnlyList<IModuleDescriptor> LoadModules(IServiceCollection services, ApplicationCreationOptions options)
    {
        return services
            .GetSingletonInstance<IModuleLoader>()
            .LoadModules(services, StartupModuleType, options.PlugInSources);
    }

    #region 配置服务

    /// <summary>
    /// 配置服务
    /// </summary>
    public virtual async Task ConfigureServicesAsync()
    {
        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
        {
            if (module.Instance is Modularity.Module Module)
            {
                Module.ServiceConfigurationContext = context;
            }
        }

        // PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
        {
            try
            {
                await ((IPreConfigureServices)module.Instance).PreConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new InitializationException($"在模块 {module.Type.AssemblyQualifiedName} 的 {nameof(IPreConfigureServices.PreConfigureServicesAsync)} 阶段发生错误。查看内部异常以获取详细信息。", ex);
            }
        }

        var assemblies = new HashSet<Assembly>();

        // ConfigureServices
        foreach (var module in Modules)
        {
            if (module.Instance is Modularity.Module Module)
            {
                if (!Module.SkipAutoServiceRegistration)
                {
                    foreach (var assembly in module.AllAssemblies)
                    {
                        if (!assemblies.Contains(assembly))
                        {
                            Services.AddAssembly(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                }
            }

            try
            {
                await module.Instance.ConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new InitializationException($"在模块 {module.Type.AssemblyQualifiedName} 的 {nameof(IModule.ConfigureServicesAsync)} 阶段发生了一个错误。查看内部异常以获取详细信息。", ex);
            }
        }

        // PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
        {
            try
            {
                await ((IPostConfigureServices)module.Instance).PostConfigureServicesAsync(context);
            }
            catch (Exception ex)
            {
                throw new InitializationException($"在模块 {module.Type.AssemblyQualifiedName} 的 {nameof(IPostConfigureServices.PostConfigureServicesAsync)} 阶段发生了一个错误。查看内部异常以了解详细信息。", ex);
            }
        }

        foreach (var module in Modules)
        {
            if (module.Instance is Modularity.Module Module)
            {
                Module.ServiceConfigurationContext = null!;
            }
        }

        _configuredServices = true;

        TryToSetEnvironment(Services);
    }

    /// <summary>
    /// 检查多个配置服务
    /// </summary>
    /// <exception cref="InitializationException"></exception>
    private void CheckMultipleConfigureServices()
    {
        if (_configuredServices)
        {
            throw new InitializationException("服务已被配置！如果调用 ConfigureServicesAsync 方法，必须在此之前将 ApplicationCreationOptions.SkipConfigureServices 设置为 true");
        }
    }

    /// <summary>
    /// 配置服务
    /// </summary>
    /// <exception cref="InitializationException"></exception>
    public virtual void ConfigureServices()
    {
        CheckMultipleConfigureServices();

        var context = new ServiceConfigurationContext(Services);
        Services.AddSingleton(context);

        foreach (var module in Modules)
        {
            if (module.Instance is Modularity.Module Module)
            {
                Module.ServiceConfigurationContext = context;
            }
        }

        // PreConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPreConfigureServices))
        {
            try
            {
                ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new InitializationException($"在模块  {module.Type.AssemblyQualifiedName}  的  {nameof(IPreConfigureServices.PreConfigureServices)}  阶段发生了一个错误。查看内部异常以获取详细信息。", ex);
            }
        }

        var assemblies = new HashSet<Assembly>();

        // ConfigureServices
        foreach (var module in Modules)
        {
            if (module.Instance is Modularity.Module Module)
            {
                if (!Module.SkipAutoServiceRegistration)
                {
                    foreach (var assembly in module.AllAssemblies)
                    {
                        if (!assemblies.Contains(assembly))
                        {
                            Services.AddAssembly(assembly);
                            assemblies.Add(assembly);
                        }
                    }
                }
            }

            try
            {
                module.Instance.ConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new InitializationException($"在模块 {module.Type.AssemblyQualifiedName} 的 {nameof(IModule.ConfigureServices)} 阶段发生了一个错误。查看内部异常以获取详细信息。", ex);
            }
        }

        // PostConfigureServices
        foreach (var module in Modules.Where(m => m.Instance is IPostConfigureServices))
        {
            try
            {
                ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new InitializationException($"在模块 {module.Type.AssemblyQualifiedName} 的 {nameof(IPostConfigureServices.PostConfigureServices)} 阶段发生了一个错误。查看内部异常以获取详细信息。", ex);
            }
        }

        foreach (var module in Modules)
        {
            if (module.Instance is Modularity.Module Module)
            {
                Module.ServiceConfigurationContext = null!;
            }
        }

        _configuredServices = true;

        TryToSetEnvironment(Services);
    }

    #endregion

    /// <summary>
    /// 获取应用程序名称
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    private static string? GetApplicationName(ApplicationCreationOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.ApplicationName))
        {
            return options.ApplicationName!;
        }

        var configuration = options.Services.GetConfigurationOrNull();
        if (configuration != null)
        {
            var appNameConfig = configuration["ApplicationName"];
            if (!string.IsNullOrWhiteSpace(appNameConfig))
            {
                return appNameConfig!;
            }
        }

        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            return entryAssembly.GetName().Name;
        }

        return null;
    }

    /// <summary>
    /// 尝试设置环境
    /// </summary>
    /// <param name="services"></param>
    private static void TryToSetEnvironment(IServiceCollection services)
    {
        var HostEnvironment = services.GetSingletonInstance<IHostEnvironment>();
        if (HostEnvironment.EnvironmentName.IsNullOrWhiteSpace())
        {
            HostEnvironment.EnvironmentName = Environments.Production;
        }
    }
}