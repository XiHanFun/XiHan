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
                string output = ShellHelper.Bash("system_profiler SPHardwareDataType").Trim();
                string[] lines = output.Split(Environment.NewLine);
                if (!string.IsNullOrEmpty(output))
                {
                    var loadProduct = lines.First(s => s.StartsWith("Model Identifier")).Split(':')[1].Trim();
                    var loadManufacturer = lines.First(s => s.StartsWith("Manufacturer")).Split(':')[1].Trim();
                    var loadSerialNumber = lines.First(s => s.StartsWith("Serial Number (system)")).Split(':')[1].Trim();
                    var loadVersion = lines.First(s => s.StartsWith("Hardware UUID")).Split(':')[1].Trim();
                    boardInfo.Product = loadProduct;
                    boardInfo.Manufacturer = loadManufacturer;
                    boardInfo.SerialNumber = loadSerialNumber;
                    boardInfo.Version = loadVersion;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                string output = ShellHelper.Bash("sudo dmidecode -t baseboard").Trim();
                string[] lines = output.Split(Environment.NewLine);
                if (!string.IsNullOrEmpty(output))
                {
                    var loadProduct = lines.First(s => s.StartsWith("Product Name")).Split(':')[1].Trim();
                    var loadManufacturer = lines.First(s => s.StartsWith("Manufacturer")).Split(':')[1].Trim();
                    var loadSerialNumber = lines.First(s => s.StartsWith("Serial Number")).Split(':')[1].Trim();
                    var loadVersion = lines.First(s => s.StartsWith("Version")).Split(':')[1].Trim();
                    boardInfo.Product = loadProduct;
                    boardInfo.Manufacturer = loadManufacturer;
                    boardInfo.SerialNumber = loadSerialNumber;
                    boardInfo.Version = loadVersion;
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var output = ShellHelper.Cmd("wmic baseboard get Product,Manufacturer,SerialNumber,Version /Value").Trim();
                string[] lines = output.Split(Environment.NewLine);
                if (lines.Any())
                {
                    var loadProduct = lines.First(s => s.StartsWith("Product")).Split('=')[1].Trim();
                    var loadManufacturer = lines.First(s => s.StartsWith("Manufacturer")).Split('=')[1].Trim();
                    var loadSerialNumber = lines.First(s => s.StartsWith("SerialNumber")).Split('=')[1].Trim();
                    var loadVersion = lines.First(s => s.StartsWith("Version")).Split('=')[1].Trim();
                    boardInfo.Product = loadProduct;
                    boardInfo.Manufacturer = loadManufacturer;
                    boardInfo.SerialNumber = loadSerialNumber;
                    boardInfo.Version = loadVersion;
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
    /// 版本号
    /// </summary>
    public string Version { get; set; } = string.Empty;
}