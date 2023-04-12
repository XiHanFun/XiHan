#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Base64Extensions
// Guid:ba9e3c1a-7705-4876-b0ed-e82ea575fd48
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 03:49:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// Base64编码解码拓展类
/// </summary>
public static class Base64Extensions
{
    /// <summary>
    /// Base64 编码
    /// </summary>
    /// <param name="encode">编码方式</param>
    /// <param name="source">要编码的字符串</param>
    /// <returns>返回编码后的字符串</returns>
    public static string Base64Encode(Encoding encode, string source)
    {
        var bytes = encode.GetBytes(source);
        var result = Convert.ToBase64String(bytes);
        return result;
    }

    /// <summary>
    /// Base64 解码
    /// </summary>
    /// <param name="encode">解码方式</param>
    /// <param name="source">要解码的字符串</param>
    /// <returns>返回解码后的字符串</returns>
    public static string Base64Decode(Encoding encode, string source)
    {
        var bytes = Convert.FromBase64String(source);
        var result = encode.GetString(bytes);
        return result;
    }
}