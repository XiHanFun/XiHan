// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestIpAddress
// Guid:21d6fa55-a9df-4ae0-acf5-940d4c82c2da
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 下午 01:48:40
// ----------------------------------------------------------------

using System.Net;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.Test.Common;

/// <summary>
/// TestIpAddress
/// </summary>
public static class TestIpAddress
{
    /// <summary>
    /// 转换
    /// </summary>
    public static void ParseIp()
    {
        byte[] address = new byte[4] { 127, 0, 0, 1 };
        IPAddress iPAddress = new(address);
        Console.WriteLine(iPAddress.ToString());
    }

    /// <summary>
    /// 本地ip
    /// </summary>
    public static void LocalIp()
    {
        Console.WriteLine("【IpV4】" + IpInfoHelper.GetLocalIpV4());
        Console.WriteLine("【IpV6】" + IpInfoHelper.GetLocalIpV6());
    }
}