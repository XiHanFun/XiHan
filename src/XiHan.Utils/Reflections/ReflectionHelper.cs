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
    /// 获取所有符合条件的的程序集所有类
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllTypes()
    {
        List<Type> types = new();

        var assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            // 取并集去重
            types = types.Union(assembly.GetTypes()).ToList();
        });

        return types;
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
        return GetAllTypes()
           .Where(e => e.CustomAttributes.Any(g => g.AttributeType == typeof(TAttribute)))
           .ToList();
    }

    /// <summary>
    /// 获取包含有某属性的类
    /// 第二种实现
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetContainsAttributeTypes(Attribute attribute)
    {
        return GetAllTypes()
           .Where(e => e.CustomAttributes.Any(g => g.AttributeType == attribute.GetType()))
           .ToList();
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
        return GetAllTypes()
            .Where(e => e.CustomAttributes.All(g => g.AttributeType != typeof(TAttribute)))
            .ToList();
    }

    /// <summary>
    /// 获取包含有某属性的类
    /// 第二种实现
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetFilterAttributeTypes(Attribute attribute)
    {
        return GetAllTypes()
           .Where(e => e.CustomAttributes.All(g => g.AttributeType != attribute.GetType()))
           .ToList();
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
        return GetAllTypes()
           .Where(t => t is { IsInterface: false, IsAbstract: false, IsClass: true })
           .Where(t => typeof(T).IsAssignableFrom(t))
           .ToList();
    }

    /// <summary>
    /// 获取某类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetSubClasses(Type type)
    {
        return GetAllTypes()
            .Where(t => t is { IsInterface: false, IsAbstract: false, IsClass: true })
            .Where(type.IsAssignableFrom)
            .ToList();
    }

    /// <summary>
    /// 获取某泛型接口的子类(非抽象类)
    /// </summary>
    /// <param name="interfaceType"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetSubClassesByGenericInterface(Type interfaceType)
    {
        List<Type> implementingTypes = new();

        foreach (Type type in GetAllTypes())
        {
            if (type.IsClass && !type.IsAbstract && type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
            {
                implementingTypes.Add(type);
            }
        }

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
    public static List<Type> GetContainsAttributeSubClasses<T, TAttribute>() where T : class where TAttribute : Attribute
    {
        return GetSubClasses<T>().Intersect(GetContainsAttributeTypes<TAttribute>()).ToList();
    }

    /// <summary>
    /// 获取继承自某类的包含有某属性的接口、类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetContainsAttributeSubClasses<TAttribute>(Type type) where TAttribute : Attribute
    {
        return GetSubClasses(type).Intersect(GetContainsAttributeTypes<TAttribute>()).ToList();
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
    public static List<Type> GetFilterAttributeSubClass<T, TAttribute>() where T : class where TAttribute : Attribute
    {
        return GetSubClasses<T>().Intersect(GetFilterAttributeTypes<TAttribute>()).ToList();
    }

    /// <summary>
    /// 获取继承自某类的不包含有某属性的接口、类的子类(非抽象类)
    /// 第二种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetFilterAttributeSubClass<TAttribute>(Type type) where TAttribute : Attribute
    {
        return GetSubClasses(type).Intersect(GetFilterAttributeTypes<TAttribute>()).ToList();
    }

    #endregion

    /// <summary>
    /// 对象转换成字典(过滤某特性)
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
}