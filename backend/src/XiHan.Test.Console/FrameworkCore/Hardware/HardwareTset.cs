#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HardwareTset
// Guid:2b3fb46f-c787-42fb-9e4f-dabb75a7e64e
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-05-13 下午 04:37:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Utilities.HardwareInfos;
using XiHan.Framework.Core.System.Extensions;
using XiHan.Framework.Core.System.Text.Json.Serialization;

namespace XiHan.Test.Console.FrameworkCore.Hardware;

/// <summary>
/// HardwareTset
/// </summary>
public class HardwareTset
{
    /// <summary>
    /// 主板测试
    /// </summary>
    public static void TestBoard()
    {
        BoardHelper.GetBoardInfos().SerializeTo().WriteLineInfo();
    }

    /// <summary>
    /// 核心测试
    /// </summary>
    public static void TestCpu()
    {
        CpuHelper.GetCpuInfos().SerializeTo().WriteLineInfo();
    }

    /// <summary>
    /// 网卡测试
    /// </summary>
    public static void TestNetwork()
    {
        NetworkHelper.GetNetworkInfos().SerializeTo().WriteLineInfo();
    }

    /// <summary>
    /// 磁盘测试
    /// </summary>
    public static void TestDisk()
    {
        DiskHelper.GetDiskInfos().SerializeTo().WriteLineInfo();
    }

    /// <summary>
    /// 内存测试
    /// </summary>
    public static void TestRam()
    {
        RamHelper.GetRamInfos().SerializeTo().WriteLineInfo();
    }

    /// <summary>
    /// 系统测试
    /// </summary>
    public static void TestOsPlatform()
    {
        OsPlatformHelper.GetOsPlatformInfos().SerializeTo().WriteLineInfo();
    }
}