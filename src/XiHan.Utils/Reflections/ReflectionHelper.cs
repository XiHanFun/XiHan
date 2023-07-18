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
using XiHan.Utils.Extensions;

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
    public static List<Assembly> GetAssemblies(string prefix = "XiHan", string suffix = "dll")
    {
        List<Assembly> result = new();

        var currentDomain = AppDomain.CurrentDomain.BaseDirectory;
        var rootDirectory = new DirectoryInfo(currentDomain);
        var files = rootDirectory.GetFiles().ToList();
        var dlls = files.Where(e => e.Name.ToLowerInvariant().Contains($"{prefix}.".ToLowerInvariant()) &&
                                    e.Name.ToLowerInvariant().Contains($".{suffix}".ToLowerInvariant()))
                        .Select(e => e.FullName).ToList();

        dlls.ForEach(dll => result.Add(Assembly.LoadFrom(dll)));

        return result;
    }

    /// <summary>
    /// 获取所有的Type
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllTypes()
    {
        List<Type> types = new();

        var assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            // 取并集去重
            types = types.Union(assembly.GetTypes().ToList()).ToList();
        });

        return types;
    }

    /// <summary>
    /// 获取包含有某属性的 Type
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetContainsAttributeTypes<TAttribute>() where TAttribute : Attribute
    {
        List<Type> types = new();

        var assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            // 取并集去重
            types = types.Union(assembly.GetTypes()
                .Where(e => e.CustomAttributes.Any(g => g.AttributeType == typeof(TAttribute)))
                .ToList())
            .ToList();
        });

        return types;
    }

    /// <summary>
    /// 获取继承自某 Type 的包含有某属性的接口、类的子类(非抽象类)
    /// 第一种实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static List<Type> GetContainsAttributeSubClass<T, TAttribute>() where T : class where TAttribute : Attribute
    {
        return GetContainsAttributeTypes<TAttribute>()
            .Where(t => t is { IsAbstract: false, IsClass: true })
            .Where(t => typeof(T).IsAssignableFrom(t))
            .ToList();
    }

    /// <summary>
    /// 获取继承自某 Type 的包含有某属性的接口、类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetContainsAttributeSubClass<TAttribute>(Type type) where TAttribute : Attribute
    {
        return GetContainsAttributeTypes<TAttribute>()
            .Where(t => t is { IsAbstract: false, IsClass: true })
            .Where(t => type.IsAssignableFrom(t))
            .ToList();
    }

    /// <summary>
    /// 获取不包含有某属性的 Type
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetFilterAttributeTypes<TAttribute>() where TAttribute : Attribute
    {
        List<Type> types = new();

        var assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            // 取并集去重
            types = types.Union(assembly.GetTypes()
                .Where(e => e.CustomAttributes.All(g => g.AttributeType != typeof(TAttribute)))
                .ToList())
            .ToList();
        });

        return types;
    }

    /// <summary>
    /// 获取继承自某 Type 的过滤有某属性的接口、类的子类(非抽象类)
    /// 第一种实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static List<Type> GetFilterAttributeSubClass<T, TAttribute>() where T : class where TAttribute : Attribute
    {
        return GetFilterAttributeTypes<TAttribute>()
            .Where(t => t is { IsAbstract: false, IsClass: true })
            .Where(t => typeof(T).IsAssignableFrom(t))
            .ToList();
    }

    /// <summary>
    /// 获取继承自某 Type 的过滤有某属性的接口、类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetFilterAttributeSubClass<TAttribute>(Type type) where TAttribute : Attribute
    {
        return GetFilterAttributeTypes<TAttribute>()
            .Where(t => t is { IsAbstract: false, IsClass: true })
            .Where(t => type.IsAssignableFrom(t))
            .ToList();
    }

    /// <summary>
    /// 获取 Type 的非抽象子类(无视属性)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetSubClass(Type type)
    {
        return GetAllTypes()
            .Where(type.IsAssignableFrom)
            .Where(t => t is { IsAbstract: false, IsClass: true })
            .ToList();
    }

    /// <summary>
    /// 对象转换成字典 过滤某特性
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static List<Dictionary<string, dynamic>> FiltrationProp<TAttribute>(object? obj) where TAttribute : Attribute
    {
        var result = new List<Dictionary<string, dynamic>>();
        if (obj == null) return result;
        if (obj is not IEnumerable<dynamic> objDynamics) return result;
        var objDynamicList = objDynamics.ToList();
        objDynamicList.ForEach(objDynamic =>
        {
            // 找到所有的没有此特性、或有此特性但忽略字段的属性
            var item = (objDynamic as object).GetType().GetProperties()
                .Where(prop => !prop.HasAttribute<TAttribute>() || (prop.HasAttribute<TAttribute>() &&
                               !(Attribute.GetCustomAttribute(prop, typeof(TAttribute)) as TAttribute)!.GetPropertyValue<TAttribute, bool>("IsIgnore")))
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(objDynamic, null));
            result.Add(item);
        });
        return result;
    }

    /// <summary>
    /// 获取表的列
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static List<string> GetTableColumns(string tableName)
    {
        List<string> result = new();

        var assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            var classType = assembly.GetTypes().FirstOrDefault(a => a.Name == tableName);
            if (classType != null) result = classType.GetProperties().Where(a => a.Name.Contains("Target")).Select(a => a.Name).ToList();
        });

        return result;
    }
}