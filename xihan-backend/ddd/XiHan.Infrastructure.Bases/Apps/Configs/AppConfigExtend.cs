#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AppConfigExtend
// Guid:6d94d4d9-7ab6-4feb-95b2-d055bcc13494
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-10 上午 02:00:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace XiHan.Infrastructures.Bases.Apps.Configs;

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
        return key == null ? throw new ArgumentNullException(nameof(key)) : AppConfigProvider.GetValue<TKey>(fullName);
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
        return key == null ? throw new ArgumentNullException(nameof(key)) : AppConfigProvider.GetSection<TKey>(fullName);
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