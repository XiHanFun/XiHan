#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AppConfigExtend
// Guid:6d94d4d9-7ab6-4feb-95b2-d055bcc13494
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-10 上午 02:00:44
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Runtime.CompilerServices;

namespace XiHan.Infrastructure.Apps.Setting;

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
    public static TKey GetValue<TKey>(this TKey key, [CallerArgumentExpression(nameof(key))] string fullName = "")
    {
        return AppConfigManager.GetValue<TKey>(fullName);
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="key"></param>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static TKey GetSection<TKey>(this TKey key, [CallerArgumentExpression(nameof(key))] string fullName = "")
    {
        return AppConfigManager.GetSection<TKey>(fullName);
    }

    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="fullName"></param>
    public static void Set<TKey, TValue>(this TKey key, TValue value, [CallerArgumentExpression(nameof(key))] string fullName = "")
    {
        AppConfigManager.Set<TKey, TValue>(fullName, value);
    }
}