﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ReflectionHelper
// Guid:e8f234f6-9d3e-4dbc-aee6-d02fbf424954
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 09:28:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;

namespace XiHan.Common.Utilities.Reflections;

/// <summary>
/// 反射拓展帮助类
/// </summary>
public static class ReflectionHelper
{
    #region 程序集

    /// <summary>
    /// 获取所有程序集
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAllAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies().Distinct();
    }

    /// <summary>
    /// 获取符合条件前后缀名称的程序集
    /// </summary>
    /// <param name="prefix">前缀名</param>
    /// <param name="suffix">后缀名</param>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetEffectivePatchAssemblies(string prefix, string suffix)
    {
        return GetAllAssemblies()
            .Where(assembly => assembly.ManifestModule.Name.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            .Where(assembly => assembly.ManifestModule.Name.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase))
            .Distinct();
    }

    /// <summary>
    /// 获取符合条件包含名称的程序集
    /// </summary>
    /// <param name="contain">包含名</param>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetEffectiveCenterAssemblies(string contain)
    {
        return GetAllAssemblies()
            .Where(assembly => assembly.ManifestModule.Name.Contains(contain, StringComparison.InvariantCultureIgnoreCase))
            .Distinct();
    }

    /// <summary>
    /// 获取曦寒程序集
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetXiHanAssemblies()
    {
        return GetEffectivePatchAssemblies("XiHan", "dll");
    }

    /// <summary>
    /// 获取应用服务程序集
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAppServiceAssemblies()
    {
        return GetEffectiveCenterAssemblies("AppService");
    }

    #endregion

    #region 程序集类

    /// <summary>
    /// 获取所有程序集类
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllTypes()
    {
        return GetAllAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Distinct();
    }

    /// <summary>
    /// 获取曦寒程序集类
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Type> GetXiHanTypes()
    {
        return GetXiHanAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Distinct();
    }

    #endregion

    #region 获取包含有某属性的类

    /// <summary>
    /// 获取包含有某属性的类
    /// 第一种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetContainsAttributeTypes<TAttribute>() where TAttribute : Attribute
    {
        return GetXiHanTypes()
            .Where(e => e.CustomAttributes.Any(g => g.AttributeType == typeof(TAttribute)));
    }

    /// <summary>
    /// 获取包含有某属性的类
    /// 第二种实现
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetContainsAttributeTypes(Attribute attribute)
    {
        return GetXiHanTypes()
            .Where(e => e.CustomAttributes.Any(g => g.AttributeType == attribute.GetType()));
    }

    #endregion

    #region 获取不包含有某属性的类

    /// <summary>
    /// 获取不包含有某属性的类
    /// 第一种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetFilterAttributeTypes<TAttribute>() where TAttribute : Attribute
    {
        return GetXiHanTypes()
            .Where(e => e.CustomAttributes.All(g => g.AttributeType != typeof(TAttribute)));
    }

    /// <summary>
    /// 获取包含有某属性的类
    /// 第二种实现
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetFilterAttributeTypes(Attribute attribute)
    {
        return GetXiHanTypes()
            .Where(e => e.CustomAttributes.All(g => g.AttributeType != attribute.GetType()));
    }

    #endregion

    #region 获取某类的子类(非抽象类)

    /// <summary>
    /// 获取某类的子类(非抽象类)
    /// 第一种实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetSubClasses<T>() where T : class
    {
        return GetXiHanTypes()
            .Where(t => t is { IsInterface: false, IsAbstract: false, IsClass: true })
            .Where(t => typeof(T).IsAssignableFrom(t));
    }

    /// <summary>
    /// 获取某类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetSubClasses(Type type)
    {
        return GetXiHanTypes()
            .Where(t => t is { IsInterface: false, IsAbstract: false, IsClass: true })
            .Where(type.IsAssignableFrom);
    }

    /// <summary>
    /// 获取某泛型接口的子类(非抽象类)
    /// </summary>
    /// <param name="interfaceType"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetSubClassesByGenericInterface(Type interfaceType)
    {
        return GetXiHanTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false } && type.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
            .ToList();
    }

    #endregion

    #region 获取继承自某类的包含有某属性的接口、类的子类(非抽象类)

    /// <summary>
    /// 获取继承自某类的包含有某属性的接口、类的子类(非抽象类)
    /// 第一种实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetContainsAttributeSubClasses<T, TAttribute>() where T : class where TAttribute : Attribute
    {
        return GetSubClasses<T>().Intersect(GetContainsAttributeTypes<TAttribute>());
    }

    /// <summary>
    /// 获取继承自某类的包含有某属性的接口、类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetContainsAttributeSubClasses<TAttribute>(Type type) where TAttribute : Attribute
    {
        return GetSubClasses(type).Intersect(GetContainsAttributeTypes<TAttribute>());
    }

    #endregion

    #region 获取继承自某类的不包含有某属性的接口、类的子类(非抽象类)

    /// <summary>
    /// 获取继承自某类的不包含有某属性的接口、类的子类(非抽象类)
    /// 第一种实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetFilterAttributeSubClass<T, TAttribute>() where T : class where TAttribute : Attribute
    {
        return GetSubClasses<T>().Intersect(GetFilterAttributeTypes<TAttribute>());
    }

    /// <summary>
    /// 获取继承自某类的不包含有某属性的接口、类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetFilterAttributeSubClass<TAttribute>(Type type) where TAttribute : Attribute
    {
        return GetSubClasses(type).Intersect(GetFilterAttributeTypes<TAttribute>());
    }

    #endregion

    #region 程序集依赖包

    /// <summary>
    /// 获取程序集依赖包
    /// </summary>
    /// <returns></returns>
    public static List<NuGetPackage> GetNuGetPackages()
    {
        List<NuGetPackage> nugetPackages = [];

        // 获取当前应用程序集
        var assemblies = GetXiHanAssemblies();

        // 查找被引用程序集中的 NuGet 库依赖项
        foreach (var assembly in assemblies)
        {
            var referencedAssemblies = assembly.GetReferencedAssemblies()
                .Where(s => !s.FullName.StartsWith("Microsoft") && !s.FullName.StartsWith("System"))
                .Where(s => !s.FullName.StartsWith("XiHan"));
            foreach (var referencedAssembly in referencedAssemblies)
                // 检查引用的程序集是否来自 NuGet
                if (referencedAssembly.FullName.Contains("Version="))
                {
                    var nuGetPackage = new NuGetPackage
                    {
                        // 获取 NuGet 包的名称和版本号
                        PackageName = referencedAssembly.Name!,
                        PackageVersion = new AssemblyName(referencedAssembly.FullName).Version!.ToString()
                    };

                    // 避免重复添加相同的 NuGet 包标识
                    if (!nugetPackages.Contains(nuGetPackage))
                        nugetPackages.Add(nuGetPackage);
                }
        }
        return nugetPackages;
    }

    #endregion
}

/// <summary>
/// NuGet程序集
/// </summary>
public record NuGetPackage
{
    /// <summary>
    /// 程序集名称
    /// </summary>
    public string PackageName = string.Empty;

    /// <summary>
    /// 程序集版本
    /// </summary>
    public string PackageVersion = string.Empty;
}