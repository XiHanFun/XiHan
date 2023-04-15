#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:BinaryEncodeHelper
// Guid:60aa070d-c5ac-4d07-9928-8feb121c1bad
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-15 上午 11:03:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;

namespace XiHan.Utils.Encodes;

/// <summary>
/// Binary编码帮助类
/// </summary>
public static class BinaryEncodeHelper
{
    /// <summary>
    /// 将字符串转化为二进制
    /// </summary>
    /// <param name="data">待转换的字符串</param>
    /// <returns>转换后的二进制数组</returns>
    public static byte[] ToBinary(this string data)
    {
        return Encoding.UTF8.GetBytes(data);
    }

    /// <summary>
    /// 将二进制数据转化为字符串
    /// </summary>
    /// <param name="data">待转换的二进制数组</param>
    /// <returns>转换后的字符串</returns>
    public static string FromBinary(this byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }
}