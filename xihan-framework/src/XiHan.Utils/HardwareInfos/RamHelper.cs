#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RamHelper
// Guid:93baae04-c99a-4095-b5ab-9f14e2a64c97
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-09 上午 06:09:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Runtime.InteropServices;
using XiHan.Utils.Extensions;
using XiHan.Utils.Shells;

namespace XiHan.Utils.HardwareInfos;

/// <summary>
/// 内存帮助类
/// </summary>
public static class RamHelper
{
    /// <summary>
    /// 内存信息
    /// </summary>
    /// <returns></returns>
    public static List<RamInfo> RamInfos => GetRamInfos();

    /// <summary>
    /// 获取内存信息
    /// </summary>
    /// <returns></returns>
    public static List<RamInfo> GetRamInfos()
    {
        List<RamInfo> ramInfos = new();

        try
        {
            // 单位是 Byte
            long totalMemoryParts = 0.ParseToLong();
            long usedMemoryParts = 0.ParseToLong();
            long freeMemoryParts = 0.ParseToLong();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string output = ShellHelper.Bash("free -k | tail -n +2| head -n +1 | awk '{print $2,$3,$4,$7}'").Trim();
                string[] lines = output.Split(' ', (char)StringSplitOptions.None);
                if (lines.Any())
                {
                    totalMemoryParts = lines[0].ParseToLong() * 1024;
                    freeMemoryParts = lines[2].ParseToLong() * 1024;
                    usedMemoryParts = lines[1].ParseToLong() * 1024;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                string output = ShellHelper.Bash("top -l 1 | head -n +7 | tail -n -1 | awk '{print $2,$4,$6,$8}'").Trim();
                string[] lines = output.Split(' ', (char)StringSplitOptions.None);
                if (lines.Any())
                {
                    long usedMemoryParts1 = lines[1].Replace('(', (char)StringSplitOptions.None).Replace('M', (char)StringSplitOptions.None).ParseToLong();
                    long usedMemoryParts2 = lines[1].Replace('M', (char)StringSplitOptions.None).ParseToLong();

                    totalMemoryParts = lines[0].Replace('G', (char)StringSplitOptions.None).ParseToLong() * 1024 * 1024 * 1024;
                    freeMemoryParts = lines[3].Replace('M', (char)StringSplitOptions.None).ParseToLong() * 1024 * 1024;
                    usedMemoryParts = (usedMemoryParts1 + usedMemoryParts2) * 1024 * 1024;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string output = ShellHelper.Cmd("wmic", "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value").Trim();
                string[] lines = output.Split('\n', (char)StringSplitOptions.None);
                if (lines.Any())
                {
                    totalMemoryParts = lines[1].Split('=', (char)StringSplitOptions.None)[1].ParseToLong() * 1024;
                    freeMemoryParts = lines[0].Split('=', (char)StringSplitOptions.None)[1].ParseToLong() * 1024;
                    usedMemoryParts = totalMemoryParts - freeMemoryParts;
                }
            }
            RamInfo ramInfo = new()
            {
                TotalSpace = totalMemoryParts.FormatFileSizeToString(),
                UsedSpace = usedMemoryParts.FormatFileSizeToString(),
                FreeSpace = freeMemoryParts.FormatFileSizeToString(),
                AvailableRate = totalMemoryParts == 0
                           ? "0%"
                           : Math.Round((decimal)freeMemoryParts / totalMemoryParts * 100, 3) + "%"
            };
            ramInfos.Add(ramInfo);
        }
        catch (Exception ex)
        {
            ("获取内存信息出错，" + ex.Message).WriteLineError();
        }

        return ramInfos;
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