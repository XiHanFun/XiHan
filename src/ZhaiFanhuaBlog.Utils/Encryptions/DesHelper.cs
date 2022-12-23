#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DesHelper
// Guid:c80cc02d-888f-4278-83ac-e6d1943633f1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:25:18
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Encryptions;

/// <summary>
/// DES加密解密帮助类
/// </summary>
public static class DesHelper
{
    // 默认密钥向量
    private static readonly byte[] Iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="encode"></param>
    /// <param name="desKey"></param>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string EncryptDes(Encoding encode, string desKey, string inputString)
    {
        var rgbKey = encode.GetBytes(desKey[..16]);
        var rgbIv = Iv;
        var inputByteArray = encode.GetBytes(inputString);
        using var des = DES.Create();
        MemoryStream mStream = new();
        CryptoStream cStream = new(mStream, des.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return Convert.ToBase64String(mStream.ToArray());
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="encode"></param>
    /// <param name="desKey"></param>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string DecryptDes(Encoding encode, string desKey, string inputString)
    {
        var rgbKey = encode.GetBytes(desKey[..16]);
        var rgbIv = Iv;
        var inputByteArray = Convert.FromBase64String(inputString);
        using var des = DES.Create();
        MemoryStream mStream = new();
        CryptoStream cStream = new(mStream, des.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return encode.GetString(mStream.ToArray());
    }
}