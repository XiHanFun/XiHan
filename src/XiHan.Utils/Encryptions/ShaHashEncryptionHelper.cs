#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ShaHashEncryptionHelper
// Guid:b6ed83a8-fa32-4248-93da-26d358d7ed54
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:26:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// Sha 生成哈希类
/// </summary>
/// <remarks>用于数据的完整性校验、数字签名等</remarks>
public static class ShaHashEncryptionHelper
{
    /// <summary>
    /// 生成 SHA1 哈希值
    /// </summary>
    /// <param name="data">待加密的数据</param>
    /// <returns>生成的哈希值</returns>
    public static string Sha1(string data)
    {
        // 创建 SHA1 加密算法实例，将字符串数据转换为字节数组，并生成相应的哈希值
        byte[] hashData = SHA1.HashData(Encoding.UTF8.GetBytes(data));
        // 将字节数组转换为十六进制字符串
        StringBuilder sb = new();
        for (int i = 0; i < hashData.Length; i++)
        {
            sb.Append(hashData[i].ToString("x2"));
        }
        // 返回生成的哈希值
        return sb.ToString();
    }

    /// <summary>
    /// 生成 SHA256 哈希值
    /// </summary>
    /// <param name="data">待加密的数据</param>
    /// <returns>生成的哈希值</returns>
    public static string Sha256(string data)
    {
        // 创建 SHA256 加密算法实例，将字符串数据转换为字节数组，并生成相应的哈希值
        byte[] hashData = SHA256.HashData(Encoding.UTF8.GetBytes(data));
        // 将字节数组转换为十六进制字符串
        StringBuilder sb = new();
        for (int i = 0; i < hashData.Length; i++)
        {
            sb.Append(hashData[i].ToString("x2"));
        }
        // 返回生成的哈希值
        return sb.ToString();
    }

    /// <summary>
    /// 生成 SHA384 哈希值
    /// </summary>
    /// <param name="data">待加密的数据</param>
    /// <returns>生成的哈希值</returns>
    public static string Sha384(string data)
    {
        // 创建 SHA384 加密算法实例，将字符串数据转换为字节数组，并生成相应的哈希值
        byte[] hashData = SHA384.HashData(Encoding.UTF8.GetBytes(data));
        // 将字节数组转换为十六进制字符串
        StringBuilder sb = new();
        for (int i = 0; i < hashData.Length; i++)
        {
            sb.Append(hashData[i].ToString("x2"));
        }
        // 返回生成的哈希值
        return sb.ToString();
    }

    /// <summary>
    /// 生成 SHA512 哈希值
    /// </summary>
    /// <param name="data">待加密的数据</param>
    /// <returns>生成的哈希值</returns>
    public static string Sha512(string data)
    {
        // 创建 SHA512 加密算法实例，将字符串数据转换为字节数组，并生成相应的哈希值
        byte[] hashData = SHA512.HashData(Encoding.UTF8.GetBytes(data));
        // 将字节数组转换为十六进制字符串
        StringBuilder sb = new();
        for (int i = 0; i < hashData.Length; i++)
        {
            sb.Append(hashData[i].ToString("x2"));
        }
        // 返回生成的哈希值
        return sb.ToString();
    }
}