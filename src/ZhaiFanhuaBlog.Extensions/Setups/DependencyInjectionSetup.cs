#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DependencyInjectionSetup
// Guid:2340e05b-ffd7-4a19-84bc-c3f73517b696
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:48:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ZhaiFanhuaBlog.Core.Services;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// DependencyInjectionSetup
/// </summary>
public static class DependencyInjectionSetup
{
    /// <summary>
    /// 依赖注入服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDependencyInjectionSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // 全局依赖注入接口
        var baseType = typeof(IDependency);
        // 程序路径
        var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
        // 根据程序路径反射出所有引用的程序集
        var referencedAssemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
        // 找出所有不是全局依赖注入接口的，但直接或间接派生自、直接或间接实现自它的接口和类
        var types = referencedAssemblies
            .SelectMany(a => a.DefinedTypes)
            .Select(type => type.AsType())
            .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
        // 所有接口
        var interfaces = types.Where(x => x.IsInterface).ToArray();
        // 所有类
        var implements = types.Where(x => x.IsClass).ToArray();
        // 批量注入
        foreach (var item in interfaces)
        {
            // 判断是否实现了该接口,实现了就直接注入
            var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x));
            if (type != null)
                services.AddScoped(item, type);
        }
        return services;
    }
}