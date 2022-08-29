// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MoneyFormaHelper
// Guid:09739585-bfe3-4b22-81a4-45b135d3466d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:39:57
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.Formats;

/// <summary>
/// 金额格式化帮助类
/// </summary>
public static class MoneyFormaHelper
{
    /// <summary>
    /// 格式化金额(由千位转万位，如 12,345,678.90=>1234,5678.90 )
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public static string FormatDecimalToString(decimal money)
    {
        try
        {
            string moneyStr = money.ToString();
            string moneyRes = string.Empty;
            string moneyInt = string.Empty;
            string moneyDecimal = string.Empty;
            if (moneyStr.Contains('.'))
            {
                moneyInt = moneyStr.Split('.')[0].ToString();
                moneyDecimal = "." + moneyStr.Split('.')[1].ToString();
                moneyRes = FormatStringComma(moneyInt);
            }
            else
            {
                moneyRes = FormatStringComma(moneyStr);
            }

            return moneyRes + moneyDecimal;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 金额字符串加逗号格式化
    /// </summary>
    /// <param name="moneyint"></param>
    /// <returns></returns>
    public static string FormatStringComma(string moneyint)
    {
        if (moneyint.ToString().Length <= 4) return moneyint;
        string moneyNoFormat = moneyint[..^4];
        string moneyFormat = moneyint.Substring(moneyint.Length - 4, 4);
        if (moneyNoFormat.Length > 4)
            return FormatStringComma(moneyNoFormat) + "," + moneyFormat;
        else
            return moneyNoFormat + "," + moneyFormat;
    }
}