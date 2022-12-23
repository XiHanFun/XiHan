#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AesHelper
// Guid:14326a53-9e3d-43c6-a57a-bc13dd9b0c2a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:23:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Encryptions;

/// <summary>
/// AES加密解密帮助类
/// </summary>
public static class AesHelper
{
    // 默认密钥向量
    private static readonly byte[] Iv = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

    /// <summary>
    /// AES加密
    /// </summary>
    /// <param name="encode"></param>
    /// <param name="aesKey"></param>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string EncryptAes(Encoding encode, string aesKey, string inputString)
    {
        var rgbKey = encode.GetBytes(aesKey[..16]);
        var rgbIv = Iv;
        var inputByteArray = encode.GetBytes(inputString);
        using var aes = Aes.Create();
        using MemoryStream mStream = new();
        using CryptoStream cStream = new(mStream, aes.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return Convert.ToBase64String(mStream.ToArray());
    }

    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="encode"></param>
    /// <param name="aesKey"></param>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string DecryptAes(Encoding encode, string aesKey, string inputString)
    {
        var rgbKey = encode.GetBytes(aesKey[..16]);
        var rgbIv = Iv;
        var inputByteArray = Convert.FromBase64String(inputString);
        using var aes = Aes.Create();
        using MemoryStream mStream = new(inputByteArray);
        using CryptoStream cStream = new(mStream, aes.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Read);
        using StreamReader sReader = new(cStream);
        var result = sReader.ReadToEnd();
        return result;
    }
}