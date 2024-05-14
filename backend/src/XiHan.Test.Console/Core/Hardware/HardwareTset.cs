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
using XiHan.Core.System.Text.Json.Serialization;
using XiHan.Core.System.Extensions;

namespace XiHan.Test.Console.Core.Hardware;

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
        SerializeHelper.SerializeTo(BoardHelper.GetBoardInfos()).WriteLineInfo();
    }

    /// <summary>
    /// 核心测试
    /// </summary>
    public static void TestCpu()
    {
        SerializeHelper.SerializeTo(CpuHelper.GetCpuInfos()).WriteLineInfo();
    }

    /// <summary>
    /// 网卡测试
    /// </summary>
    public static void TestNetwork()
    {
        SerializeHelper.SerializeTo(NetworkHelper.GetNetworkInfos()).WriteLineInfo();
    }

    /// <summary>
    /// 磁盘测试
    /// </summary>
    public static void TestDisk()
    {
        SerializeHelper.SerializeTo(DiskHelper.GetDiskInfos()).WriteLineInfo();
    }

    /// <summary>
    /// 内存测试
    /// </summary>
    public static void TestRam()
    {
        SerializeHelper.SerializeTo(RamHelper.GetRamInfos()).WriteLineInfo();
    }

    /// <summary>
    /// 系统测试
    /// </summary>
    public static void TestOsPlatform()
    {
        SerializeHelper.SerializeTo(OsPlatformHelper.GetOsPlatformInfos()).WriteLineInfo();
    }
}