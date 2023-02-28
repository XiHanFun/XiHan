#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UrlHelper
// Guid:521ad433-968f-49dc-84b1-5948de76eaec
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 上午 05:04:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using System.Web;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// URL编码帮助类
/// </summary>
public static class UrlHelper
{
    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="encode"></param>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string UrlEncode(Encoding encode, string inputString)
    {
        return HttpUtility.UrlEncode(inputString, encode);
    }

    /// <summary>
    /// Url解码
    /// </summary>
    /// <param name="encode"></param>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string UrlDecode(Encoding encode, string inputString)
    {
        return HttpUtility.UrlDecode(inputString, encode);
    }

    /// <summary>
    /// Url编码扩展
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string ToUrlEncode(this string inputString)
    {
        return UrlEncode(Encoding.UTF8, inputString);
    }

    /// <summary>
    /// Url解码扩展
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string ToUrlDecode(this string inputString)
    {
        return UrlDecode(Encoding.UTF8, inputString);
    }
}