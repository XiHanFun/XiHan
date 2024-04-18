#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// PathName:RsaEncryptionHelper
// Guid:fa690f78-718e-4573-9710-aa74828385a6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-15 下午 12:09:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Common.Utilities.Encryptions;

/// <summary>
/// RSA 加密解密，非对称
/// </summary>
public static class RsaEncryptionHelper
{
    // Rsa 容器
    private static readonly RSACryptoServiceProvider _rsaProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    static RsaEncryptionHelper()
    {
        _rsaProvider = new RSACryptoServiceProvider
        {
            PersistKeyInCsp = false
        };
    }

    #region 加密

    /// <summary>
    /// 生成一个新的 RSA 密钥对，并将公钥和私钥存储到文件中
    /// </summary>
    /// <param name="publicKeyPath">公钥路径</param>
    /// <param name="privateKeyPath">私钥路径</param>
    public static void GenerateKeys(string publicKeyPath, string privateKeyPath)
    {
        // 保存公钥
        var publicKey = _rsaProvider.ToXmlString(false);
        File.WriteAllText(publicKeyPath, publicKey);

        // 保存私钥
        var privateKey = _rsaProvider.ToXmlString(true);
        File.WriteAllText(privateKeyPath, privateKey);
    }

    /// <summary>
    /// 使用公钥加密数据
    /// </summary>
    /// <param name="plainText">要加密的文本</param>
    /// <returns>加密结果</returns>
    /// <remarks>
    /// 须先调用 GenerateKeys 方法生成密钥对
    /// </remarks>
    public static string Encrypt(string plainText)
    {
        // 检查参数
        ArgumentException.ThrowIfNullOrEmpty(plainText, nameof(plainText));

        // 检查是否已加载密钥对
        if (_rsaProvider.PublicOnly)
        {
            throw new InvalidOperationException("请先加载密钥对");
        }

        // 加载公钥
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = _rsaProvider.Encrypt(plainBytes, RSAEncryptionPadding.Pkcs1);
        var encryptedText = Convert.ToBase64String(encryptedBytes);
        return encryptedText;
    }

    #endregion

    #region 解密

    /// <summary>
    /// 加载一个已有的 RSA 密钥对
    /// </summary>
    /// <param name="publicKeyPath">公钥路径</param>
    /// <param name="privateKeyPath">私钥路径</param>
    public static void LoadKeys(string publicKeyPath, string privateKeyPath)
    {
        // 加载公钥
        var publicKey = File.ReadAllText(publicKeyPath);
        _rsaProvider.FromXmlString(publicKey);

        // 加载私钥
        var privateKey = File.ReadAllText(privateKeyPath);
        _rsaProvider.FromXmlString(privateKey);
    }

    /// <summary>
    /// 使用私钥解密数据
    /// </summary>
    /// <param name="encryptedText">要加密的文本</param>
    /// <returns>解密结果</returns>
    /// <remarks>
    /// 须先调用 LoadKeys 方法加载密钥对
    /// </remarks>
    public static string Decrypt(string encryptedText)
    {
        // 检查参数
        ArgumentException.ThrowIfNullOrEmpty(encryptedText, nameof(encryptedText));

        // 检查是否已加载密钥对
        if (_rsaProvider.PublicOnly)
        {
            throw new InvalidOperationException("请先加载密钥对");
        }

        var encryptedBytes = Convert.FromBase64String(encryptedText);
        var plainBytes = _rsaProvider.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
        var plainText = Encoding.UTF8.GetString(plainBytes);
        return plainText;
    }

    #endregion
}