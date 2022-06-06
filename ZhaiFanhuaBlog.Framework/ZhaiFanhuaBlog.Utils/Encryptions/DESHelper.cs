// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DESHelper
// Guid:c80cc02d-888f-4278-83ac-e6d1943633f1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:25:18
// ----------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Encryptions;

/// <summary>
/// DES加密解密帮助类
/// </summary>
public static class DESHelper
{
    // 默认加密秘钥
    private const string desKey = "ZhaiFanhuaBlogDESHelper";

    // 默认密钥向量
    private static readonly byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">待加密的字符串</param>
    /// <returns></returns>
    public static string EncryptDES(Encoding encode, string inputString)
    {
        try
        {
            byte[] rgbKey = encode.GetBytes(desKey[..8]);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = encode.GetBytes(inputString);
            using DES des = DES.Create();
            MemoryStream mStream = new();
            CryptoStream cStream = new(mStream, des.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">待解密的字符串</param>
    /// <returns></returns>
    public static string DecryptDES(Encoding encode, string inputString)
    {
        try
        {
            byte[] rgbKey = encode.GetBytes(desKey[..8]);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(inputString);
            using DES des = DES.Create();
            MemoryStream mStream = new();
            CryptoStream cStream = new(mStream, des.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return encode.GetString(mStream.ToArray());
        }
        catch (Exception)
        {
            throw;
        }
    }
}