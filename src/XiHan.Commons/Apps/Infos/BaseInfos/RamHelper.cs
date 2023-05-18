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

using XiHan.Utils.Consoles;
using XiHan.Utils.Formats;
using XiHan.Utils.Objects;
using XiHan.Utils.Shells;

namespace XiHan.Commons.Infos.BaseInfos;

/// <summary>
/// 内存帮助类
/// </summary>
public static class RamHelper
{
    /// <summary>
    /// Windows 系统获取内存信息
    /// </summary>
    /// <returns></returns>
    public static RamInfo GetWindowsRam()
    {
        var ramInfo = new RamInfo();
        try
        {
            string output = ShellHelper.Cmd("wmic", "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value");
            var lines = output.Trim().Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            if (lines.Any())
            {
                // 单位是 KB
                var freeMemoryParts = lines[0].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
                var totalMemoryParts = lines[1].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);

                ramInfo.TotalSpace = (totalMemoryParts[1].ParseToLong() * 1024).FormatByteToString();
                ramInfo.UsedSpace = ((totalMemoryParts[1].ParseToLong() - freeMemoryParts[1].ParseToLong()) * 1024).FormatByteToString();
                ramInfo.FreeSpace = (freeMemoryParts[1].ParseToLong() * 1024).FormatByteToString();
                ramInfo.AvailableRate = totalMemoryParts[1].ParseToLong() == 0 ? "0%" : Math.Round((decimal)freeMemoryParts[1].ParseToLong() / totalMemoryParts[1].ParseToLong() * 100, 3) + "%";
            }
        }
        catch (Exception ex)
        {
            ("获取内存信息出错，" + ex.Message).WriteLineError();
        }
        return ramInfo;
    }

    /// <summary>
    /// Unix 系统获取内存信息
    /// </summary>
    /// <returns></returns>
    public static RamInfo GetUnixRam()
    {
        var ramInfo = new RamInfo();
        try
        {
            string output = ShellHelper.Bash("free -k | awk '{print $2,$3,$4,$7}'");
            var lines = output.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            if (lines.Any())
            {
                // 单位是 KB
                var memory = lines[1].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (memory.Length >= 4)
                {
                    ramInfo.TotalSpace = (memory[0].ParseToLong() * 1024).FormatByteToString();
                    ramInfo.UsedSpace = (memory[1].ParseToLong() * 1024).FormatByteToString();
                    ramInfo.FreeSpace = (memory[2].ParseToLong() * 1024).FormatByteToString();
                    ramInfo.AvailableRate = memory[0].ParseToLong() == 0 ? "0%" : Math.Round((decimal)memory[3].ParseToLong() / memory[0].ParseToLong() * 100, 3) + "%";
                }
            }
        }
        catch (Exception ex)
        {
            ("获取内存信息出错，" + ex.Message).WriteLineError();
        }
        return ramInfo;
    }

    /// <summary>
    /// 获取内存信息
    /// </summary>
    /// <returns></returns>
    public static RamInfo GetRamInfos()
    {
        if (OsPlatformHelper.GetOsIsUnix())
        {
            return GetUnixRam();
        }
        else
        {
            return GetWindowsRam();
        }
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