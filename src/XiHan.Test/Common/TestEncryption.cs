#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestEncryption
// Guid:3bf38913-bf49-4796-aa60-a4f18646a898
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 上午 02:12:48
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text;
using XiHan.Utils.Encryptions;

namespace XiHan.Test.Common;

/// <summary>
/// 测试加密
/// </summary>
public static class TestEncryption
{
    /// <summary>
    /// 加密
    /// </summary>
    public static void Encryption()
    {
        var str = @"123456";
        Console.WriteLine($@"字符串【{str}】MD5加密后：{str.Md5Encrypt(Encoding.UTF8)}；");

        var file = @"D:\Blog\餐饮企业订单信息源数据.zip";
        Console.WriteLine($@"文件【{file}】MD5加密后：{Md5Extensions.Md5Encrypt(file)}；");
    }
}