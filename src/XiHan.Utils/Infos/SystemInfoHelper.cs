﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SystemInfoHelper
// Guid:78a183f7-8c40-40af-a3f0-7e9bff93392b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:53:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net.NetworkInformation;
using XiHan.Utils.Infos.BaseInfos;

namespace XiHan.Utils.Infos;

/// <summary>
/// 系统信息帮助类
/// </summary>
public static class SystemInfoHelper
{
    /// <summary>
    /// 操作系统
    /// </summary>
    public static string OperatingSystem => OSPlatformHelper.GetOperatingSystem().ToString();

    /// <summary>
    /// 系统描述
    /// </summary>
    public static string OsDescription => OSPlatformHelper.GetOsDescription();

    /// <summary>
    /// 系统版本
    /// </summary>
    public static string OsVersion => OSPlatformHelper.GetOsVersion();

    /// <summary>
    /// 系统平台
    /// </summary>
    public static string Platform => OSPlatformHelper.GetPlatform();

    /// <summary>
    /// 系统架构
    /// </summary>
    public static string OsArchitecture => OSPlatformHelper.GetOsArchitecture();

    /// <summary>
    /// 系统目录
    /// </summary>
    public static string SystemDirectory => OSPlatformHelper.GetSystemDirectory();

    /// <summary>
    /// 运行时间
    /// </summary>
    public static string RunningTime => OSPlatformHelper.GetRunningTime();

    /// <summary>
    /// 交互模式
    /// </summary>
    public static string InteractiveMode => OSPlatformHelper.GetInteractiveMode();

    /// <summary>
    /// 处理器信息
    /// </summary>
    /// <returns></returns>
    public static CpuInfo CpuInfo => CpuHelper.GetCpuInfos();

    /// <summary>
    /// 内存信息
    /// </summary>
    /// <returns></returns>
    public static RamInfo RamInfo => RamHelper.GetRamInfos();

    /// <summary>
    /// 磁盘信息
    /// </summary>
    /// <returns></returns>
    public static List<DiskInfo> DiskInfo => DiskHelper.GetDiskInfos();

    /// <summary>
    /// 网卡信息
    /// </summary>
    public static List<NetworkInfo> NetworkInfo => NetworkHelper.GetNetworkInfos();
}