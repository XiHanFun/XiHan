#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ReflectionHelper
// Guid:e8f234f6-9d3e-4dbc-aee6-d02fbf424954
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-11 下午 09:28:46
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
        DirectoryInfo rootDirectory = new(currentDomain);
        var files = rootDirectory.GetFiles().ToList();
        var dlls = files.Where(e => e.Name.ToLowerInvariant().Contains($"{prefix}.".ToLowerInvariant()) &&
                                    e.Name.ToLowerInvariant().Contains($".{suffix}".ToLowerInvariant()))
            .Select(e => e.FullName).ToList();

        dlls.ForEach(dll => { result.Add(Assembly.LoadFrom(dll)); });

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
        assemblies.ForEach(assembly => { types = types.Union(assembly.GetTypes().ToList()).ToList(); });

        return types;
    }

    /// <summary>
    /// 过滤有 XX 属性的
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IEnumerable<Type> GetTypes<TAttribute>() where TAttribute : Attribute
    {
        List<Type> types = new();

        var assemblies = GetAssemblies();
        assemblies.ForEach(assembly =>
        {
            types = types.Union(assembly.GetTypes()
                .Where(e => e.CustomAttributes.All(g => g.AttributeType != typeof(TAttribute))).ToList()).ToList();
        });

        return types;
    }

    /// <summary>
    /// 获取 XX 接口、类的子类（非抽象类）
    /// 第一种实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static List<Type> GetSubClass<T, TAttribute>() where T : class where TAttribute : Attribute
    {
        return GetTypes<TAttribute>().Where(t => typeof(T).IsAssignableFrom(t))
            .Where(t => t is { IsAbstract: false, IsClass: true }).ToList();
    }

    /// <summary>
    /// 获取 XX 接口、类的子类（非抽象类）
    /// 第二种实现
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetSubClass<TAttribute>(Type type) where TAttribute : Attribute
    {
        return GetTypes<TAttribute>().Where(t => type.IsAssignableFrom(t))
            .Where(t => t is { IsAbstract: false, IsClass: true }).ToList();
    }

    /// <summary>
    /// 获取 type 的非抽象子类（无视属性）
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Type> GetSubClass(Type type)
    {
        return GetAllTypes().Where(type.IsAssignableFrom).Where(t => t is { IsAbstract: false, IsClass: true })
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
            // 找到所有【没有此特性】或【有此特性但忽略字段】的属性
            var item = (objDynamic as object).GetType().GetProperties()
                .Where(prop => !prop.HasAttribute<TAttribute>()
                               || (prop.HasAttribute<TAttribute>() &&
                                   !(Attribute.GetCustomAttribute(prop, typeof(TAttribute)) as TAttribute)!
                                       .GetPropertyValue<TAttribute, bool>("IsIgnore")))
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
        GetAssemblies().ForEach(assembly =>
        {
            var classType = assembly.GetTypes().FirstOrDefault(a => a.Name == tableName);
            if (classType != null)
                result = classType.GetProperties().Where(a => a.Name.Contains("Target"))
                    .Select(a => a.Name.FirstToLower()).ToList();
        });
        return result;
    }
}