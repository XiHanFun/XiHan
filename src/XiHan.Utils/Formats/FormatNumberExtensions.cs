#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:FormatnumExtensions
// Guid:09739585-bfe3-4b22-81a4-45b135d3466d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:39:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Globalization;

namespace XiHan.Utils.Formats;

/// <summary>
/// 金额格式化拓展类
/// </summary>
public static class FormatNumberExtensions
{
    /// <summary>
    /// 格式化金额(由千位转万位，如 12,345,678.90=>1234,5678.90 )
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string FormatNumberToString(this decimal num)
    {
        var numStr = num.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        string numRes;
        var numDecimal = string.Empty;
        if (numStr.Contains('.'))
        {
            var numInt = numStr.Split('.')[0];
            numDecimal = "." + numStr.Split('.')[1];
            numRes = FormatStringComma(numInt);
        }
        else
        {
            numRes = FormatStringComma(numStr);
        }

        return numRes + numDecimal;
    }

    /// <summary>
    /// 金额字符串加逗号格式化
    /// </summary>
    /// <param name="numint"></param>
    /// <returns></returns>
    private static string FormatStringComma(string numint)
    {
        if (numint.Length <= 4)
        {
            return numint;
        }
        var numNoFormat = numint[..^4];
        var numFormat = numint.Substring(numint.Length - 4, 4);
        if (numNoFormat.Length > 4)
        {
            return FormatStringComma(numNoFormat) + "," + numFormat;
        }

        return numNoFormat + "," + numFormat;
    }
}