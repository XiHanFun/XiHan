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
    /// 格式化金额(1234,5678.90)
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public static string FormatMoneyToDecimal(decimal money)
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
                moneyRes = FormatMoneyToInt(moneyInt);
            }
            else
            {
                moneyRes = FormatMoneyToInt(moneyStr);
            }
            string FormatMoneyToInt(string moneyint)
            {
                if (moneyint.ToString().Length > 4)
                {
                    string moneyNotFormat = moneyint.Substring(0, moneyint.Length - 4);
                    string moneyFormat = moneyint.Substring(moneyint.Length - 4, 4);
                    if (moneyNotFormat.Length > 4)
                    {
                        return FormatMoneyToInt(moneyNotFormat) + "," + moneyFormat;
                    }
                    else
                    {
                        return moneyNotFormat + "," + moneyFormat;
                    }
                }
                else
                {
                    return moneyint;
                }
            }
            return moneyRes + moneyDecimal;
        }
        catch (Exception)
        {
            throw;
        }
    }
}