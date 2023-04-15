#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:HtmlEncodeHelper
// Guid:e135ef5a-b4d2-4d05-933f-bdc428da4088
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:24:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Encodes;

/// <summary>
/// Html编码帮助类
/// </summary>
public static class HtmlEncodeHelper
{
    /// <summary>
    /// 对字符串进行 HTML 编码
    /// </summary>
    /// <param name="data">待编码的字符串</param>
    /// <returns>编码后的字符串</returns>
    public static string HtmlEncode(this string data)
    {
        return System.Web.HttpUtility.HtmlEncode(data);
    }

    /// <summary>
    /// 对 HTML 编码的字符串进行解码
    /// </summary>
    /// <param name="data">待解码的字符串</param>
    /// <returns>解码后的字符串</returns>
    public static string HtmlDecode(this string data)
    {
        return System.Web.HttpUtility.HtmlDecode(data);
    }
}