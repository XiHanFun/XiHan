#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AesHelper
// Guid:5ee7ecb0-0156-4152-99d5-13a4274e92b4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-15 下午 12:06:07
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Framework.Core.System.Security.Cryptography;

/// <summary>
/// Aes 加密解密
/// </summary>
/// <remarks>比 Des 加密更加安全</remarks>
public static class AesHelper
{
    // AES KEY 的位数
    private const int KeySize = 256;

    // 加密块大小
    private const int BlockSize = 128;

    // 迭代次数
    private const int Iterations = 10000;

    // 分割器
    private static readonly char _separator = ':';

    /// <summary>
    /// 加密字符串
    /// </summary>
    /// <param name="plainText">要加密的文本</param>
    /// <param name="password">密码</param>
    /// <returns>加密结果</returns>
    public static string Encrypt(string plainText, string password)
    {
        // 检查参数
        ArgumentException.ThrowIfNullOrEmpty(plainText, nameof(plainText));
        ArgumentException.ThrowIfNullOrEmpty(password, nameof(password));

        // 生成盐
        var salt = new byte[BlockSize / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // 扩展方法密码为 IV 和 KEY
        var key = DeriveKey(password, salt, KeySize / 8);
        var iv = DeriveKey(password, salt, BlockSize / 8);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // 加密算法
        string cipherText;
        using (MemoryStream cipherStream = new())
        {
            using CryptoStream cryptoStream = new(cipherStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherBytes = cipherStream.ToArray();
            cipherText = Convert.ToBase64String(cipherBytes);
        }

        // 返回加密结果
        return $"{Convert.ToBase64String(salt)}{_separator}{cipherText}";
    }

    /// <summary>
    /// 解密字符串
    /// </summary>
    /// <param name="cipherText">要解密的文本</param>
    /// <param name="password">密码</param>
    /// <returns>解密结果</returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Decrypt(string cipherText, string password)
    {
        // 检查参数
        ArgumentException.ThrowIfNullOrEmpty(cipherText, nameof(cipherText));
        ArgumentException.ThrowIfNullOrEmpty(password, nameof(password));

        // 解析盐和密文
        var parts = cipherText.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
            throw new ArgumentException("Invalid cipher text", nameof(cipherText));

        var salt = Convert.FromBase64String(parts[0]);
        var cipherBytes = Convert.FromBase64String(parts[1]);

        // 扩展方法密码为 IV 和 KEY
        var key = DeriveKey(password, salt, KeySize / 8);
        var iv = DeriveKey(password, salt, BlockSize / 8);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // 解密算法
        using MemoryStream plainStream = new();
        using CryptoStream cryptoStream = new(plainStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
        cryptoStream.FlushFinalBlock();
        var plainBytes = plainStream.ToArray();
        var plainText = Encoding.UTF8.GetString(plainBytes);

        // 返回解密结果
        return plainText;
    }

    /// <summary>
    /// 扩展方法密码为 IV 和 KEY
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private static byte[] DeriveKey(string password, byte[] salt, int bytes)
    {
        using Rfc2898DeriveBytes pbkdf2 = new(password, salt, Iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(bytes);
    }
}