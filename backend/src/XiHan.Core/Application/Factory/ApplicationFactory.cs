#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationFactory
// Guid:13544933-ab7f-4c65-9939-afa01a8dcb98
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 11:18:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Application.Abstracts;
using XiHan.Core.Modularity.Abstracts;
using XiHan.Core.Options;

namespace XiHan.Core.Application.Factory;

/// <summary>
/// 应用工厂
/// </summary>
public static class ApplicationFactory
{
    #region 创建内部服务应用

    /// <summary>
    /// 创建内部服务应用，异步
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IApplicationWithInternalServiceProvider> CreateAsync<TStartupModule>(
        Action<ApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IModule
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
    /// 创建内部服务应用，异步
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IApplicationWithInternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        Action<ApplicationCreationOptions>? optionsAction = null)
    {
        var app = new ApplicationWithInternalServiceProvider(startupModuleType, options =>
        {
            options.SkipConfigureServices = true;
            optionsAction?.Invoke(options);
        });
        await app.ConfigureServicesAsync();
        return app;
    }

    /// <summary>
    /// 创建内部服务应用
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IApplicationWithInternalServiceProvider Create<TStartupModule>(
        Action<ApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IModule
    {
        return Create(typeof(TStartupModule), optionsAction);
    }

    /// <summary>
    /// 创建内部服务应用
    /// </summary>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IApplicationWithInternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        Action<ApplicationCreationOptions>? optionsAction = null)
    {
        return new ApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
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
    public static async Task<IApplicationWithExternalServiceProvider> CreateAsync<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<ApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IModule
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
    public static async Task<IApplicationWithExternalServiceProvider> CreateAsync(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<ApplicationCreationOptions>? optionsAction = null)
    {
        var app = new ApplicationWithExternalServiceProvider(startupModuleType, services, options =>
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
    public static IApplicationWithExternalServiceProvider Create<TStartupModule>(
        [NotNull] IServiceCollection services,
        Action<ApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IModule
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
    public static IApplicationWithExternalServiceProvider Create(
        [NotNull] Type startupModuleType,
        [NotNull] IServiceCollection services,
        Action<ApplicationCreationOptions>? optionsAction = null)
    {
        return new ApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
    }

    #endregion
}