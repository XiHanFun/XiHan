#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:TestIpAddress
// Guid:21d6fa55-a9df-4ae0-acf5-940d4c82c2da
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 下午 01:48:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net;
using XiHan.Utils.Info.BaseInfos;

namespace XiHan.Test.Common;

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
        var address = new byte[] { 127, 0, 0, 1 };
        IPAddress iPAddress = new(address);
        Console.WriteLine(iPAddress.ToString());
    }

    /// <summary>
    /// 本地ip
    /// </summary>
    public static void LocalIp()
    {
        Console.WriteLine("【IpV4】" + LocalIpHelper.GetLocalIpV4());
        Console.WriteLine("【IpV6】" + LocalIpHelper.GetLocalIpV6());
    }
}