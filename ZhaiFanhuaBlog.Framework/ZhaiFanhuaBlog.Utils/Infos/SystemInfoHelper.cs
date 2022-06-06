// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SystemInfoHelper
// Guid:78a183f7-8c40-40af-a3f0-7e9bff93392b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:53:15
// ----------------------------------------------------------------

using System.Runtime.InteropServices;
using ZhaiFanhuaBlog.Utils.Formats;

namespace ZhaiFanhuaBlog.Utils.Infos;

/// <summary>
/// 系统信息帮助类
/// </summary>
public static class SystemInfoHelper
{
    /// <summary>
    /// 操作系统
    /// </summary>
    public static string OperatingSystem => GetOperatingSystem().ToString();

    /// <summary>
    /// 系统描述
    /// </summary>
    public static string OSDescription => RuntimeInformation.OSDescription.ToString();

    /// <summary>
    /// 系统版本
    /// </summary>
    public static string OSVersion => Environment.OSVersion.Version.ToString();

    /// <summary>
    /// 系统平台
    /// </summary>
    public static string Platform => Environment.OSVersion.Platform.ToString();

    /// <summary>
    /// 系统架构
    /// </summary>
    public static string OSArchitecture => RuntimeInformation.OSArchitecture.ToString();

    /// <summary>
    /// CPU个数
    /// </summary>
    public static string ProcessorCount => Environment.ProcessorCount.ToString();

    /// <summary>
    /// 系统目录
    /// </summary>
    public static string SystemDirectory => Environment.SystemDirectory.ToString();

    /// <summary>
    /// 磁盘分区
    /// </summary>
    public static string DiskPartition => string.Join("；", Environment.GetLogicalDrives()).ToString();

    /// <summary>
    /// 运行时间
    /// </summary>
    public static string RunningTime => TimeFormatHelper.ParseTimeTicks(Math.Abs(Environment.TickCount)).ToString();

    /// <summary>
    /// 交互模式
    /// </summary>
    public static string InteractiveMode => (Environment.UserInteractive == true ? "交互运行" : "非交互运行").ToString();

    /// <summary>
    /// 操作系统
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static OSPlatform GetOperatingSystem()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OSPlatform.OSX;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OSPlatform.Linux;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OSPlatform.Windows;
        }
        throw new Exception("Cannot determine operating system!");
    }
}