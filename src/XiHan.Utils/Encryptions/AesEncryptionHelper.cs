#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:AesEncryptionHelper
// Guid:5ee7ecb0-0156-4152-99d5-13a4274e92b4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 下午 12:06:07
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// Aes加密解密类
/// 比Des加密更加安全
/// </summary>
public static class AesEncryptionHelper
{
    // AES KEY 的位数
    private const int KEY_SIZE = 256;

    // 加密块大小
    private const int BLOCK_SIZE = 128;

    // 迭代次数
    private const int ITERATIONS = 10000;

    /// <summary>
    /// 加密方法
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string Encrypt(string plainText, string password)
    {
        // 生成盐
        byte[] salt = new byte[BLOCK_SIZE / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // 扩展密码为 IV 和 KEY
        byte[] key = DeriveKey(password, salt, KEY_SIZE / 8);
        byte[] iv = DeriveKey(password, salt, BLOCK_SIZE / 8);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // 加密算法
        string cipherText;
        using (var cipherStream = new MemoryStream())
        using (var cryptoStream = new CryptoStream(cipherStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = cipherStream.ToArray();

            cipherText = Convert.ToBase64String(cipherBytes);
        }

        // 返回加密结果
        return $"{Convert.ToBase64String(salt)}:{cipherText}";
    }

    /// <summary>
    /// 解密方法
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Decrypt(string cipherText, string password)
    {
        // 检查密文的有效性
        if (string.IsNullOrEmpty(cipherText))
        {
            throw new ArgumentException("Invalid cipher text", nameof(cipherText));
        }

        // 解析盐和密文
        string[] parts = cipherText.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid cipher text", nameof(cipherText));
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] cipherBytes = Convert.FromBase64String(parts[1]);

        // 扩展密码为 IV 和 KEY
        byte[] key = DeriveKey(password, salt, KEY_SIZE / 8);
        byte[] iv = DeriveKey(password, salt, BLOCK_SIZE / 8);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        // 解密算法
        string plainText;
        using (var plainStream = new MemoryStream())
        using (var cryptoStream = new CryptoStream(plainStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] plainBytes = plainStream.ToArray();

            plainText = Encoding.UTF8.GetString(plainBytes);
        }

        // 返回解密结果
        return plainText;
    }

    /// <summary>
    /// 扩展密码为 IV 和 KEY
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private static byte[] DeriveKey(string password, byte[] salt, int bytes)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(bytes);
    }
}