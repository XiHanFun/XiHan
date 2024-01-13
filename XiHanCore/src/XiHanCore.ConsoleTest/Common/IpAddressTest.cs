#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IpAddressTest
// Guid:21d6fa55-a9df-4ae0-acf5-940d4c82c2da
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-07-22 下午 01:48:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net;

namespace XiHanCore.ConsoleTest.Common;

/// <summary>
/// TestIpAddress
/// </summary>
public static class IpAddressTest
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
}