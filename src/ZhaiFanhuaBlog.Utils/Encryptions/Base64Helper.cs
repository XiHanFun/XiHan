#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Base64Helper
// Guid:ba9e3c1a-7705-4876-b0ed-e82ea575fd48
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 03:49:57
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace ZhaiFanhuaBlog.Utils.Encryptions;

/// <summary>
/// Base64编码解码帮助类
/// </summary>
public static class Base64Helper
{
    /// <summary>
    /// Base64 编码字符串
    /// </summary>
    /// <param name="encode">编码方式</param>
    /// <param name="source">要编码的字符串</param>
    /// <returns>返回编码后的字符串</returns>
    public static string EncodeBase64String(Encoding encode, string source)
    {
        byte[] bytes = encode.GetBytes(source);
        string result;
        try
        {
            result = Convert.ToBase64String(bytes);
        }
        catch
        {
            throw;
        }
        return result;
    }

    /// <summary>
    /// Base64 解码字符串
    /// </summary>
    /// <param name="encode">解码方式</param>
    /// <param name="source">要解码的字符串</param>
    /// <returns>返回解码后的字符串</returns>
    public static string DecodeBase64String(Encoding encode, string source)
    {
        byte[] bytes = Convert.FromBase64String(source);
        string result;
        try
        {
            result = encode.GetString(bytes);
        }
        catch
        {
            throw;
        }
        return result;
    }
}