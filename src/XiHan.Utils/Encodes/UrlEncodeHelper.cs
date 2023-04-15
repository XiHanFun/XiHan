#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:UrlEncodeHelper
// Guid:1e01afd2-583b-40bf-ab4a-bae4d919a70e
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:25:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Encodes;

/// <summary>
/// Url编码帮助类
/// </summary>
public static class UrlEncodeHelper
{
    /// <summary>
    /// 对字符串进行 URL 编码
    /// </summary>
    /// <param name="data">待编码的字符串</param>
    /// <returns>编码后的字符串</returns>
    public static string UrlEncode(this string data)
    {
        return Uri.EscapeDataString(data);
    }

    /// <summary>
    /// 对 URL 编码的字符串进行解码
    /// </summary>
    /// <param name="data">待解码的字符串</param>
    /// <returns>解码后的字符串</returns>
    public static string UrlDecode(this string data)
    {
        return Uri.UnescapeDataString(data);
    }
}