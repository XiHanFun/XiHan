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
    /// 获取内存信息
    /// </summary>
    /// <returns></returns>
    public static List<RamInfo> GetRamInfos()
    {
        var ramInfos = new List<RamInfo>();

        try
        {
            // 单位是 Byte
            var totalMemoryParts = 0.ParseToLong();
            var usedMemoryParts = 0.ParseToLong();
            var freeMemoryParts = 0.ParseToLong();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var output = ShellHelper.Bash("free -k | awk '{print $2,$3,$4,$7}'");
                var lines = output.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
                if (lines.Any())
                {
                    lines = lines[1].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                    totalMemoryParts = lines[0].ParseToLong() * 1024;
                    freeMemoryParts = lines[2].ParseToLong() * 1024;
                    usedMemoryParts = lines[1].ParseToLong() * 1024;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                var output = ShellHelper.Bash("top -l 1 | head -n 7 | tail -n 1 | awk '{print $2,$4,$6,$8}'");
                var lines = output.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (lines.Any())
                {
                    var usedMemoryParts1 = lines[1].Replace('(', (char)StringSplitOptions.RemoveEmptyEntries).Replace('M', (char)StringSplitOptions.RemoveEmptyEntries).ParseToLong();
                    var usedMemoryParts2 = lines[1].Replace('M', (char)StringSplitOptions.RemoveEmptyEntries).ParseToLong();

                    totalMemoryParts = lines[0].Replace('G', (char)StringSplitOptions.RemoveEmptyEntries).ParseToLong() * 1024 * 1024 * 1024;
                    freeMemoryParts = lines[3].Replace('M', (char)StringSplitOptions.RemoveEmptyEntries).ParseToLong() * 1024 * 1024;
                    usedMemoryParts = (usedMemoryParts1 + usedMemoryParts2) * 1024 * 1024;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var output = ShellHelper.Cmd("wmic", "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value");
                var lines = output.Trim().Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
                if (lines.Any())
                {
                    totalMemoryParts = lines[1].Split('=', (char)StringSplitOptions.RemoveEmptyEntries)[1].ParseToLong() * 1024;
                    freeMemoryParts = lines[0].Split('=', (char)StringSplitOptions.RemoveEmptyEntries)[1].ParseToLong() * 1024;
                    usedMemoryParts = totalMemoryParts - freeMemoryParts;
                }
            }
            var ramInfo = new RamInfo
            {
                TotalSpace = totalMemoryParts.FormatByteToString(),
                UsedSpace = usedMemoryParts.FormatByteToString(),
                FreeSpace = freeMemoryParts.FormatByteToString(),
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