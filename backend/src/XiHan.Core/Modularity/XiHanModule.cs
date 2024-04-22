#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:XiHanModule
// Guid:2909728d-05d0-4da7-9647-c08f6da40f5e
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:34:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JetBrains.Annotations;
using System.Reflection;
using XiHan.Core.Exceptions;
using XiHan.Core.Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Modularity.Abstracts;
using XiHan.Core.Modularity.Contexts;

namespace XiHan.Core.Modularity;

/// <summary>
/// 曦寒模块，模块化服务配置基类
/// </summary>
public abstract class XiHanModule : IPreConfigureServices, IXiHanModule, IPostConfigureServices,
    IOnPreApplicationInitialization, IOnApplicationInitialization, IOnPostApplicationInitialization, IOnApplicationShutdown
{
    #region 服务配置

    /// <summary>
    /// 服务配置前，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task PreConfigureServicesAsync(ServiceConfigurationContext context)
    {
        PreConfigureServices(context);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 服务配置前
    /// </summary>
    /// <param name="context"></param>
    public void PreConfigureServices(ServiceConfigurationContext context)
    {
    }

    /// <summary>
    /// 服务配置，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual Task ConfigureServicesAsync(ServiceConfigurationContext context)
    {
        ConfigureServices(context);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 服务配置
    /// </summary>
    /// <param name="context"></param>
    public virtual void ConfigureServices(ServiceConfigurationContext context)
    {
    }

    /// <summary>
    /// 服务配置后，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task PostConfigureServicesAsync(ServiceConfigurationContext context)
    {
        PostConfigureServices(context);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 服务配置后
    /// </summary>
    /// <param name="context"></param>
    public void PostConfigureServices(ServiceConfigurationContext context)
    {
    }

    #endregion

    #region 程序相关

    /// <summary>
    /// 程序初始化前，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnPreApplicationInitializationAsync([NotNull] ApplicationInitializationContext context)
    {
        OnPreApplicationInitialization(context);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 程序初始化前
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnPreApplicationInitialization([NotNull] ApplicationInitializationContext context)
    {
    }

    /// <summary>
    /// 程序初始化，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task OnApplicationInitializationAsync([NotNull] ApplicationInitializationContext context)
    {
        OnApplicationInitialization(context);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 程序初始化
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnApplicationInitialization([NotNull] ApplicationInitializationContext context)
    {
    }

    /// <summary>
    /// 程序初始化后，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnPostApplicationInitializationAsync([NotNull] ApplicationInitializationContext context)
    {
        OnPostApplicationInitialization(context);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 程序初始化后
    /// </summary>
    /// <param name="context"></param>
    public void OnPostApplicationInitialization([NotNull] ApplicationInitializationContext context)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 程序关闭时，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnApplicationShutdownAsync([NotNull] ApplicationShutdownContext context)
    {
        OnApplicationShutdown(context);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 程序关闭时
    /// </summary>
    /// <param name="context"></param>
    public void OnApplicationShutdown([NotNull] ApplicationShutdownContext context)
    {
    }

    #endregion

    private ServiceConfigurationContext? _serviceConfigurationContext;

    /// <summary>
    /// 服务配置上下文
    /// </summary>
    protected internal ServiceConfigurationContext ServiceConfigurationContext
    {
        get
        {
            if (_serviceConfigurationContext == null)
            {
                throw new CustomException($"{nameof(ServiceConfigurationContext)}只能在{nameof(ConfigureServices)}、{nameof(PreConfigureServices)}和{nameof(PostConfigureServices)}方法中使用。");
            }

            return _serviceConfigurationContext;
        }
        internal set => _serviceConfigurationContext = value;
    }

    /// <summary>
    /// 是否为曦寒模块
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsXiHanModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();

        return typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsGenericType &&
            typeof(IXiHanModule).GetTypeInfo().IsAssignableFrom(type);
    }

    /// <summary>
    /// 检测曦寒模块类
    /// </summary>
    /// <param name="moduleType"></param>
    /// <exception cref="ArgumentException"></exception>
    internal static void CheckXiHanModuleType(Type moduleType)
    {
        if (!IsXiHanModule(moduleType))
        {
            throw new ArgumentException("给定的类型不是曦寒模块:" + moduleType.AssemblyQualifiedName);
        }
    }

    /// <summary>
    /// 配置选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="configureOptions"></param>
    protected void Configure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure(configureOptions);
    }

    /// <summary>
    /// 配置选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="name"></param>
    /// <param name="configureOptions"></param>
    protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure(name, configureOptions);
    }

    /// <summary>
    /// 配置选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="configuration"></param>
    protected void Configure<TOptions>(IConfiguration configuration)
       where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure<TOptions>(configuration);
    }

    /// <summary>
    /// 配置选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="configuration"></param>
    /// <param name="configureBinder"></param>
    protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);
    }

    /// <summary>
    /// 配置选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="name"></param>
    /// <param name="configuration"></param>
    protected void Configure<TOptions>(string name, IConfiguration configuration)
       where TOptions : class
    {
        ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);
    }

    /// <summary>
    /// 配置前选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="configureOptions"></param>
    protected void PreConfigure<TOptions>(Action<TOptions> configureOptions)
       where TOptions : class
    {
        ServiceConfigurationContext.Services.PreConfigure(configureOptions);
    }

    /// <summary>
    /// 配置后选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="configureOptions"></param>
    protected void PostConfigure<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.PostConfigure(configureOptions);
    }

    /// <summary>
    /// 配置前所有选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="configureOptions"></param>
    protected void PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
        where TOptions : class
    {
        ServiceConfigurationContext.Services.PostConfigureAll(configureOptions);
    }
}