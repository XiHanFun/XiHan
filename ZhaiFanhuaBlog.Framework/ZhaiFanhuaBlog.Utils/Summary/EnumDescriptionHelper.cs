// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EnumDescriptionHelper
// Guid:23f4fdd1-650e-49f7-bdc6-7ba00110a2ac
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 12:55:52
// ----------------------------------------------------------------

using System.ComponentModel;
using System.Reflection;

namespace ZhaiFanhuaBlog.Utils.Summary;

/// <summary>
/// 枚举描述帮助类
/// </summary>
public static class EnumDescriptionHelper
{
    /// <summary>
    /// 获取枚举的中文描述
    /// </summary>
    /// <param name="enumObj"></param>
    /// <returns></returns>
    public static string GetEnumDescription(Enum enumObj)
    {
        try
        {
            string enumname = enumObj.ToString();
            Type type = enumObj.GetType();
            FieldInfo field = type.GetField(enumname)!;
            DescriptionAttribute[] description = (DescriptionAttribute[])field!.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return description[0].Description;
        }
        catch (Exception)
        {
            throw;
        }
    }
}