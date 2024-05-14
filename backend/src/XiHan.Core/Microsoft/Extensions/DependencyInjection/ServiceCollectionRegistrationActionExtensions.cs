#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ServiceCollectionRegistrationActionExtensions
// Guid:28304c8d-c4f2-448e-a486-fd98153efcd4
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 05:43:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using XiHan.Core.DependencyInjection;

namespace XiHan.Core.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 服务集合注册操作扩展方法
/// </summary>
public static class ServiceCollectionRegistrationActionExtensions
{
    #region 注册

    /// <summary>
    /// 注册服务时触发
    /// </summary>
    /// <param name="services"></param>
    /// <param name="registrationAction"></param>
    public static void OnRegistered(this IServiceCollection services, Action<IOnServiceRegistredContext> registrationAction)
    {
        GetOrCreateRegistrationActionList(services).Add(registrationAction);
    }

    /// <summary>
    /// 获取注册服务时的操作列表
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static ServiceRegistrationActionList GetRegistrationActionList(this IServiceCollection services)
    {
        return GetOrCreateRegistrationActionList(services);
    }

    /// <summary>
    /// 获取或创建注册服务时的操作列表
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static ServiceRegistrationActionList GetOrCreateRegistrationActionList(IServiceCollection services)
    {
        var actionList = services.GetSingletonInstanceOrNull<IObjectAccessor<ServiceRegistrationActionList>>()?.Value;
        if (actionList == null)
        {
            actionList = [];
            services.AddObjectAccessor(actionList);
        }

        return actionList;
    }

    /// <summary>
    /// 禁用拦截器
    /// </summary>
    /// <param name="services"></param>
    public static void DisableClassInterceptors(this IServiceCollection services)
    {
        GetOrCreateRegistrationActionList(services).IsClassInterceptorsDisabled = true;
    }

    /// <summary>
    /// 是否禁用拦截器
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static bool IsClassInterceptorsDisabled(this IServiceCollection services)
    {
        return GetOrCreateRegistrationActionList(services).IsClassInterceptorsDisabled;
    }

    #endregion

    #region 暴露

    /// <summary>
    /// 暴露服务时触发
    /// </summary>
    /// <param name="services"></param>
    /// <param name="exposeAction"></param>
    public static void OnExposing(this IServiceCollection services, Action<IOnServiceExposingContext> exposeAction)
    {
        GetOrCreateExposingList(services).Add(exposeAction);
    }

    /// <summary>
    /// 获取暴露服务时的操作列表
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static ServiceExposingActionList GetExposingActionList(this IServiceCollection services)
    {
        return GetOrCreateExposingList(services);
    }

    /// <summary>
    /// 获取或创建暴露服务时的操作列表
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static ServiceExposingActionList GetOrCreateExposingList(IServiceCollection services)
    {
        var actionList = services.GetSingletonInstanceOrNull<IObjectAccessor<ServiceExposingActionList>>()?.Value;
        if (actionList == null)
        {
            actionList = [];
            services.AddObjectAccessor(actionList);
        }

        return actionList;
    }

    #endregion
}