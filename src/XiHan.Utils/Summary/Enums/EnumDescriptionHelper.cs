#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EnumDescriptionHelper
// Guid:23f4fdd1-650e-49f7-bdc6-7ba00110a2ac
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 12:55:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Utils.Summary.Enums;

/// <summary>
/// 枚举描述帮助类
/// </summary>
public static class EnumDescriptionHelper
{
    /// <summary>
    /// 获取单个枚举健的描述信息
    /// </summary>
    /// <param name="enumObj"></param>
    /// <returns></returns>
    public static string GetEnumDescription(this Enum enumObj)
    {
        var enumName = enumObj.ToString();
        var field = enumObj.GetType().GetField(enumName);
        if (field == null) return string.Empty;
        if (field.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] description)
            return description.First().Description;
        return string.Empty;
    }

    /// <summary>
    /// 获取枚举描述信息列表
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static List<EnumDescDto>? GetEnumDescriptions(this Type enumType)
    {
        List<EnumDescDto> result = new();
        var fields = enumType.GetFields().ToList();
        if (fields.Any()) return null;
        fields.ForEach(field =>
        {
            // 不是枚举字段不处理
            if (field.FieldType.IsEnum)
            {
                var desc = string.Empty;
                if (field.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] description)
                    desc = description[0].Description;
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
}