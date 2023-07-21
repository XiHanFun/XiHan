#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:App
// Guid:6b0d15f4-7ee3-4b90-9f6d-fe08cf27a29c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-16 上午 04:41:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Claims;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Utils.IdGenerator;
using XiHan.Utils.Reflections;

namespace XiHan.Infrastructures.Apps;

/// <summary>
/// 应用全局管理器
/// </summary>
public static class App
{
    /// <summary>
    /// 应用有效程序集
    /// </summary>
    public static IEnumerable<Assembly> Assemblies => ReflectionHelper.GetAssemblies();

    /// <summary>
    /// 有效程序集类型
    /// </summary>
    public static IEnumerable<Type> EffectiveTypes => ReflectionHelper.GetAllTypes();

    /// <summary>
    /// 获取请求上下文
    /// </summary>
    public static HttpContext HttpContextCurrent => AppHttpContextManager.Current;

    /// <summary>
    /// 获取请求上下文用户
    /// </summary>
    public static ClaimsPrincipal User => HttpContextCurrent.User;

    /// <summary>
    /// 获取请求服务容器
    /// </summary>
    public static IServiceProvider ServiceProvider => HttpContextCurrent.RequestServices ?? AppServiceManager.ServiceProvider;

    /// <summary>
    /// 获取请求生命周期服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetService<TService>() where TService : class
    {
        return GetService(typeof(TService)) as TService;
    }

    /// <summary>
    /// 获取请求生命周期服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object? GetService(Type type)
    {
        return ServiceProvider.GetService(type);
    }

    /// <summary>
    /// 获取请求依赖的生命周期服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService? GetRequiredService<TService>() where TService : class
    {
        return GetRequiredService(typeof(TService)) as TService;
    }

    /// <summary>
    /// 获取请求依赖的生命周期服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object? GetRequiredService(Type type)
    {
        return ServiceProvider.GetRequiredService(type);
    }

    /// <summary>
    /// 获取雪花Id
    /// </summary>
    /// <returns></returns>
    public static long GetSnowflakeId()
    {
        return IdHelper.NextId();
    }
}