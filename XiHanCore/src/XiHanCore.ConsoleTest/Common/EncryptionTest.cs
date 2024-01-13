#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:EncryptionTest
// Guid:3bf38913-bf49-4796-aa60-a4f18646a898
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-06-30 上午 02:12:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHanCore.Utils.Encryptions;

namespace XiHanCore.ConsoleTest.Common;

/// <summary>
/// 加密测试
/// </summary>
public static class EncryptionTest
{
    /// <summary>
    /// Des
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public static void EncryptionDes()
    {
        Console.WriteLine("请输入要Des加密的文本：");
        var plainText = Console.ReadLine();
        var strDes1 = plainText != null ? DesEncryptionHelper.Encrypt(plainText) : throw new ArgumentNullException();
        Console.WriteLine($@"字符串【{plainText}】Des加密后：{strDes1}；");

        Console.WriteLine("请输入要Des解密的文本：");
        var cipherText = Console.ReadLine();
        var strDes2 = cipherText != null ? DesEncryptionHelper.Decrypt(cipherText) : throw new ArgumentNullException();
        Console.WriteLine($@"字符串【{cipherText}】Des解密后：{strDes2}；");
    }

    /// <summary>
    /// Aes
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public static void EncryptionAes()
    {
        Console.WriteLine("请输入要Aes加密的文本：");
        var plainText = Console.ReadLine();
        Console.WriteLine("请输入要Aes加密的密码：");
        var password1 = Console.ReadLine();
        var strAes1 = plainText != null && password1 != null
            ? AesEncryptionHelper.Encrypt(plainText, password1)
            : throw new ArgumentNullException();
        Console.WriteLine($@"字符串【{plainText}】Aes加密后：{strAes1}；");

        Console.WriteLine("请输入要Aes解密的文本：");
        var cipherText = Console.ReadLine();
        Console.WriteLine("请输入要Aes解密的密码：");
        var password2 = Console.ReadLine();
        var strAes2 = cipherText != null && password2 != null
            ? AesEncryptionHelper.Decrypt(cipherText, password2)
            : throw new ArgumentNullException();
        Console.WriteLine($@"字符串【{cipherText}】Aes解密后：{strAes2}；");
    }

    /// <summary>
    /// Md5
    /// </summary>
    public static void EncryptionMd5()
    {
        Console.WriteLine("请输入要Md5加密的文本：");
        var plainText = Console.ReadLine();
        var strMd5 = plainText != null ? Md5HashEncryptionHelper.Encrypt(plainText) : throw new ArgumentNullException();
        Console.WriteLine($@"字符串【{plainText}】Md5加密后：{strMd5}；");

        Console.WriteLine("请输入要Md5加密的文件路径：");
        var file = Console.ReadLine();
        var strMd5File = file != null ? Md5HashEncryptionHelper.EncryptStream(file) : throw new ArgumentNullException();
        Console.WriteLine($@"文件【{file}】Md5加密后：{strMd5File}；");
    }

    /// <summary>
    /// Sha
    /// </summary>
    public static void EncryptionSha()
    {
        Console.WriteLine("请输入要Sha加密的文本：");
        var plainText = Console.ReadLine();
        var strSha1 = plainText != null ? ShaHashEncryptionHelper.Sha1(plainText) : throw new ArgumentNullException();
        var strSha256 = plainText != null
            ? ShaHashEncryptionHelper.Sha256(plainText)
            : throw new ArgumentNullException();
        var strSha384 = plainText != null
            ? ShaHashEncryptionHelper.Sha384(plainText)
            : throw new ArgumentNullException();
        var strSha512 = plainText != null
            ? ShaHashEncryptionHelper.Sha512(plainText)
            : throw new ArgumentNullException();
        Console.WriteLine($@"字符串【{plainText}】Sha1加密后：{strSha1}；");
        Console.WriteLine($@"字符串【{plainText}】Sha256加密后：{strSha256}；");
        Console.WriteLine($@"字符串【{plainText}】Sha384加密后：{strSha384}；");
        Console.WriteLine($@"字符串【{plainText}】Sha512加密后：{strSha512}；");
    }

    /// <summary>
    /// Rsa
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public static void EncryptionRsa()
    {
        Console.WriteLine("请输入要Rsa加密的公钥路径：(如：publicKey.xml)");
        var publicKey = Console.ReadLine();
        Console.WriteLine("请输入要Rsa加密的私钥路径：(如：privateKey.xml)");
        var privateKey = Console.ReadLine();
        Console.WriteLine("请输入要Rsa加密的文本：");
        var plainText = Console.ReadLine();
        if (publicKey == null || privateKey == null || plainText == null) throw new ArgumentNullException();

        RsaEncryptionHelper.GenerateKeys(publicKey, privateKey);
        var strRsa1 = RsaEncryptionHelper.Encrypt(plainText);
        Console.WriteLine($@"字符串【{plainText}】Rsa加密后：{strRsa1}；");

        Console.WriteLine("请输入要Rsa解密的文本：");
        var encryptedText = Console.ReadLine() ?? throw new ArgumentNullException();
        RsaEncryptionHelper.LoadKeys(publicKey, privateKey);
        var strRsa2 = RsaEncryptionHelper.Decrypt(encryptedText);
        Console.WriteLine($@"字符串【{plainText}】Rsa解密后：{strRsa2}；");
    }
}