#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MoneyFormatHelper
// Guid:09739585-bfe3-4b22-81a4-45b135d3466d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:39:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Globalization;

namespace ZhaiFanhuaBlog.Utils.Formats;

/// <summary>
/// 金额格式化帮助类
/// </summary>
public static class MoneyFormatHelper
{
    /// <summary>
    /// 格式化金额(由千位转万位，如 12,345,678.90=>1234,5678.90 )
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public static string FormatDecimalToString(decimal money)
    {
        var moneyStr = money.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        string moneyRes;
        var moneyDecimal = string.Empty;
        if (moneyStr.Contains('.'))
        {
            var moneyInt = moneyStr.Split('.')[0];
            moneyDecimal = "." + moneyStr.Split('.')[1];
            moneyRes = FormatStringComma(moneyInt);
        }
        else
        {
            moneyRes = FormatStringComma(moneyStr);
        }

        return moneyRes + moneyDecimal;
    }

    /// <summary>
    /// 金额字符串加逗号格式化
    /// </summary>
    /// <param name="moneyint"></param>
    /// <returns></returns>
    public static string FormatStringComma(string moneyint)
    {
        if (moneyint.Length <= 4) return moneyint;
        var moneyNoFormat = moneyint[..^4];
        var moneyFormat = moneyint.Substring(moneyint.Length - 4, 4);
        if (moneyNoFormat.Length > 4)
            return FormatStringComma(moneyNoFormat) + "," + moneyFormat;
        return moneyNoFormat + "," + moneyFormat;
    }
}