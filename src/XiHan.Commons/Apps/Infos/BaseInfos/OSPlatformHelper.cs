#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:OsPlatformHelper
// Guid:d404f006-9a93-45b2-b33b-8ec201355621
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-09 上午 06:49:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Runtime.InteropServices;
using XiHan.Utils.Formats;
using XiHan.Utils.Objects;

namespace XiHan.Commons.Infos.BaseInfos;

/// <summary>
/// 操作系统帮助类
/// </summary>
public static class OsPlatformHelper
{
    /// <summary>
    /// 操作系统
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetOperatingSystem()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OSPlatform.OSX.ToString();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OSPlatform.Linux.ToString();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OSPlatform.Windows.ToString();
        }

        throw new Exception("Cannot determine operating system!");
    }

    /// <summary>
    /// 是否 Unix 系统
    /// </summary>
    /// <returns></returns>
    public static bool GetOsIsUnix()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }

    /// <summary>
    /// 系统描述
    /// </summary>
    public static string GetOsDescription()
    {
        return RuntimeInformation.OSDescription;
    }

    /// <summary>
    /// 系统版本
    /// </summary>
    public static string GetOsVersion()
    {
        return Environment.OSVersion.Version.ToString();
    }

    /// <summary>
    /// 系统平台
    /// </summary>
    public static string GetPlatform()
    {
        return Environment.OSVersion.Platform.ToString();
    }

    /// <summary>
    /// 系统架构
    /// </summary>
    public static string GetOsArchitecture()
    {
        return RuntimeInformation.OSArchitecture.ToString();
    }

    /// <summary>
    /// 系统目录
    /// </summary>
    public static string GetSystemDirectory()
    {
        return Environment.SystemDirectory;
    }

    /// <summary>
    /// 运行时间
    /// </summary>
    public static string GetRunningTime()
    {
        return Environment.TickCount.ParseToLong().FormatMilliSecondsToString();
    }

    /// <summary>
    /// 交互模式
    /// </summary>
    public static string GetInteractiveMode()
    {
        return Environment.UserInteractive ? "交互运行" : "非交互运行";
    }
}