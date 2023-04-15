#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:Base64EncodeHelper
// Guid:df79d6d2-b60d-4210-889f-51b82f469af1
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:25:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Utils.Encodes;

/// <summary>
/// Base64编码帮助类
/// </summary>
public static class Base64EncodeHelper
{
    /// <summary>
    /// 对字符串进行 Base64 编码
    /// </summary>
    /// <param name="data">待编码的字符串</param>
    /// <returns>编码后的字符串</returns>
    public static string Base64Encode(this string data)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 对 Base64 编码的字符串进行解码
    /// </summary>
    /// <param name="data">待解码的字符串</param>
    /// <returns>解码后的字符串</returns>
    public static string Base64Decode(this string data)
    {
        byte[] bytes = Convert.FromBase64String(data);
        return Encoding.UTF8.GetString(bytes);
    }
}