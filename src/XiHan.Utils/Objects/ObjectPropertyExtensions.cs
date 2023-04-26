#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ObjectPropertyExtensions
// Guid:3c22cdf5-2be0-4377-9412-322dcc2ab5e3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-28 下午 06:58:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using XiHan.Utils.Types;

namespace XiHan.Utils.Objects;

/// <summary>
/// 对象属性拓展类
/// </summary>
public static class ObjectPropertyExtensions
{
    /// <summary>
    /// 获取属性全名
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static string GetFullNameOf<TEntity>(this TEntity entity, [CallerArgumentExpression(nameof(entity))] string fullName = "")
    {
        return fullName;
    }

    /// <summary>
    /// 获取类型的Description特性描述信息
    /// </summary>
    /// <param name="entity">类型对象</param>
    /// <param name="inherit">是否搜索类型的继承链以查找描述特性</param>
    /// <returns>返回Description特性描述信息，如不存在则返回类型的全名</returns>
    public static string GetDescription<TEntity>(this TEntity entity, bool inherit = true)
    {
        var result = string.Empty;
        Type objectType = typeof(TEntity);
        var fullName = objectType.FullName ?? result;
        DescriptionAttribute? desc = objectType.GetAttribute<DescriptionAttribute>(inherit);
        if (desc != null)
        {
            var description = desc.Description;
            result = fullName + "(" + description + ")";
        }
        return result;
    }

    /// <summary>
    /// 利用反射来判断对象是否包含某个属性
    /// </summary>
    /// <param name="instance">object</param>
    /// <param name="propertyName">需要判断的属性</param>
    /// <returns>是否包含</returns>
    public static bool IsContainProperty(this object? instance, string propertyName)
    {
        if (instance != null && !string.IsNullOrEmpty(propertyName))
        {
            var findedPropertyInfo = instance.GetType().GetProperty(propertyName);
            if (findedPropertyInfo != null)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 获取对象属性值
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="entity"></param>
    /// <param name="propertyName">需要判断的属性</param>
    /// <returns></returns>
    public static TValue GetPropertyValue<TEntity, TValue>(this TEntity entity, string propertyName)
    {
        Type objectType = typeof(TEntity);
        PropertyInfo? propertyInfo = objectType.GetProperty(propertyName);
        if (propertyInfo == null || !propertyInfo.PropertyType.IsGenericType)
        {
            throw new ArgumentException($"The property '{propertyName}' does not exist or is not a generic type in type '{objectType.Name}'.");
        }

        var param_obj = Expression.Parameter(typeof(TEntity));
        var param_val = Expression.Parameter(typeof(TValue));

        // 转成真实类型，防止Dynamic类型转换成object
        var body_obj = Expression.Convert(param_obj, objectType);
        var body = Expression.Property(body_obj, propertyInfo);
        var getValue = Expression.Lambda<Func<TEntity, TValue>>(body, param_obj).Compile();
        return getValue(entity);
    }

    /// <summary>
    /// 设置对象属性值
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="entity"></param>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool SetPropertyValue<TEntity, TValue>(this TEntity entity, string propertyName, TValue value)
    {
        Type objectType = typeof(TEntity);
        PropertyInfo? propertyInfo = objectType.GetProperty(propertyName);
        if (propertyInfo == null || !propertyInfo.PropertyType.IsGenericType)
        {
            throw new ArgumentException($"The property '{propertyName}' does not exist or is not a generic type in type '{objectType.Name}'.");
        }

        var param_obj = Expression.Parameter(objectType);
        var param_val = Expression.Parameter(typeof(TValue));
        var body_obj = Expression.Convert(param_obj, objectType);
        var body_val = Expression.Convert(param_val, propertyInfo.PropertyType);

        // 获取设置属性的值的方法
        var setMethod = propertyInfo.GetSetMethod(true);

        // 如果只是只读,则 setMethod==null
        if (setMethod != null)
        {
            var body = Expression.Call(param_obj, setMethod, body_val);
            var setValue = Expression.Lambda<Action<TEntity, TValue>>(body, param_obj, param_val).Compile();
            setValue(entity, value);

            return true;
        }
        return false;
    }

    /// <summary>
    /// 获取对象属性信息列表
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public static List<CustomPropertyInfo> GetPropertyInfos<TEntity>(this TEntity entity) where TEntity : class
    {
        var type = typeof(TEntity);
        PropertyInfo[] properties = type.GetProperties();
        List<CustomPropertyInfo> customPropertyInfos = new();
        foreach (var info in properties)
        {
            CustomPropertyInfo customPropertyInfo = new()
            {
                PropertyName = info.Name,
                PropertyType = info.PropertyType.Name,
                PropertyValue = info.GetValue(entity).ParseToString(),
            };
            customPropertyInfos.Add(customPropertyInfo);
        }
        return customPropertyInfos;
    }

    /// <summary>
    /// 对比两个相同类型的相同属性之间的差异信息
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <param name="entity1">对象实例1</param>
    /// <param name="entity2">对象实例2</param>
    /// <returns></returns>
    public static List<CustomPropertyVariance> GetPropertyDetailedCompare<TEntity>(this TEntity entity1, TEntity entity2) where TEntity : class
    {
        var propertyInfo = typeof(TEntity).GetProperties();
        return propertyInfo.Select(variance => new CustomPropertyVariance
        {
            PropertyName = variance.Name,
            // 确保不为null
            ValueA = variance.GetValue(entity1, null)?.ToString() ?? string.Empty,
            ValueB = variance.GetValue(entity2, null)?.ToString() ?? string.Empty
        })
        //调用内置判断
        .Where(variance => !variance.ValueA.Equals(variance.ValueB))
        .ToList();
    }

    /// <summary>
    /// 把两个对象的差异信息转换为Json 格式
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <param name="oldVal">对象实例1</param>
    /// <param name="newVal">对象实例2</param>
    /// <param name="specialList">要排除某些特殊属性</param>
    /// <returns></returns>
    public static string GetPropertyChangedNote<TEntity>(this TEntity oldVal, TEntity newVal, List<string> specialList) where TEntity : class
    {
        // 要排除某些特殊属性
        var list = GetPropertyDetailedCompare(oldVal, newVal);
        var newList = list.Select(s => new
        {
            s.PropertyName,
            s.ValueA,
            s.ValueB,
        });
        if (specialList.Any())
        {
            newList = newList.Where(s => !specialList.Contains(s.PropertyName));
        }

        var enumerable = newList.ToList();
        if (enumerable.ToList().Any())
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(enumerable, options);
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
    public string? PropertyType { get; set; } = string.Empty;

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