#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:Md5HashEncryptionHelper
// Guid:21e9cb49-385d-4549-ad4e-1fcfd56b3472
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-15 上午 11:57:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;

namespace XiHan.Utils.Encryptions;

/// <summary>
/// Md5 生成哈希类
/// </summary>
public static class Md5HashEncryptionHelper
{
    /// <summary>
    /// 对字符串进行 MD5 生成哈希
    /// </summary>
    /// <param name="input">待加密的明文字符串</param>
    /// <returns>生成的哈希值</returns>
    public static string Encrypt(string input)
    {
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hashBytes);
    }

    /// <summary>
    /// 对数据流进行 MD5 生成哈希
    /// </summary>
    /// <param name="inputPath">待加密的数据流路径</param>
    /// <returns>生成的哈希值</returns>
    public static string EncryptStream(string inputPath)
    {
        using FileStream stream = new(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var hashBytes = MD5.HashData(stream);
        return Convert.ToHexString(hashBytes);
    }
}