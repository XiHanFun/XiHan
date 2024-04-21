#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:MemberInfoExtensions
// Guid:ea8182f6-8462-4ae3-894b-d2eb401671a2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/22 1:08:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace XiHan.Core.System.Reflection;

/// <summary>
/// 成员信息扩展方法
/// </summary>
public static class MemberInfoExtensions
{
    #region 描述信息

    /// <summary>
    /// 获取成员元数据的 Description 特性描述信息
    /// </summary>
    /// <param name="member">成员元数据对象</param>
    /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
    /// <returns>返回 Description 特性描述信息，如不存在则返回成员的名称</returns>
    public static string GetDescription(this MemberInfo member, bool inherit = true)
    {
        var desc = member.GetAttribute<DescriptionAttribute>(inherit);
        if (desc != null)
            return desc.Description;

        var displayName = member.GetAttribute<DisplayNameAttribute>(inherit);
        if (displayName != null)
            return displayName.DisplayName;

        var display = member.GetAttribute<DisplayAttribute>(inherit);
        return display != null
            ? display.Name ?? string.Empty : member.Name;
    }

    #endregion

    #region 特性信息

    /// <summary>
    /// 检查指定指定类型成员中是否存在指定的Attribute特性
    /// </summary>
    /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
    /// <param name="memberInfo">要检查的类型成员</param>
    /// <param name="inherit">是否从继承中查找</param>
    /// <returns>是否存在</returns>
    public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
    {
        return memberInfo.IsDefined(typeof(T), inherit);
    }

    /// <summary>
    /// 从类型成员获取指定Attribute特性
    /// </summary>
    /// <typeparam name="T">Attribute特性类型</typeparam>
    /// <param name="memberInfo">类型类型成员</param>
    /// <param name="inherit">是否从继承中查找</param>
    /// <returns>存在返回第一个，不存在返回null</returns>
    public static T? GetAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
    {
        var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
        return attributes.FirstOrDefault() as T;
    }

    /// <summary>
    /// 从类型成员获取指定Attribute特性
    /// </summary>
    /// <typeparam name="T">Attribute特性类型</typeparam>
    /// <param name="memberInfo">类型类型成员</param>
    /// <param name="inherit">是否从继承中查找</param>
    /// <returns>返回所有指定Attribute特性的数组</returns>
    public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
    {
        return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
    }

    #endregion
}