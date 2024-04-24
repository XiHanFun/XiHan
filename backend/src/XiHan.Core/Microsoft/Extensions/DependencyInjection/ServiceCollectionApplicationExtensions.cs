#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceCollectionApplicationExtensions
// Guid:ed4f3f32-5703-4841-a84b-33f1c8972bf2
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 11:09:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Core.Application;
using XiHan.Core.Application.Abstracts;
using XiHan.Core.Application.Factory;
using XiHan.Core.Modularity.Abstracts;
using IHostEnvironment = XiHan.Core.Application.Abstracts.IHostEnvironment;

namespace XiHan.Core.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 服务集合应用扩展方法
/// </summary>
public static class ServiceCollectionApplicationExtensions
{
    /// <summary>
    /// 添加应用
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IApplicationWithExternalServiceProvider AddApplication<TStartupModule>(
       [NotNull] this IServiceCollection services,
       Action<ApplicationCreationOptions>? optionsAction = null)
       where TStartupModule : IModule
    {
        return ApplicationFactory.Create<TStartupModule>(services, optionsAction);
    }

    /// <summary>
    /// 添加应用
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IApplicationWithExternalServiceProvider AddApplication(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<ApplicationCreationOptions>? optionsAction = null)
    {
        return ApplicationFactory.Create(startupModuleType, services, optionsAction);
    }

    /// <summary>
    /// 添加应用，异步
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IApplicationWithExternalServiceProvider> AddApplicationAsync<TStartupModule>(
        [NotNull] this IServiceCollection services,
        Action<ApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IModule
    {
        return await ApplicationFactory.CreateAsync<TStartupModule>(services, optionsAction);
    }

    /// <summary>
    /// 添加应用，异步
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IApplicationWithExternalServiceProvider> AddApplicationAsync(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<ApplicationCreationOptions>? optionsAction = null)
    {
        return await ApplicationFactory.CreateAsync(startupModuleType, services, optionsAction);
    }

    /// <summary>
    /// 获取应用名称
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static string? GetApplicationName(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().ApplicationName;
    }

    /// <summary>
    /// 获取应用唯一标识
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    [NotNull]
    public static string GetApplicationInstanceId(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IApplicationInfoAccessor>().InstanceId;
    }

    /// <summary>
    /// 获取应用环境
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    [NotNull]
    public static IHostEnvironment GetHostEnvironment(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IHostEnvironment>();
    }
}