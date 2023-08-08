#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ObjectJudgeExtensions
// Guid:1301570f-1215-438e-bb49-f9b2fca6525b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-11 下午 10:12:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Extensions;

/// <summary>
/// 对象判断拓展类
/// </summary>
public static class ObjectJudgeExtension
{
    #region 判断范围

    /// <summary>
    /// 判断当前值是否介于指定范围内
    /// </summary>
    /// <typeparam name="T"> 动态类型 </typeparam>
    /// <param name="value"> 动态类型对象 </param>
    /// <param name="start"> 范围起点 </param>
    /// <param name="end"> 范围终点 </param>
    /// <param name="leftEqual"> 是否可等于上限(默认等于) </param>
    /// <param name="rightEqual"> 是否可等于下限(默认等于) </param>
    /// <returns> 是否介于 </returns>
    public static bool IsBetween<T>(this IComparable<T> value, T start, T end, bool leftEqual = true, bool rightEqual = true) where T : IComparable
    {
        var flag = leftEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
        return flag && (rightEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0);
    }

    /// <summary>
    /// 判断当前值是否介于指定范围内
    /// </summary>
    /// <typeparam name="T"> 动态类型 </typeparam>
    /// <param name="value"> 动态类型对象 </param>
    /// <param name="min">范围小值</param>
    /// <param name="max">范围大值</param>
    /// <param name="minEqual">是否可等于小值(默认等于)</param>
    /// <param name="maxEqual">是否可等于大值(默认等于)</param>
    public static bool IsInRange<T>(this IComparable<T> value, T min, T max, bool minEqual = true, bool maxEqual = true) where T : IComparable
    {
        var flag = minEqual ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0;
        return flag && (maxEqual ? value.CompareTo(max) <= 0 : value.CompareTo(max) < 0);
    }

    #endregion

    #region 判断为空

    /// <summary>
    /// 判断对象是否为空，为空返回true
    /// </summary>
    /// <typeparam name="T">要验证的对象的类型</typeparam>
    /// <param name="data">要验证的对象</param>
    public static bool IsNullOrEmpty<T>(this T? data)
    {
        // 如果为null
        if (data == null) return true;

        // 如果为""
        if (data is not string) return data is DBNull;
        if (string.IsNullOrEmpty(data.ToString()?.Trim())) return true;

        // 如果为DBNull
        return data is DBNull;
    }

    /// <summary>
    /// 判断对象是否为空，为空返回true
    /// </summary>
    /// <param name="data">要验证的对象</param>
    public static bool IsNullOrEmpty(this object? data)
    {
        // 如果为null
        if (data == null) return true;

        // 如果为""
        if (data is not string) return data is DBNull;
        if (string.IsNullOrEmpty(data.ToString()?.Trim())) return true;

        // 如果为DBNull
        return data is DBNull;
    }

    #endregion
}