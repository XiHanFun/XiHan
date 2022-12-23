#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:Md5Helper
// Guid:c77bf375-d1f2-4a68-b4d1-bb2affcc0aa8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:26:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Encryptions;

/// <summary>
/// MD5Helper加密帮助类
/// </summary>
public static class Md5Helper
{
    /// <summary>
    /// MD5字符串加密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">输入</param>
    /// <returns>加密后字符串</returns>
    public static string EncryptMd5(Encoding encode, string inputString)
    {
        var buffer = encode.GetBytes(inputString);
        //开始加密
        var byteBuffer = MD5.HashData(buffer);
        StringBuilder strBuild = new();
        foreach (var buff in byteBuffer)
        {
            strBuild.Append(buff.ToString("x2"));
        }
        return strBuild.ToString();
    }

    /// <summary>
    /// MD5流加密
    /// </summary>
    /// <param name="inputPath">文件路径</param>
    /// <returns></returns>
    public static string EncryptMd5(string inputPath)
    {
        using FileStream stream = new(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);

        using var md5 = MD5.Create();
        //开始加密
        var buffer = md5.ComputeHash(stream);
        StringBuilder strBuild = new();
        foreach (var buff in buffer)
        {
            strBuild.Append(buff.ToString("x2"));
        }
        return strBuild.ToString();
    }

    /// <summary>
    /// MD5字符串加密扩展方法
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string ToMd5(this string inputString)
    {
        return EncryptMd5(Encoding.UTF8, inputString);
    }
}