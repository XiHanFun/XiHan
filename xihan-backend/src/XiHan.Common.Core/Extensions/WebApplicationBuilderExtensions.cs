#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WebApplicationBuilderExtensions
// Guid:46bd0cbe-11b1-418e-a47c-91b939580e9c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/30 6:20:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.Loader;
using XiHan.Common.Core.Modules.Abstracts;

namespace XiHan.Common.Core.Extensions;

/// <summary>
/// Web 应用程序和服务的生成器扩展
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 初始化应用程序
    /// </summary>
    /// <param name="builder"></param>
    /// <remarks>
    /// 每个项目中都可以自己写一些实现了 IModuleInitializer 接口的类，在其中注册自己需要的服务，这样避免所有内容到入口项目中注册
    /// </remarks>
    public static async Task InitAppliation(this WebApplicationBuilder builder)
    {
        var host = builder.Host;

        host.InitHostConfig();

        var configuration = builder.Configuration;

        var environment = builder.Environment;
        var services = builder.Services;
        var logging = builder.Logging;

        services.AddBuilderServices(configuration);

        var app = builder.Build();

        app.UseApps(environment);

        await app.RunAsync();
    }

    /// <summary>
    /// 初始化主机配置
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostBuilder InitHostConfig(this IHostBuilder builder)
    {
        return builder;
    }

    /// <summary>
    /// 添加服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddBuilderServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = AssemblyLoadContext.Default.Assemblies;

        foreach (var assembly in assemblies)
        {
            Console.WriteLine($"[{assembly.FullName}]正在加载……");

            // 获取所有实现了 IModuleInitializer 接口的类1`
            var moduleInitializerTypes = GetTypeImplementedIModuleInitializer(assembly);

            // 获取所有程序集
            foreach (var moduleType in moduleInitializerTypes)
            {
                // 动态创建 moduleType 的实例
                if (Activator.CreateInstance(moduleType) is not IModuleInitializer initializer)
                {
                    throw new ApplicationException($"无法创建[{moduleType}]实例");
                }

                Console.WriteLine($"[{moduleType}]正在初始化……");

                initializer.InitializeServices(services, configuration);

                Console.WriteLine($"[{moduleType}]初始化完成！");
            }

            Console.WriteLine($"[{assembly.FullName}]加载完成！");
        }

        return services;
    }

    /// <summary>
    /// 注册中间件
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="environment"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseApps(this IApplicationBuilder builder, IWebHostEnvironment environment)
    {
        var assemblies = AssemblyLoadContext.Default.Assemblies;

        foreach (var assembly in assemblies)
        {
            Console.WriteLine($"[{assembly.FullName}]正在加载……");

            // 获取所有实现了 IModuleInitializer 接口的类
            var moduleInitializerTypes = GetTypeImplementedIModuleInitializer(assembly);

            // 获取所有程序集
            foreach (var moduleType in moduleInitializerTypes)
            {
                // 动态创建 moduleType 的实例
                if (Activator.CreateInstance(moduleType) is not IModuleInitializer initializer)
                {
                    throw new ApplicationException($"无法创建[{moduleType}]实例");
                }

                Console.WriteLine($"[{moduleType}]正在初始化……");

                initializer.Initialize(builder, environment);

                Console.WriteLine($"[{moduleType}]初始化完成！");
            }

            Console.WriteLine($"[{assembly.FullName}]加载完成！");
        }

        return builder;
    }

    /// <summary>
    /// 获取所有实现了 IModuleInitializer 接口的类
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Type> GetTypeImplementedIModuleInitializer(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(type => type is { IsInterface: false, IsClass: true, IsAbstract: false })
            .Where(type => typeof(IModuleInitializer).IsAssignableFrom(type));
    }
}