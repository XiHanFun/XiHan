#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ObjectConvertHelper
// Guid:53530b50-ea0a-4f9e-b05a-af39735a6bc0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-18 上午 02:20:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Collections;

namespace XiHan.Utils.Object;

/// <summary>
/// 对象转换帮助类
/// </summary>
public static class ObjectConvertHelper
{
    #region Bool

    /// <summary>
    /// 对象转布尔值
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool ParseToBool(this object? thisValue)
    {
        var reval = false;
        if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return reval;
    }

    #endregion

    #region Int

    /// <summary>
    /// 对象转数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static int ParseToInt(this object? thisValue)
    {
        var reval = 0;
        if (thisValue == null)
        {
            return 0;
        }
        if (thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
        {
            return reval;
        }
        return reval;
    }

    /// <summary>
    /// 对象转数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static int ParseToInt(this object? thisValue, int errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region Short

    /// <summary>
    /// 对象转短整数
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static short ParseToShort(this object? thisValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && short.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return 0;
    }

    /// <summary>
    /// 对象转短整数
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static short ParseToShort(this object? thisValue, short errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && short.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region Long

    /// <summary>
    /// 对象转长整数
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static long ParseToLong(this object? thisValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && long.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return 0L;
    }

    /// <summary>
    /// 对象转长整数
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static long ParseToLong(this object? thisValue, long errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && long.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region Float

    /// <summary>
    /// 对象转浮点数
    /// ±1.5 x 10 e−45 至 ±3.4 x 10 e38	大约 6-9 位数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static float ParseToFloat(this object? thisValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && float.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return 0.0F;
    }

    /// <summary>
    /// 对象转浮点数
    /// ±1.5 x 10 e−45 至 ±3.4 x 10 e38	大约 6-9 位数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static float ParseToFloat(this object? thisValue, float errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && float.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region Double

    /// <summary>
    /// 对象转浮点数
    /// ±5.0 × 10 e−324 到 ±1.7 × 10 e308	大约 15-17 位数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static double ParseToDouble(this object? thisValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return 0.0D;
    }

    /// <summary>
    /// 对象转浮点数
    /// ±5.0 × 10 e−324 到 ±1.7 × 10 e308	大约 15-17 位数字
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static double ParseToDouble(this object? thisValue, double errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region Decimal

    /// <summary>
    /// 对象转浮点数
    /// ±1.0 x 10 e-28 至 ±7.9228 x 10 e28	28-29 位
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static decimal ParseToDecimal(this object? thisValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return 0M;
    }

    /// <summary>
    /// 对象转浮点数
    /// ±1.0 x 10 e-28 至 ±7.9228 x 10 e28	28-29 位
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static decimal ParseToDecimal(this object? thisValue, decimal errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region Money

    /// <summary>
    /// 对象转金额
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static double ParseToMoney(this object? thisValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return 0;
    }

    /// <summary>
    /// 对象转金额
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static double ParseToMoney(this object? thisValue, double errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region String

    /// <summary>
    /// 对象转字符串
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static string ParseToString(this object? thisValue)
    {
        if (thisValue != null)
            return thisValue.ToString()!.Trim();
        return string.Empty;
    }

    /// <summary>
    /// 对象转字符串
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static string ParseToString(this object? thisValue, string errorValue)
    {
        if (thisValue != null)
            return thisValue.ToString()!.Trim();
        return errorValue;
    }

    /// <summary>
    /// 判断是否为空
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool IsEmptyOrNull(this object? thisValue)
    {
        return !IsNotEmptyOrNull(thisValue);
    }

    /// <summary>
    /// 判断是否为空
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool IsNotEmptyOrNull(this object? thisValue)
    {
        return ParseToString(thisValue) != string.Empty && ParseToString(thisValue) != "" && ParseToString(thisValue) != "undefined" && ParseToString(thisValue) != "null";
    }

    /// <summary>
    /// 判断是否为空或零
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool IsNullOrZero(this object? thisValue)
    {
        return !IsNotNullOrZero(thisValue);
    }

    /// <summary>
    /// 判断是否为空或零
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool IsNotNullOrZero(this object? thisValue)
    {
        return IsNotEmptyOrNull(thisValue) && ParseToString(thisValue) != "0";
    }

    #endregion

    #region Date

    /// <summary>
    /// 对象转日期
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static DateTime ParseToDate(this object? thisValue)
    {
        var reval = DateTime.MinValue;
        if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
        {
            reval = Convert.ToDateTime(thisValue);
        }
        return reval;
    }

    /// <summary>
    /// 对象转日期
    /// </summary>
    /// <param name="thisValue"></param>
    /// <param name="errorValue"></param>
    /// <returns></returns>
    public static DateTime ParseToDate(this object? thisValue, DateTime errorValue)
    {
        if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out var reval))
        {
            return reval;
        }
        return errorValue;
    }

    #endregion

    #region Guid

    /// <summary>
    /// 将string转换为Guid
    /// 若转换失败，则返回Guid.Empty，不抛出异常。
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static Guid ParseToGuid(this string thisValue)
    {
        try
        {
            return new Guid(thisValue);
        }
        catch
        {
            return Guid.Empty;
        }
    }

    #endregion

    #region 强制转换类型

    /// <summary>
    /// 强制转换类型
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<TResult> CastSuper<TResult>(this IEnumerable source)
    {
        foreach (object item in source)
        {
            yield return (TResult)Convert.ChangeType(item, typeof(TResult));
        }
    }

    #endregion
}