#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ShaHelper
// Guid:871e648a-233c-41fd-8531-de4ce3a3820f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:28:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// SHAHelper加密帮助类
/// </summary>
public static class ShaHelper
{
    /// <summary>
    /// SHA1字符串加密
    /// </summary>
    /// <param name="inputString">输入</param>
    /// <param name="encode">编码</param>
    /// <returns>加密后字符串160位</returns>
    public static string EncryptSha1(this string inputString, Encoding encode)
    {
        var buffer = encode.GetBytes(inputString);
        //开始加密
        var byteBuffer = SHA1.HashData(buffer);
        StringBuilder strBuild = new();
        foreach (var buff in byteBuffer)
        {
            strBuild.Append(buff.ToString("x2"));
        }
        return strBuild.ToString();
    }

    /// <summary>
    /// SHA256字符串加密
    /// </summary>
    /// <param name="inputString">输入</param>
    /// <param name="encode">编码</param>
    /// <returns>加密后字符串256位</returns>
    public static string EncryptSha256(this string inputString, Encoding encode)
    {
        var buffer = encode.GetBytes(inputString);
        //开始加密
        var byteBuffer = SHA256.HashData(buffer);
        StringBuilder strBuild = new();
        foreach (var buff in byteBuffer)
        {
            strBuild.Append(buff.ToString("x2"));
        }
        return strBuild.ToString();
    }

    /// <summary>
    /// SHA384字符串加密
    /// </summary>
    /// <param name="inputString">输入</param>
    /// <param name="encode">编码</param>
    /// <returns>加密后字符串384位</returns>
    public static string EncryptSha384(this string inputString, Encoding encode)
    {
        var buffer = encode.GetBytes(inputString);
        //开始加密
        var byteBuffer = SHA384.HashData(buffer);
        StringBuilder strBuild = new();
        foreach (var buff in byteBuffer)
        {
            strBuild.Append(buff.ToString("x2"));
        }
        return strBuild.ToString();
    }

    /// <summary>
    /// SHA512字符串加密
    /// </summary>
    /// <param name="inputString">输入</param>
    /// <param name="encode">编码</param>
    /// <returns>加密后字符串512位</returns>
    public static string EncryptSha512(this string inputString, Encoding encode)
    {
        var buffer = encode.GetBytes(inputString);
        //开始加密
        var byteBuffer = SHA512.HashData(buffer);
        StringBuilder strBuild = new();
        foreach (var buff in byteBuffer)
        {
            strBuild.Append(buff.ToString("x2"));
        }
        return strBuild.ToString();
    }
}