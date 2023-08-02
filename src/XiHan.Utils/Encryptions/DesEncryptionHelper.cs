#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DesEncryptionHelper
// Guid:f43de28f-10c4-4735-84e6-27e4f3cec000
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-15 下午 12:01:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// Des 加密解密类
/// </summary>
public static class DesEncryptionHelper
{
    // 加密所需的密钥
    private static readonly byte[] _keyBytes;

    // 加密所需的密钥
    private static readonly byte[] _ivBytes;

    /// <summary>
    /// 构造函数
    /// </summary>
    static DesEncryptionHelper()
    {
        // 构造函数，初始化密钥和向量
        var password = "XiHan_ZhaiFanhua";
        var pdb = new PasswordDeriveBytes(password, null);
        _keyBytes = pdb.GetBytes(8);
        _ivBytes = pdb.GetBytes(8);
    }

    /// <summary>
    /// 加密字符串
    /// </summary>
    /// <param name="plainText">待加密的明文字符串</param>
    /// <returns>返回加密后的字符串</returns>
    public static string Encrypt(string plainText)
    {
        // 创建加密算法实例
        using var des = DES.Create();
        // 设置加密算法的密钥和向量
        des.Key = _keyBytes;
        des.IV = _ivBytes;

        // 创建内存流，并使用 CryptoStream 对象包装它
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
        {
            // 将明文转换为字节数组，并将其写入 CryptoStream 对象中
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            cs.Write(plainTextBytes, 0, plainTextBytes.Length);
            cs.Close();
        }

        // 从内存流中获取加密后的字节数组，并将其转换为 Base64 字符串
        var cipherTextBytes = ms.ToArray();
        return Convert.ToBase64String(cipherTextBytes);
    }

    /// <summary>
    /// 解密字符串
    /// </summary>
    /// <param name="cipherText">待解密的密文字符串</param>
    /// <returns>返回解密后的字符串</returns>
    public static string Decrypt(string cipherText)
    {
        // 将 Base64 字符串转换为字节数组
        var cipherTextBytes = Convert.FromBase64String(cipherText);

        // 创建解密算法实例
        using var des = DES.Create();
        // 设置解密算法的密钥和向量
        des.Key = _keyBytes;
        des.IV = _ivBytes;

        // 创建内存流，并使用 CryptoStream 对象包装它
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
        {
            // 将密文字节数组写入 CryptoStream 对象中
            cs.Write(cipherTextBytes, 0, cipherTextBytes.Length);
            cs.Close();
        }

        // 将内存流中的解密后的字节数组转换为字符串
        var plainTextBytes = ms.ToArray();
        return Encoding.UTF8.GetString(plainTextBytes);
    }
}