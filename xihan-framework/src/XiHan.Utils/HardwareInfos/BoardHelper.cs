#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BoardHelper
// Guid:083d66ea-4eeb-4083-811c-65e8d406a818
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-10-20 下午 03:00:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Runtime.InteropServices;
using XiHan.Utils.Extensions;
using XiHan.Utils.Shells;

namespace XiHan.Utils.HardwareInfos;

/// <summary>
/// 主板帮助类
/// </summary>
public static class BoardHelper
{
    /// <summary>
    /// 主板信息
    /// </summary>
    public static BoardInfo BoardInfos => GetBoardInfos();

    /// <summary>
    /// 获取主板信息
    /// </summary>
    /// <returns></returns>
    public static BoardInfo GetBoardInfos()
    {
        BoardInfo boardInfo = new();

        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                //var output = ShellHelper.Bash(@"top -b -n1 | grep ""Cpu(s)""").Trim();
                //string[] lines = output.Split(',', (char)StringSplitOptions.None);
                //if (lines.Any())
                //{
                //    var loadPercentage = lines[3].Trim().Split(' ', (char)StringSplitOptions.None)[0];
                //    cpuInfo.CpuRate = loadPercentage.ParseToLong() + "%";
                //}
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                //var output = ShellHelper.Bash(@"top -l 1 -F | awk '/CPU usage/ {gsub(""%"", """"); print $7}'").Trim();
                //cpuInfo.CpuRate = output.ParseToLong() + "%";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var output = ShellHelper.Cmd("wmic", "baseboard get Product,Manufacturer,SerialNumber /Value").Trim();
                string[] lines = output.Split('\n', (char)StringSplitOptions.None);
                if (lines.Any())
                {
                    var loadProduct = lines.First(s => s.StartsWith("Product")).Split('=', (char)StringSplitOptions.None)[1].Trim();
                    var loadManufacturer = lines.First(s => s.StartsWith("Manufacturer")).Split('=', (char)StringSplitOptions.None)[1].Trim();
                    var loadSerialNumber = lines.First(s => s.StartsWith("SerialNumber")).Split('=', (char)StringSplitOptions.None)[1].Trim();
                    boardInfo.Product = loadProduct;
                    boardInfo.Manufacturer = loadManufacturer;
                    boardInfo.SerialNumber = loadSerialNumber;
                }
            }
        }
        catch (Exception ex)
        {
            ("获取主板信息出错，" + ex.Message).WriteLineError();
        }

        return boardInfo;
    }
}

/// <summary>
/// 主板信息
/// </summary>
public class BoardInfo
{
    /// <summary>
    /// 型号
    /// </summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>
    /// 制造商
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// 序列号
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// BIOS 信息
    /// </summary>
    public string Bios { get; set; } = string.Empty;

    /// <summary>
    /// 芯片组信息
    /// </summary>
    public string Chipset { get; set; } = string.Empty;

    /// <summary>
    /// 插槽类型和数量
    /// </summary>
    public string Slots { get; set; } = string.Empty;

    /// <summary>
    /// 内存支持信息
    /// </summary>
    public string Memory { get; set; } = string.Empty;

    /// <summary>
    /// 存储接口信息
    /// </summary>
    public string Storage { get; set; } = string.Empty;

    /// <summary>
    /// USB接口信息
    /// </summary>
    public string Usb { get; set; } = string.Empty;

    /// <summary>
    /// 网络接口信息
    /// </summary>
    public string Network { get; set; } = string.Empty;

    /// <summary>
    /// 接口信息
    /// </summary>
    public string Display { get; set; } = string.Empty;
}