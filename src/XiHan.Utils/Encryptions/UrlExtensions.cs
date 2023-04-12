#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UrlExtensions
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
/// URL编码拓展类
/// </summary>
public static class UrlExtensions
{
    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="inputString">输入</param>
    /// <param name="encode">编码</param>
    /// <returns></returns>
    public static string UrlEncode(this string inputString, Encoding encode)
    {
        return HttpUtility.UrlEncode(inputString, encode);
    }

    /// <summary>
    /// Url解码
    /// </summary>
    /// <param name="inputString">输入</param>
    /// <param name="encode">编码</param>
    /// <returns></returns>
    public static string UrlDecode(this string inputString, Encoding encode)
    {
        return HttpUtility.UrlDecode(inputString, encode);
    }
}