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

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.Environments;
using XiHan.Infrastructures.Apps.HttpContexts;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Utils.Reflections;
using Yitter.IdGenerator;

namespace XiHan.Infrastructures.Apps;

/// <summary>
/// 应用全局管理器
/// </summary>
public static class App
{
    /// <summary>
    /// 有效程序集
    /// </summary>
    public static IEnumerable<Assembly> EffectiveAssemblies => ReflectionHelper.GetAllEffectiveAssemblies();

    /// <summary>
    /// 有效程序集类型
    /// </summary>
    public static IEnumerable<Type> EffectiveTypes => ReflectionHelper.GetAllEffectiveTypes();

    /// <summary>
    /// 全局宿主环境
    /// </summary>
    public static IWebHostEnvironment WebHostEnvironment => AppEnvironmentProvider.WebHostEnvironment;

    /// <summary>
    /// 全局请求服务容器
    /// </summary>
    public static IServiceProvider ServiceProvider => HttpContextCurrent?.RequestServices ?? AppServiceProvider.ServiceProvider;

    /// <summary>
    /// 全局配置构建器
    /// </summary>
    public static IConfiguration Configuration => AppConfigProvider.ConfigurationRoot;

    /// <summary>
    /// 入口程序集
    /// </summary>
    public static Assembly EntryAssembly => Assembly.GetEntryAssembly()!;

    /// <summary>
    /// 全局请求上下文
    /// </summary>
    public static HttpContext? HttpContextCurrent => AppHttpContextProvider.HttpContextCurrent;

    /// <summary>
    /// 上传根路径
    /// </summary>
    public static string WebRootPath => WebHostEnvironment.WebRootPath;

    /// <summary>
    /// 上传根路径
    /// </summary>
    public static string RootUploadPath => Path.Combine(WebRootPath, "Uploads");

    /// <summary>
    /// 导入根路径
    /// </summary>
    public static string RootImportPath => Path.Combine(WebRootPath, "Imports");

    /// <summary>
    /// 模板根路径
    /// </summary>
    public static string RootTemplatePath => Path.Combine(WebRootPath, "Templates");

    /// <summary>
    /// 导出根路径
    /// </summary>
    public static string RootExportPath => Path.Combine(WebRootPath, "Exports");

    /// <summary>
    /// 获取请求生命周期服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetService<TService>() where TService : class
    {
        var service = GetService(typeof(TService)) as TService;
        return service!;
    }

    /// <summary>
    /// 获取请求生命周期服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object GetService(Type type)
    {
        var service = ServiceProvider.GetService(type);
        return service!;
    }

    /// <summary>
    /// 获取请求依赖的生命周期服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetRequiredService<TService>() where TService : class
    {
        var service = GetRequiredService(typeof(TService)) as TService;
        return service!;
    }

    /// <summary>
    /// 获取请求依赖的生命周期服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object GetRequiredService(Type type)
    {
        var service = ServiceProvider.GetRequiredService(type);
        return service;
    }

    /// <summary>
    /// 获取雪花Id
    /// </summary>
    /// <returns></returns>
    public static long GetSnowflakeId()
    {
        return YitIdHelper.NextId();
    }
}