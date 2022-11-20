#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ObjectPropertyHelper
// Guid:3c22cdf5-2be0-4377-9412-322dcc2ab5e3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-28 下午 06:58:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ZhaiFanhuaBlog.Utils.Object;

/// <summary>
/// 对象属性的处理操作帮助类
/// </summary>
public static class ObjectPropertyHelper
{
    /// <summary>
    /// 获取属性全名并转化为读取方式 例如 AppSettings.Database.Initialization => Database:Initialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static string FullNameOf<T>(this T _, [CallerArgumentExpression("_")] string fullName = "")
    {
        return fullName;
    }

    /// <summary>
    /// 利用反射来判断对象是否包含某个属性
    /// </summary>
    /// <param name="instance">object</param>
    /// <param name="propertyName">需要判断的属性</param>
    /// <returns>是否包含</returns>
    public static bool ContainProperty(this object instance, string propertyName)
    {
        if (instance != null && !string.IsNullOrEmpty(propertyName))
        {
            PropertyInfo? findedPropertyInfo = instance.GetType().GetProperty(propertyName);
            if (findedPropertyInfo != null)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 获取对象属性值
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="tentity"></param>
    /// <param name="propertyname"></param>
    /// <returns></returns>
    public static string GetObjectPropertyValue<TEntity>(TEntity tentity, string propertyname) where TEntity : class
    {
        Type type = typeof(TEntity);
        PropertyInfo? property = type.GetProperty(propertyname);
        if (property != null)
        {
            object? obj = property.GetValue(tentity, null);
            if (obj != null && obj.ToString() != null)
            {
                return obj.ToString()!;
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// 获取属性列表
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public static List<CustomPropertyInfo> GetObjectProperty<TEntity>()
    {
        Type type = typeof(TEntity);
        PropertyInfo[] properties = type.GetProperties();
        List<CustomPropertyInfo> customPropertyInfos = new();
        foreach (PropertyInfo info in properties)
        {
            CustomPropertyInfo customPropertyInfo = new()
            {
                PropertyName = info.Name,
                PropertyType = info.PropertyType,
                PropertyValue = GetObjectPropertyValue(new object(), info.Name)
            };
            customPropertyInfos.Add(customPropertyInfo);
        }
        return customPropertyInfos;
    }

    /// <summary>
    /// 对比两个属性的差异信息
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="val1">对象实例1</param>
    /// <param name="val2">对象实例2</param>
    /// <returns></returns>
    public static List<CustomPropertyVariance> DetailedCompare<T>(this T val1, T val2)
    {
        var propertyInfo = typeof(T).GetType().GetProperties();
        return propertyInfo.Select(variance => new CustomPropertyVariance
        {
            PropertyName = variance.Name,
            //确保不为null
            ValueA = variance.GetValue(val1, null)?.ToString() ?? string.Empty,
            ValueB = variance.GetValue(val2, null)?.ToString() ?? string.Empty
        })
        //调用内置判断
        .Where(variance => !variance.ValueA.Equals(variance.ValueB))
        .ToList();
    }

    /// <summary>
    /// 把两个对象的差异信息转换为Json 格式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="oldVal">对象实例1</param>
    /// <param name="newVal">对象实例2</param>
    /// <param name="specialList">要排除某些特殊属性</param>
    /// <returns></returns>
    public static string GetChangedNote<T>(this T oldVal, T newVal, List<string> specialList)
    {
        // 要排除某些特殊属性
        var list = DetailedCompare<T>(oldVal, newVal);
        var newList = list.Select(s => new
        {
            s.PropertyName,
            OldValue = s.ValueA,
            NewValue = s.ValueB
        });
        if (specialList?.Count > 0)
        {
            newList = newList.Where(s => !specialList.Contains(s.PropertyName));
        }
        if (newList.ToList()?.Count > 0)
        {
            return JsonConvert.SerializeObject(newList, Formatting.Indented);
        }
        return string.Empty;
    }
}

/// <summary>
/// 属性信息
/// </summary>
public class CustomPropertyInfo
{
    /// <summary>
    /// 属性名称
    /// </summary>
    public string? PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    public Type? PropertyType { get; set; }

    /// <summary>
    /// 属性值
    /// </summary>
    public string? PropertyValue { get; set; } = string.Empty;
}

/// <summary>
/// 属性变化
/// </summary>
public class CustomPropertyVariance
{
    /// <summary>
    /// 属性名称
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// 值A
    /// </summary>
    public string ValueA { get; set; } = string.Empty;

    /// <summary>
    /// 值B
    /// </summary>
    public string ValueB { get; set; } = string.Empty;
}