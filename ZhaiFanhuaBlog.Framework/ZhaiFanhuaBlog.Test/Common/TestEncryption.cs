// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestEncryption
// Guid:3bf38913-bf49-4796-aa60-a4f18646a898
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 上午 02:12:48
// ----------------------------------------------------------------

using System.Text;
using ZhaiFanhuaBlog.Utils.Encryptions;

namespace ZhaiFanhuaBlog.Test.Common;

/// <summary>
/// 测试加密
/// </summary>
[TestClass]
public static class TestEncryption
{
    /// <summary>
    /// 加密
    /// </summary>
    [TestMethod]
    public static void Encryption()
    {
        string str = @"123456";
        Console.WriteLine($@"字符串【{str}】MD5加密后：{MD5Helper.EncryptMD5(Encoding.UTF8, str)}；");

        string file = @"D:\Blog\餐饮企业订单信息源数据.zip";
        Console.WriteLine($@"文件【{file}】MD5加密后：{MD5Helper.EncryptMD5(file)}；");
    }
}