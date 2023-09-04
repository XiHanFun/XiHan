#region <<版权版本注释>>

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

namespace XiHan.Utils.Reflections;

/// <summary>
/// 反射拓展帮助类
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// 获取所有符合条件的程序集
    /// </summary>
    /// <param name="prefix">前缀名</param>
    /// <param name="suffix">后缀名</param>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAllEffectiveAssemblies(string prefix = "XiHan", string suffix = "dll")
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        IEnumerable<Assembly> filteredAssemblies = assemblies
            .Where(assembly => assembly.ManifestModule.Name.ToLowerInvariant().StartsWith(prefix.ToLowerInvariant()))
            .Where(assembly => assembly.ManifestModule.Name.ToLowerInvariant().EndsWith(suffix.ToLowerInvariant()));

        return filteredAssemblies;
    }

    /// <summary>
    /// 获取所有符合条件的的程序集所有类
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllEffectiveTypes()
    {
        return GetAllEffectiveAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Distinct();
    }

    /// <summary>
    /// 获取自身依赖的所有包
    /// </summary>
    /// <returns></returns>
    public static List<string> GetInstalledNuGetPackages()
    {
        List<string> nugetPackages = new();

        // 获取当前应用程序集引用的所有程序集
        IEnumerable<Assembly> assemblies = GetAllEffectiveAssemblies();

        // 查找被引用程序集中的 NuGet 库依赖项
        foreach (var assembly in assemblies)
        {
            IEnumerable<AssemblyName> referencedAssemblies = assembly.GetReferencedAssemblies()
                .Where(s => !s.FullName!.StartsWith("Microsoft"))
                .Where(s => !s.FullName!.StartsWith("System"));
            foreach (var referencedAssembly in referencedAssemblies)
                // 检查引用的程序集是否来自 NuGet
                if (referencedAssembly.FullName.Contains("Version="))
                {
                    // 获取 NuGet 包的名称和版本号
                    var packageName = referencedAssembly.Name!;
                    var packageVersion = new AssemblyName(referencedAssembly.FullName).Version!.ToString();

                    // 拼接成 NuGet 包的标识（名称:版本）
                    var packageIdentifier = $"{packageName}:{packageVersion}";

                    // 避免重复添加相同的 NuGet 包标识
                    if (!nugetPackages.Contains(packageIdentifier)) nugetPackages.Add(packageIdentifier);
                }
        }

        return nugetPackages;
    }

    #region 获取包含有某属性的类

    /// <summary>
    /// 获取包含有某属性的类
    /// 第一种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetContainsAttributeTypes<TAttribute>() where TAttribute : Attribute
    {
        return GetAllEffectiveTypes()
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
        return GetAllEffectiveTypes()
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
        return GetAllEffectiveTypes()
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
        return GetAllEffectiveTypes()
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
        return GetAllEffectiveTypes()
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
        return GetAllEffectiveTypes()
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
        List<Type> implementingTypes = new();

        foreach (var type in GetAllEffectiveTypes())
            if (type.IsClass && !type.IsAbstract && type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                implementingTypes.Add(type);

        return implementingTypes;
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
    public static IEnumerable<Type> GetContainsAttributeSubClasses<T, TAttribute>()
        where T : class where TAttribute : Attribute
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
    public static IEnumerable<Type> GetFilterAttributeSubClass<T, TAttribute>()
        where T : class where TAttribute : Attribute
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
}