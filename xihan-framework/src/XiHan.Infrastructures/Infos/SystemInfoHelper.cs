#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SystemInfoHelper
// Guid:78a183f7-8c40-40af-a3f0-7e9bff93392b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 03:53:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.HardwareInfos;

namespace XiHan.Infrastructures.Infos;

/// <summary>
/// 系统信息帮助类
/// </summary>
public static class SystemInfoHelper
{
    /// <summary>
    /// 操作系统
    /// </summary>
    public static string OperatingSystem => OsPlatformHelper.OperatingSystem;

    /// <summary>
    /// 系统描述
    /// </summary>
    public static string OsDescription => OsPlatformHelper.OsDescription;

    /// <summary>
    /// 系统版本
    /// </summary>
    public static string OsVersion => OsPlatformHelper.OsVersion;

    /// <summary>
    /// 系统平台
    /// </summary>
    public static string Platform => OsPlatformHelper.Platform;

    /// <summary>
    /// 系统架构
    /// </summary>
    public static string OsArchitecture => OsPlatformHelper.OsArchitecture;

    /// <summary>
    /// 系统目录
    /// </summary>
    public static string SystemDirectory => OsPlatformHelper.SystemDirectory;

    /// <summary>
    /// 运行时间
    /// </summary>
    public static string RunningTime => OsPlatformHelper.RunningTime;

    /// <summary>
    /// 交互模式
    /// </summary>
    public static string InteractiveMode => OsPlatformHelper.InteractiveMode;

    /// <summary>
    /// 处理器信息
    /// </summary>
    public static CpuInfo CpuInfo => CpuHelper.CpuInfos;

    /// <summary>
    /// 内存信息
    /// </summary>
    public static List<RamInfo> RamInfo => RamHelper.RamInfos;

    /// <summary>
    /// 磁盘信息
    /// </summary>
    public static List<DiskInfo> DiskInfo => DiskHelper.DiskInfos;

    /// <summary>
    /// 网卡信息
    /// </summary>
    public static List<NetworkInfo> NetworkInfo => NetworkHelper.NetworkInfos;
}