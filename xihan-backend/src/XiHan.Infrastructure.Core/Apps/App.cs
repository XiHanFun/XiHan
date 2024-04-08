#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:App
// Guid:0f3a2723-43e0-4efe-9218-fe6342f2d177
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:29:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XiHan.Common.Utilities.Reflections;
using XiHan.Infrastructure.Core.Apps.Configs;
using XiHan.Infrastructure.Core.Apps.Environments;
using XiHan.Infrastructure.Core.Apps.Services;
using Yitter.IdGenerator;

namespace XiHan.Infrastructure.Core.Apps;

/// <summary>
/// 应用全局管理器
/// </summary>
public static class App
{
    /// <summary>
    /// 有效程序集
    /// </summary>
    public static IEnumerable<Assembly> EffectiveAssemblies => ReflectionHelper.GetAllAssemblies();

    /// <summary>
    /// 有效程序集类型
    /// </summary>
    public static IEnumerable<Type> EffectiveTypes => ReflectionHelper.GetAllTypes();

    /// <summary>
    /// 全局宿主环境
    /// </summary>
    public static IWebHostEnvironment WebHostEnvironment => AppEnvironmentProvider.WebHostEnvironment;

    /// <summary>
    /// 全局请求服务容器
    /// </summary>
    public static IServiceProvider ServiceProvider => AppServiceProvider.ServiceProvider;

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
    public static HttpContext? HttpContextCurrent => CatchOrDefault(() => ServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext);

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
        return GetService(typeof(TService)) as TService;
    }

    /// <summary>
    /// 获取请求生命周期服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object GetService(Type type)
    {
        return ServiceProvider.GetService(type);
    }

    /// <summary>
    /// 获取请求依赖的生命周期服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static TService GetRequiredService<TService>() where TService : class
    {
        return GetRequiredService(typeof(TService)) as TService;
    }

    /// <summary>
    /// 获取请求依赖的生命周期服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object GetRequiredService(Type type)
    {
        return ServiceProvider.GetRequiredService(type);
    }

    /// <summary>
    /// 处理获取对象异常问题
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="action">获取对象委托</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>T</returns>
    private static T CatchOrDefault<T>(Func<T> action, T defaultValue = null) where T : class
    {
        try
        {
            return action();
        }
        catch
        {
            return defaultValue ?? null;
        }
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