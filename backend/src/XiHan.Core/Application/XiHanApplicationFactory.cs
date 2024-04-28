#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:XiHanApplicationFactory
// Guid:13544933-ab7f-4c65-9939-afa01a8dcb98
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 11:18:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Application.Abstracts;
using XiHan.Core.Modularity;

namespace XiHan.Core.Application;

/// <summary>
/// 曦寒应用工厂
/// </summary>
public static class XiHanApplicationFactory
{
    #region 创建集成服务应用

    /// <summary>
    /// 创建集成服务应用，异步
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IXiHanApplicationWithInternalServiceProvider> CreateAsync<TStartupModule>(
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IXiHanModule
    {
        var app = Create(typeof(TStartupModule), options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    /// <summary>
    /// 创建集成服务应用，异步
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IXiHanApplicationWithInternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
    {
        var app = new XiHanApplicationWithInternalServiceProvider(startupModuleType, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    /// <summary>
    /// 创建集成服务应用
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IXiHanApplicationWithInternalServiceProvider Create<TStartupModule>(
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IXiHanModule
    {
        return Create(typeof(TStartupModule), optionsAction);
    }

    /// <summary>
    /// 创建集成服务应用
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IXiHanApplicationWithInternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
    {
        return new XiHanApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
    }

    #endregion

    #region 创建外部服务应用

    /// <summary>
    /// 创建外部服务应用，异步
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IXiHanApplicationWithExternalServiceProvider> CreateAsync<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IXiHanModule
    {
        var app = Create(typeof(TStartupModule), services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    /// <summary>
    /// 创建外部服务应用，异步
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IXiHanApplicationWithExternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
    {
        var app = new XiHanApplicationWithExternalServiceProvider(startupModuleType, services, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    /// <summary>
    /// 创建外部服务应用
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IXiHanApplicationWithExternalServiceProvider Create<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IXiHanModule
    {
        return Create(typeof(TStartupModule), services, optionsAction);
    }

    /// <summary>
    /// 创建外部服务应用
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IXiHanApplicationWithExternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
    {
        return new XiHanApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
    }

    #endregion
}