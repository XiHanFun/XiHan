#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CheckHelper
// Guid:1273e49e-19ef-400b-9226-e6eada680d2a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/21 22:23:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using System.Diagnostics;
using XiHan.Core.System.Extensions;

namespace XiHan.Core.Verification;

/// <summary>
/// 数据检测帮助类
/// </summary>
[DebuggerStepThrough]
public static class CheckHelper
{
    /// <summary>
    /// 数据不为空判断
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T NotNull<T>(T? value, [NotNull] string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return value;
    }

    /// <summary>
    /// 数据不为空判断
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static T NotNull<T>(T? value, [NotNull] string parameterName, string message)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        return value;
    }

    /// <summary>
    /// 数据不为空判断
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="maxLength"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NotNull(string? value, [NotNull] string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        if (value == null)
        {
            throw new ArgumentException($"{parameterName}不能为空!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 数据不为无效或空白判断
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="maxLength"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NotNullOrWhiteSpace(string? value, [NotNull] string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{parameterName}不能为无效、空值或空白!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 数据不为无效或空值判断
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="maxLength"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NotNullOrEmpty(string? value, [NotNull] string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"{parameterName}不能为无效、空值!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 数据不为无效或空值判断
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static ICollection<T> NotNullOrEmpty<T>(ICollection<T>? value, [NotNull] string parameterName)
    {
        if (value == null || value.Count <= 0)
        {
            throw new ArgumentException($"{parameterName}不能为无效、空值!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// 检查 type 是否可以分配给 TBaseType 类型或其子类型
    /// </summary>
    /// <typeparam name="TBaseType"></typeparam>
    /// <param name="type"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Type AssignableTo<TBaseType>(Type type, [NotNull] string parameterName)
    {
        NotNull(type, parameterName);

        if (!type.IsAssignableTo(typeof(TBaseType)))
        {
            throw new ArgumentException($"({type.AssemblyQualifiedName}的类型)应该分配给{typeof(TBaseType).GetFullNameWithAssemblyName()}!", parameterName);
        }

        return type;
    }

    /// <summary>
    /// 验证字符串的长度是否满足指定的最大长度和最小长度要求
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parameterName"></param>
    /// <param name="maxLength"></param>
    /// <param name="minLength"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string? Length(string? value, [NotNull] string parameterName, int maxLength, int minLength = 0)
    {
        if (minLength > 0)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{parameterName}不能为无效、空值!", parameterName);
            }

            if (value!.Length < minLength)
            {
                throw new ArgumentException($"{parameterName}长度必须等于或大于{minLength}!", parameterName);
            }
        }

        if (value != null && value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName}长度必须等于或小于{maxLength}!", parameterName);
        }

        return value;
    }
}