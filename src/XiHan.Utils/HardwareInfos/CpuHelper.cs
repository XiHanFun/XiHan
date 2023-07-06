#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CpuHelper
// Guid:2e1f186b-92ad-4e02-9e15-d373684b181e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-09 上午 06:41:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Extensions;
using XiHan.Utils.Shells;

namespace XiHan.Utils.HardwareInfos;

/// <summary>
/// 处理器帮助类
/// </summary>
public static class CpuHelper
{
    /// <summary>
    /// 获取处理器个数
    /// </summary>
    public static string GetCpuCount()
    {
        return Environment.ProcessorCount.ToString();
    }

    /// <summary>
    /// Windows 系统获取处理器使用率
    /// </summary>
    /// <returns></returns>
    public static string GetWindowsCpuRate()
    {
        var result = string.Empty;
        try
        {
            var output = ShellHelper.Cmd("wmic", "cpu get LoadPercentage");
            result = output.Replace("LoadPercentage", string.Empty).Trim() + "%";
        }
        catch (Exception ex)
        {
            ("获取处理器信息出错，" + ex.Message).WriteLineError();
        }

        return result;
    }

    /// <summary>
    /// Unix 系统获取处理器使用率
    /// </summary>
    /// <returns></returns>
    public static string GetUnixCpuRate()
    {
        var result = string.Empty;
        try
        {
            var output = ShellHelper.Bash(@"top -b -n1 | grep ""Cpu(s)"" | awk '{print $2 + $4}'");
            result = output.Trim() + "%";
        }
        catch (Exception ex)
        {
            ("获取处理器信息出错，" + ex.Message).WriteLineError();
        }

        return result;
    }

    /// <summary>
    /// 获取处理器信息
    /// </summary>
    /// <returns></returns>
    public static CpuInfo GetCpuInfos()
    {
        CpuInfo cpuInfo = new();
        if (OsPlatformHelper.GetOsIsUnix())
        {
            cpuInfo.CpuCount = GetCpuCount();
            cpuInfo.CpuRate = GetUnixCpuRate();
        }
        else
        {
            cpuInfo.CpuCount = GetCpuCount();
            cpuInfo.CpuRate = GetWindowsCpuRate();
        }

        return cpuInfo;
    }
}

/// <summary>
/// 处理器信息
/// </summary>
public class CpuInfo
{
    /// <summary>
    /// 处理器个数
    /// </summary>
    public string CpuCount { get; set; } = string.Empty;

    /// <summary>
    /// 处理器使用占比
    /// </summary>
    public string CpuRate { get; set; } = string.Empty;
}