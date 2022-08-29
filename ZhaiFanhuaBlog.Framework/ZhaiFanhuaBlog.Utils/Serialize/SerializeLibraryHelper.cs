// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SerializeLibraryHelper
// Guid:1756a682-b60c-4ecc-aff0-f614ac7a057c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 上午 02:04:48
// ----------------------------------------------------------------

using System.Reflection;

namespace ZhaiFanhuaBlog.Utils.Serialize;

/// <summary>
/// 序列化库帮助类
/// </summary>
public static class SerializeLibraryHelper
{
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
}

/// <summary>
/// 属性信息
/// </summary>
public class CustomPropertyInfo
{
    /// <summary>
    /// 属性名称
    /// </summary>
    public string? PropertyName;

    /// <summary>
    /// 类型
    /// </summary>
    public Type? PropertyType;

    /// <summary>
    /// 属性值
    /// </summary>
    public string? PropertyValue;
}