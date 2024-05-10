#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppConfigExtend
// Guid:519baabf-8911-488b-a4fb-d69445ceb28c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 14:13:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace XiHan.Infrastructure.Core.Apps.Configs;

/// <summary>
/// 配置扩展
/// </summary>
public static class AppConfigExtend
{
    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TKey GetValue<TKey>([DisallowNull] this TKey key, [CallerArgumentExpression(nameof(key))] string fullName = "")
    {
        return key == null
            ? throw new ArgumentNullException(nameof(key))
            : AppConfigProvider.GetValue<TKey>(fullName);
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TKey GetSection<TKey>([DisallowNull] this TKey key, [CallerArgumentExpression(nameof(key))] string fullName = "")
    {
        return key == null
            ? throw new ArgumentNullException(nameof(key))
            : AppConfigProvider.GetSection<TKey>(fullName);
    }

    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="fullName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Set<TKey, TValue>([DisallowNull] this TKey key, TValue value, [CallerArgumentExpression(nameof(key))] string fullName = "")
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        AppConfigProvider.Set<TKey, TValue>(fullName, value);
    }
}