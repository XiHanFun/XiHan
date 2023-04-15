#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:Md5EncryptionHelper
// Guid:21e9cb49-385d-4549-ad4e-1fcfd56b3472
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:57:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// Md5加密类
/// </summary>
public static class Md5EncryptionHelper
{
    /// <summary>
    /// 对字符串进行MD5加密
    /// </summary>
    /// <param name="input">待加密的明文字符串</param>
    /// <returns>加密后的字符串（32位或64位）</returns>
    public static string Encrypt(string input)
    {
        // 计算32位MD5值
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);
        StringBuilder sb = new();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }
        string md5Str = sb.ToString();
        if (md5Str.Length == 32)
        {
            // 转换为64位MD5值
            byte[] bytes = Encoding.UTF8.GetBytes(md5Str);
            StringBuilder sb2 = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb2.Append(bytes[i].ToString("x2"));
            }
            return sb2.ToString();
        }
        else
        {
            return md5Str;
        }
    }
}

/// <summary>
/// MD5数据流加密类
/// </summary>
public static class Md5StreamEncryptionHelper
{
    /// <summary>
    /// 对数据流进行MD5加密，返回32位MD5值
    /// </summary>
    /// <param name="inputPath">待加密的数据流路径</param>
    /// <returns></returns>
    public static string Encrypt(string inputPath)
    {
        using FileStream stream = new(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    /// <summary>
    /// 对数据流进行MD5加密，返回64位MD5值
    /// </summary>
    /// <param name="inputPath">待加密的数据流</param>
    /// <returns></returns>
    public static string Encrypt64(string inputPath)
    {
        using FileStream stream = new(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(stream);
        var sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }
        return sb.ToString();
    }
}