#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:RsaEncryptionHelper
// Guid:fa690f78-718e-4573-9710-aa74828385a6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 下午 12:09:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// RSA加密解密类
/// </summary>
public static class RsaEncryptionHelper
{
    // Rsa容器
    private static readonly RSACryptoServiceProvider RsaProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    static RsaEncryptionHelper()
    {
        RsaProvider = new RSACryptoServiceProvider
        {
            PersistKeyInCsp = false
        };
    }

    /// <summary>
    /// 生成一个新的RSA密钥对，并将公钥和私钥存储到文件中
    /// </summary>
    /// <param name="publicKeyFile"></param>
    /// <param name="privateKeyFile"></param>
    public static void GenerateKeys(string publicKeyFile, string privateKeyFile)
    {
        using var rsaProvider = new RSACryptoServiceProvider();
        // 保存公钥
        string publicKey = rsaProvider.ToXmlString(false);
        File.WriteAllText(publicKeyFile, publicKey);

        // 保存私钥
        string privateKey = rsaProvider.ToXmlString(true);
        File.WriteAllText(privateKeyFile, privateKey);
    }

    /// <summary>
    /// 加载一个已有的RSA密钥对
    /// </summary>
    /// <param name="publicKeyFile"></param>
    /// <param name="privateKeyFile"></param>
    public static void LoadKeys(string publicKeyFile, string privateKeyFile)
    {
        // 加载公钥
        string publicKey = File.ReadAllText(publicKeyFile);
        RsaProvider.FromXmlString(publicKey);

        // 加载私钥
        string privateKey = File.ReadAllText(privateKeyFile);
        RsaProvider.FromXmlString(privateKey);
    }

    /// <summary>
    /// 使用公钥加密数据
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public static string Encrypt(string plainText)
    {
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] encryptedBytes = RsaProvider.Encrypt(plainBytes, RSAEncryptionPadding.Pkcs1);
        string encryptedText = Convert.ToBase64String(encryptedBytes);
        return encryptedText;
    }

    /// <summary>
    /// 使用私钥解密数据
    /// </summary>
    /// <param name="encryptedText"></param>
    /// <returns></returns>
    public static string Decrypt(string encryptedText)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        byte[] plainBytes = RsaProvider.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
        string plainText = Encoding.UTF8.GetString(plainBytes);
        return plainText;
    }
}