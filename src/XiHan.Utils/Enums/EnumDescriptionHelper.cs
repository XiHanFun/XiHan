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
using System.Reflection;

namespace XiHan.Utils.Enums;

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
        Type type = enumObj.GetType();
        string? name = Enum.GetName(type, enumObj);
        if (name != null)
        {
            var fieldInfo = type.GetField(name);
            if (fieldInfo != null)
            {
                if (fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false) is DescriptionAttribute description)
                    return description.Description;
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// 获取枚举描述信息列表
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static List<EnumDescDto> GetEnumDescriptions(this Type enumType)
    {
        List<EnumDescDto> result = new();
        var fieldInfos = enumType.GetFields().ToList();
        fieldInfos.ForEach(fieldInfo =>
        {
            // 不是枚举字段不处理
            if (fieldInfo.FieldType.IsEnum)
            {
                var desc = string.Empty;
                if (fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false) is DescriptionAttribute description)
                    desc = description.Description;
                result.Add(new EnumDescDto()
                {
                    Key = fieldInfo.Name.ToString(),
                    Value = Convert.ToInt32(fieldInfo.GetRawConstantValue()),
                    Label = desc,
                });
            }
        });
        return result;
    }
}