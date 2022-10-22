// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ObjectHelper
// Guid:3c22cdf5-2be0-4377-9412-322dcc2ab5e3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-28 下午 06:58:18
// ----------------------------------------------------------------

using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.Utils.Object;

/// <summary>
/// 对象属性的处理操作帮助类
/// </summary>
public static class ObjectHelper
{
    /// <summary>
    /// 对比两个属性的差异信息
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="val1">对象实例1</param>
    /// <param name="val2">对象实例2</param>
    /// <returns></returns>
    public static List<PropertyVariance> DetailedCompare<T>(this T val1, T val2)
    {
        var propertyInfo = typeof(T).GetType().GetProperties();
        return propertyInfo.Select(v => new PropertyVariance
        {
            Property = v.Name,
            //确保不为null
            ValueA = v.GetValue(val1, null)?.ToString() ?? string.Empty,
            ValueB = v.GetValue(val2, null)?.ToString() ?? string.Empty
        })
        //调用内置判断
        .Where(v => !v.ValueA.Equals(v.ValueB))
        .ToList();
    }

    /// <summary>
    /// 把两个对象的差异信息转换为Json 格式
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="oldVal">对象实例1</param>-*8765432kj   zax5
    /// <param name="newVal">对象实例2</param>
    /// <returns></returns>
    public static string GetChangedNote<T>(this T oldVal, T newVal)
    {
        // 要排除某些特殊属性
        var specialList = new List<string> { "CreateTime", "ModifyTime", "DeleteTime" };
        var list = DetailedCompare<T>(oldVal, newVal);
        var newList = list.Select(s => new
        {
            s.Property,
            OldValue = s.ValueA,
            NewValue = s.ValueB
        }).Where(s => !specialList.Contains(s.Property)).ToList();
        if (newList?.Count > 0)
        {
            return JsonConvert.SerializeObject(newList, Formatting.Indented);
        }
        return string.Empty;
    }
}

/// <summary>
/// 属性变化
/// </summary>
public class PropertyVariance
{
    /// <summary>
    /// 属性
    /// </summary>
    public string Property { get; set; } = string.Empty;

    /// <summary>
    /// 值A
    /// </summary>
    public string ValueA { get; set; } = string.Empty;

    /// <summary>
    /// 值B
    /// </summary>
    public string ValueB { get; set; } = string.Empty;
}