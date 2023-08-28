﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:Md5HashEncryptionHelper
// Guid:21e9cb49-385d-4549-ad4e-1fcfd56b3472
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-15 上午 11:57:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// Md5 生成哈希类
/// </summary>
public static class Md5HashEncryptionHelper
{
    /// <summary>
    /// 对字符串进行 MD5 生成哈希
    /// </summary>
    /// <param name="input">待加密的明文字符串</param>
    /// <returns>加密后的字符串(32位或64位)</returns>
    public static string Encrypt(string input)
    {
        // 计算32位MD5值
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);
        string md5Str = ComputeHash(hashBytes);
        if (md5Str.Length == 32)
        {
            // 转换为64位MD5值
            byte[] bytes = Encoding.UTF8.GetBytes(md5Str);
            return ComputeHash(bytes);
        }
        else
        {
            return md5Str;
        }
    }

    /// <summary>
    /// 对数据流进行 MD5 生成哈希，返回32位MD5值
    /// </summary>
    /// <param name="inputPath">待加密的数据流路径</param>
    /// <returns></returns>
    public static string EncryptStream(string inputPath)
    {
        using FileStream stream = new(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using MD5 md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
    }

    /// <summary>
    /// 对数据流进行 MD5 生成哈希，返回64位MD5值
    /// </summary>
    /// <param name="inputPath">待加密的数据流</param>
    /// <returns></returns>
    public static string Encrypt64Stream(string inputPath)
    {
        using FileStream stream = new(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using MD5 md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(stream);
        return ComputeHash(hashBytes);
    }

    /// <summary>
    /// 计算哈希值
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private static string ComputeHash(IEnumerable<byte> source)
    {
        // 将字节数组转换为十六进制字符串
        StringBuilder sb = new();
        foreach (byte t in source)
        {
            _ = sb.Append(t.ToString("x2"));
        }
        // 返回生成的哈希值
        return sb.ToString();
    }
}