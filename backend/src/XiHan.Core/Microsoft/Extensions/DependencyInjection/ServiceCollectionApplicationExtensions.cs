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
using XiHan.Core.Modularity.Abstracts;

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
    public static IXiHanApplicationWithExternalServiceProvider AddApplication<TStartupModule>(
       [NotNull] this IServiceCollection services,
       Action<XiHanApplicationCreationOptions>? optionsAction = null)
       where TStartupModule : IXiHanModule
    {
        return XiHanApplicationFactory.Create<TStartupModule>(services, optionsAction);
    }

    /// <summary>
    /// 添加应用
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static IXiHanApplicationWithExternalServiceProvider AddApplication(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
    {
        return XiHanApplicationFactory.Create(startupModuleType, services, optionsAction);
    }

    /// <summary>
    /// 添加应用，异步
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IXiHanApplicationWithExternalServiceProvider> AddApplicationAsync<TStartupModule>(
        [NotNull] this IServiceCollection services,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
        where TStartupModule : IXiHanModule
    {
        return await XiHanApplicationFactory.CreateAsync<TStartupModule>(services, optionsAction);
    }

    /// <summary>
    /// 添加应用，异步
    /// </summary>
    /// <param name="services"></param>
    /// <param name="startupModuleType"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    public static async Task<IXiHanApplicationWithExternalServiceProvider> AddApplicationAsync(
        [NotNull] this IServiceCollection services,
        [NotNull] Type startupModuleType,
        Action<XiHanApplicationCreationOptions>? optionsAction = null)
    {
        return await XiHanApplicationFactory.CreateAsync(startupModuleType, services, optionsAction);
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
    public static IXiHanHostEnvironment GetHostEnvironment(this IServiceCollection services)
    {
        return services.GetSingletonInstance<IXiHanHostEnvironment>();
    }
}