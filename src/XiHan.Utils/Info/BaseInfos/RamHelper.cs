#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:RamHelper
// Guid:93baae04-c99a-4095-b5ab-9f14e2a64c97
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-09 上午 06:09:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Formats;
using XiHan.Utils.Object;
using XiHan.Utils.Shell;

namespace XiHan.Utils.Info.BaseInfos;

/// <summary>
/// 内存帮助类
/// </summary>
public static class RamHelper
{
    /// <summary>
    /// Windows系统获取内存信息
    /// </summary>
    /// <returns></returns>
    public static RamInfo GetWindowsRam()
    {
        string output = "wmic".Cmd("OS get FreePhysicalMemory,TotalVisibleMemorySize /Value");

        var ramInfo = new RamInfo();
        var lines = output.Trim().Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
        if (lines.Any())
        {
            // 单位是 KB
            var freeMemoryParts = lines[0].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);

            ramInfo.TotalSpace = (totalMemoryParts[1].ParseToLong() * 1024).FormatByteToString();
            ramInfo.UsedSpace = ((totalMemoryParts[1].ParseToLong() - freeMemoryParts[1].ParseToLong()) * 1024).FormatByteToString();
            ramInfo.FreeSpace = (freeMemoryParts[1].ParseToLong() * 1024).FormatByteToString();
            ramInfo.AvailableRate = Math.Round((decimal)freeMemoryParts[1].ParseToLong() / totalMemoryParts[1].ParseToLong() * 100, 3) + "%";
        }

        return ramInfo;
    }

    /// <summary>
    /// Unix系统获取内存信息
    /// </summary>
    /// <returns></returns>
    public static RamInfo GetUnixRam()
    {
        string output = "free -m | awk '{print $2,$3,$4,$5,$6}'".Bash();

        var ramInfo = new RamInfo();
        var lines = output.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);

        if (lines.Any())
        {
            var memory = lines[1].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
            if (memory.Length >= 3)
            {
                ramInfo.TotalSpace = memory[0].ParseToLong().FormatByteToString();
                ramInfo.UsedSpace = memory[1].ParseToLong().FormatByteToString();
                ramInfo.FreeSpace = memory[2].ParseToLong().FormatByteToString();
                ramInfo.AvailableRate = Math.Round((decimal)memory[2].ParseToLong() / memory[0].ParseToLong() * 100, 3) + "%";
            }
        }

        return ramInfo;
    }

    /// <summary>
    /// 获取内存信息
    /// </summary>
    /// <returns></returns>
    public static RamInfo GetRamInfos()
    {
        RamInfo ramInfo = new();
        try
        {
            if (OSPlatformHelper.GetOsIsUnix())
            {
                ramInfo = GetUnixRam();
            }
            else
            {
                ramInfo = GetWindowsRam();
            }
        }
        catch (Exception)
        {
            throw;
        }
        return ramInfo;
    }
}

/// <summary>
/// 内存信息
/// </summary>
public class RamInfo
{
    /// <summary>
    /// 总大小
    /// </summary>
    public string TotalSpace { get; set; } = string.Empty;

    /// <summary>
    /// 空闲大小
    /// </summary>
    public string FreeSpace { get; set; } = string.Empty;

    /// <summary>
    /// 已用大小
    /// </summary>
    public string UsedSpace { get; set; } = string.Empty;

    /// <summary>
    /// 可用占比
    /// </summary>
    public string AvailableRate { get; set; } = string.Empty;
}