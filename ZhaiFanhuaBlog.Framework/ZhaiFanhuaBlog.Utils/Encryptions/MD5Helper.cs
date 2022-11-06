// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MD5Helper
// Guid:c77bf375-d1f2-4a68-b4d1-bb2affcc0aa8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:26:25
// ----------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.Encryptions;

/// <summary>
/// MD5Helper加密帮助类
/// </summary>
public static class MD5Helper
{
    /// <summary>
    /// MD5字符串加密
    /// </summary>
    /// <param name="encode">编码</param>
    /// <param name="inputString">输入</param>
    /// <returns>加密后字符串</returns>
    public static string EncryptMD5(Encoding encode, string inputString)
    {
        try
        {
            using MD5 md5 = MD5.Create();
            byte[] buffer = encode.GetBytes(inputString);
            //开始加密
            byte[] byteBuffer = md5.ComputeHash(buffer);
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
    /// MD5流加密
    /// </summary>
    /// <param name="inputPath">文件路径</param>
    /// <returns></returns>
    public static string EncryptMD5(string inputPath)
    {
        try
        {
            using FileStream stream = new(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            using MD5 md5 = MD5.Create();
            //开始加密
            byte[] buffer = md5.ComputeHash(stream);
            StringBuilder strBuild = new();
            foreach (byte buff in buffer)
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
    /// MD5字符串加密扩展方法
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static string ToMD5(this string inputString)
    {
        return EncryptMD5(Encoding.UTF8, inputString);
    }
}