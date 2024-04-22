﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:TypeExtensions
// Guid:31e3c3dd-e34d-4f03-a890-6f27dd69487f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/22 0:14:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Collections;
using System.ComponentModel;
using JetBrains.Annotations;
using System.Text;
using XiHan.Core.System.Reflection;
using XiHan.Core.Verification;

namespace XiHan.Core.System;

/// <summary>
/// 类型扩展方法
/// </summary>
public static class TypeExtensions
{
    #region 判断类型

    /// <summary>
    /// 判断当前类型是否可由指定类型派生
    /// </summary>
    public static bool IsDeriveClassFrom<TBaseType>([NotNull] this Type type, bool canAbstract = false)
    {
        CheckHelper.NotNull(type, nameof(type));

        return type.IsDeriveClassFrom(typeof(TBaseType), canAbstract);
    }

    /// <summary>
    /// 判断当前类型是否可由指定类型派生
    /// </summary>
    public static bool IsDeriveClassFrom([NotNull] this Type type, Type baseType, bool canAbstract = false)
    {
        CheckHelper.NotNull(type, nameof(type));

        return type.IsClass && (canAbstract || !type.IsAbstract) && type.IsBaseOn(baseType);
    }

    /// <summary>
    /// 判断类型是否为Nullable类型
    /// </summary>
    /// <param name="type"> 要处理的类型 </param>
    /// <returns> 是返回True，不是返回False </returns>
    public static bool IsNullableType(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// 判断类型是否不为Nullable类型
    /// </summary>
    /// <param name="type"> 要处理的类型 </param>
    /// <returns> 是返回True，不是返回False </returns>
    public static bool IsNotNullableType(this Type type)
    {
        return !type.IsNullableType();
    }

    /// <summary>
    /// 判断类型是否为集合类型
    /// </summary>
    /// <param name="type">要处理的类型</param>
    /// <returns>是返回True，不是返回False</returns>
    public static bool IsEnumerable([NotNull] this Type type)
    {
        CheckHelper.NotNull(type, nameof(type));

        return type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
    }

    /// <summary>
    /// 判断当前泛型类型是否可由指定类型的实例填充
    /// </summary>
    /// <param name="genericType">泛型类型</param>
    /// <param name="type">指定类型</param>
    /// <returns></returns>
    public static bool IsGenericAssignableFrom([NotNull] this Type genericType, Type type)
    {
        CheckHelper.NotNull(genericType, nameof(genericType));

        if (!genericType.IsGenericType)
            throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");

        List<Type> allOthers = [type];
        if (genericType.IsInterface) allOthers.AddRange(type.GetInterfaces());

        foreach (var other in allOthers)
        {
            var cur = other;
            while (cur != null)
            {
                if (cur.IsGenericType) cur = cur.GetGenericTypeDefinition();

                if (cur.IsSubclassOf(genericType) || cur == genericType) return true;

                if (cur.BaseType != null) cur = cur.BaseType;
            }
        }

        return false;
    }

    /// <summary>
    /// 返回当前类型是否是指定基类的派生类
    /// </summary>
    /// <param name="type">当前类型</param>
    /// <param name="baseType">要判断的基类型</param>
    /// <returns></returns>
    public static bool IsBaseOn([NotNull] this Type type, Type baseType)
    {
        CheckHelper.NotNull(type, nameof(type));

        return baseType.IsGenericTypeDefinition
            ? baseType.IsGenericAssignableFrom(type)
            : baseType.IsAssignableFrom(type);
    }

    /// <summary>
    /// 返回当前类型是否是指定基类的派生类
    /// </summary>
    /// <typeparam name="TBaseType">要判断的基类型</typeparam>
    /// <param name="type">当前类型</param>
    /// <returns></returns>
    public static bool IsBaseOn<TBaseType>([NotNull] this Type type)
    {
        CheckHelper.NotNull(type, nameof(type));

        var baseType = typeof(TBaseType);
        return type.IsBaseOn(baseType);
    }

    #endregion

    #region 空类型

    /// <summary>
    /// 由类型的Nullable类型返回实际类型
    /// </summary>
    /// <param name="type"> 要处理的类型对象 </param>
    /// <returns> </returns>
    public static Type GetNonNullableType(this Type type)
    {
        return type.IsNullableType() ? type.GetGenericArguments()[0] : type;
    }

    /// <summary>
    /// 通过类型转换器获取Nullable类型的基础类型
    /// </summary>
    /// <param name="type"> 要处理的类型对象 </param>
    /// <returns> </returns>
    public static Type GetUnNullableType(this Type type)
    {
        if (!type.IsNullableType()) return type;
        NullableConverter nullableConverter = new(type);
        return nullableConverter.UnderlyingType;
    }

    #endregion

    #region 获取描述

    /// <summary>
    /// 获取类型的 Description 特性描述信息
    /// </summary>
    /// <param name="type">类型对象</param>
    /// <param name="inherit">是否搜索类型的继承链以查找 Description 特性</param>
    /// <returns>返回 Description 特性描述信息，如不存在则返回类型的全名</returns>
    public static string GetDescription([NotNull] this Type type, bool inherit = true)
    {
        CheckHelper.NotNull(type, nameof(type));

        var result = string.Empty;
        var fullName = type.FullName ?? result;

        var desc = type.GetAttribute<DescriptionAttribute>(inherit);

        if (desc == null)
            return result;

        var description = desc.Description;
        result = fullName + "(" + description + ")";

        return result;
    }

    #endregion

    #region 类型名称

    /// <summary>
    /// 获取类型的全名
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetFullNameWithAssemblyName([NotNull] this Type type)
    {
        CheckHelper.NotNull(type, nameof(type));

        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }

    /// <summary>
    /// 获取类型的全名，附带所在类库
    /// </summary>
    public static string GetFullNameWithModule([NotNull] this Type type)
    {
        CheckHelper.NotNull(type, nameof(type));

        return $"{type.FullName},{type.Module.Name.Replace(".dll", string.Empty).Replace(".exe", string.Empty)}";
    }

    /// <summary>
    /// 获取类型的显示短名称
    /// </summary>
    public static string GetShortDisplayName([NotNull] this Type type)
    {
        CheckHelper.NotNull(type, nameof(type));

        return type.GetDisplayName(false);
    }

    /// <summary>
    /// 获取类型的显示名称
    /// </summary>
    public static string GetDisplayName([NotNull] this Type type, bool fullName = true)
    {
        CheckHelper.NotNull(type, nameof(type));

        StringBuilder sb = new();
        ProcessType(sb, type, fullName);
        return sb.ToString();
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 内置类型名称
    /// </summary>
    private static readonly Dictionary<Type, string> BuiltInTypeNames = new()
    {
        { typeof(bool), "bool" },
        { typeof(byte), "byte" },
        { typeof(char), "char" },
        { typeof(decimal), "decimal" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(int), "int" },
        { typeof(long), "long" },
        { typeof(object), "object" },
        { typeof(sbyte), "sbyte" },
        { typeof(short), "short" },
        { typeof(string), "string" },
        { typeof(uint), "uint" },
        { typeof(ulong), "ulong" },
        { typeof(ushort), "ushort" },
        { typeof(void), "void" }
    };

    /// <summary>
    /// 处理类型
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="type"></param>
    /// <param name="fullName"></param>
    private static void ProcessType(StringBuilder builder, Type type, bool fullName)
    {
        if (type.IsGenericType)
        {
            var genericArguments = type.GetGenericArguments();
            ProcessGenericType(builder, type, genericArguments, genericArguments.Length, fullName);
        }
        else if (type.IsArray)
        {
            ProcessArrayType(builder, type, fullName);
        }
        else if (BuiltInTypeNames.TryGetValue(type, out var builtInName))
        {
            builder.Append(builtInName);
        }
        else if (!type.IsGenericParameter)
        {
            builder.Append(fullName ? type.FullName : type.Name);
        }
    }

    /// <summary>
    /// 处理数组类型
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="type"></param>
    /// <param name="fullName"></param>
    private static void ProcessArrayType(StringBuilder builder, Type type, bool fullName)
    {
        var innerType = type;
        while (innerType!.IsArray) innerType = innerType.GetElementType();

        ProcessType(builder, innerType, fullName);

        while (type.IsArray)
        {
            builder.Append('[');
            builder.Append(',', type.GetArrayRank() - 1);
            builder.Append(']');
            type = type.GetElementType()!;
        }
    }

    /// <summary>
    /// 处理泛型类型
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="type"></param>
    /// <param name="genericArguments"></param>
    /// <param name="length"></param>
    /// <param name="fullName"></param>
    private static void ProcessGenericType(StringBuilder builder, Type type, IReadOnlyList<Type> genericArguments, int length,
        bool fullName)
    {
        var offset = type.IsNested ? type.DeclaringType!.GetGenericArguments().Length : 0;

        if (fullName)
        {
            if (type.IsNested)
            {
                ProcessGenericType(builder, type.DeclaringType!, genericArguments, offset, fullName);
                builder.Append('+');
            }
            else
            {
                builder.Append(type.Namespace);
                builder.Append('.');
            }
        }

        var genericPartIndex = type.Name.IndexOf('`');
        if (genericPartIndex <= 0)
        {
            builder.Append(type.Name);
            return;
        }

        builder.Append(type.Name, 0, genericPartIndex);
        builder.Append('<');

        for (var i = offset; i < length; i++)
        {
            ProcessType(builder, genericArguments[i], fullName);
            if (i + 1 == length) continue;

            builder.Append(',');
            if (!genericArguments[i + 1].IsGenericParameter) builder.Append(' ');
        }

        builder.Append('>');
    }

    #endregion 私有方法
}