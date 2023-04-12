#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EnumHelper
// Guid:23f4fdd1-650e-49f7-bdc6-7ba00110a2ac
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 12:55:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;
using System.Reflection;
using XiHan.Utils.Consoles;
using XiHan.Utils.Objects;

namespace XiHan.Utils.Enums;

/// <summary>
/// 枚举帮助类
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// 根据键获取单个枚举的描述信息
    /// </summary>
    /// <param name="keyObj"></param>
    /// <returns></returns>
    public static string GetEnumDescriptionByKey(this Enum keyObj)
    {
        var enumName = keyObj.ToString();
        var field = keyObj.GetType().GetField(enumName);
        if (field == null) return string.Empty;
        if (field.GetCustomAttribute(typeof(DescriptionAttribute), false) is DescriptionAttribute description)
            return description.Description;
        return string.Empty;
    }

    /// <summary>
    /// 根据值获取单个枚举的描述信息
    /// </summary>
    /// <param name="valueObj"></param>
    /// <returns></returns>
    public static string GetEnumDescriptionByValue<T>(this object valueObj)
    {
        var description = string.Empty;
        try
        {
            var tEnum = Enum.Parse(typeof(T), valueObj.ParseToString()) as Enum;
            description = tEnum!.GetEnumDescriptionByKey();
        }
        catch (Exception ex)
        {
            ("获取单个枚举的描述信息出错，" + ex.Message).WriteLineError();
        }
        return description;
    }

    /// <summary>
    /// 获取枚举信息列表
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static List<EnumDescDto> GetEnumInfos(this Type enumType)
    {
        List<EnumDescDto> result = new();
        var fields = enumType.GetFields().ToList();
        if (fields.Any()) return result;
        fields.ForEach(field =>
        {
            // 不是枚举字段不处理
            if (field.FieldType.IsEnum)
            {
                var desc = string.Empty;
                if (field.GetCustomAttribute(typeof(DescriptionAttribute), false) is DescriptionAttribute description)
                    desc = description.Description;
                result.Add(new EnumDescDto()
                {
                    Key = field.Name.ToString(),
                    Value = (int)field.GetRawConstantValue()!,
                    Label = desc,
                });
            }
        });
        return result;
    }

    /// <summary>
    /// 转成字典类型
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static Dictionary<int, string> EnumToDictionary(this Type enumType)
    {
        Dictionary<int, string> result = new();
        var fields = enumType.GetFields().ToList();
        if (fields.Any()) return result;
        fields.ForEach(field =>
        {
            // 不是枚举字段不处理
            if (field.FieldType.IsEnum)
            {
                var desc = string.Empty;
                if (field.GetCustomAttribute(typeof(DescriptionAttribute), false) is DescriptionAttribute description)
                    desc = description.Description;
                result.Add((int)field.GetRawConstantValue()!, desc);
            }
        });
        return result;
    }
}

/// <summary>
/// EnumDescDto
/// </summary>
public class EnumDescDto
{
    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 值
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Label { get; set; } = string.Empty;
}