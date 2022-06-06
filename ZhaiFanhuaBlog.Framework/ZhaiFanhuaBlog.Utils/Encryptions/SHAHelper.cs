// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SHAHelper
// Guid:871e648a-233c-41fd-8531-de4ce3a3820f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:28:31
// ----------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Encryptions;

/// <summary>
/// SHAHelper加密帮助类
/// </summary>

public static class SHAHelper
{
    /// <summary>
    /// SHA1字符串加密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">输入</param>
    /// <returns>加密后字符串160位</returns>
    public static string EncryptSHA1(Encoding encode, string inputString)
    {
        try
        {
            using SHA1 sha1 = SHA1.Create();
            byte[] buffer = encode.GetBytes(inputString);
            //开始加密
            byte[] byteBuffer = sha1.ComputeHash(buffer);
            StringBuilder strBuild = new();
            foreach (byte buff in byteBuffer)
            {
                strBuild.Append(buff.ToString("x2"));
            }
            return strBuild.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// SHA256字符串加密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">输入</param>
    /// <returns>加密后字符串256位</returns>
    public static string EncryptSHA256(Encoding encode, string inputString)
    {
        try
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] buffer = encode.GetBytes(inputString);
            //开始加密
            byte[] byteBuffer = sha256.ComputeHash(buffer);
            StringBuilder strBuild = new();
            foreach (byte buff in byteBuffer)
            {
                strBuild.Append(buff.ToString("x2"));
            }
            return strBuild.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// SHA384字符串加密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">输入</param>
    /// <returns>加密后字符串384位</returns>
    public static string EncryptSHA384(Encoding encode, string inputString)
    {
        try
        {
            using SHA384 sha384 = SHA384.Create();
            byte[] buffer = encode.GetBytes(inputString);
            //开始加密
            byte[] byteBuffer = sha384.ComputeHash(buffer);
            StringBuilder strBuild = new();
            foreach (byte buff in byteBuffer)
            {
                strBuild.Append(buff.ToString("x2"));
            }
            return strBuild.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// SHA512字符串加密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">输入</param>
    /// <returns>加密后字符串512位</returns>
    public static string EncryptSHA512(Encoding encode, string inputString)
    {
        try
        {
            using SHA512 sha512 = SHA512.Create();
            byte[] buffer = encode.GetBytes(inputString);
            //开始加密
            byte[] byteBuffer = sha512.ComputeHash(buffer);
            StringBuilder strBuild = new();
            foreach (byte buff in byteBuffer)
            {
                strBuild.Append(buff.ToString("x2"));
            }
            return strBuild.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }
}