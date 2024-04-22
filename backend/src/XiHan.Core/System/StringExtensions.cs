#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:StringExtensions
// Guid:3630d8a8-77e0-45eb-a1e6-f9a6b5dc26ba
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-03 上午 12:30:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.System;

/// <summary>
/// 字符串扩展方法
/// </summary>
public static class StringExtensions
{
    #region 半角全角转换

    /// <summary>
    /// 半角转全角的函数(SBC case)
    /// </summary>
    /// <param name="sourceStr">源字符串</param>
    /// <returns></returns>
    public static string ToSbc(this string sourceStr)
    {
        var c = sourceStr.ToCharArray();
        for (var i = 0; i < c.Length; i++)
        {
            if (c[i] == 32)
            {
                c[i] = (char)12288;
                continue;
            }

            if (c[i] < 127) c[i] = (char)(c[i] + 65248);
        }

        return new string(c);
    }

    /// <summary>
    /// 全角转半角的函数(SBC case)
    /// </summary>
    /// <param name="sourceStr">源字符串</param>
    /// <returns></returns>
    public static string ToDbc(this string sourceStr)
    {
        var c = sourceStr.ToCharArray();
        for (var i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }

            if (c[i] is > (char)65280 and < (char)65375) c[i] = (char)(c[i] - 65248);
        }

        return new string(c);
    }

    #endregion
}