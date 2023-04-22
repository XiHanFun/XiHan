#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:CpuHelper
// Guid:2e1f186b-92ad-4e02-9e15-d373684b181e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-09 上午 06:41:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Consoles;
using XiHan.Utils.Shells;

namespace XiHan.Utils.Infos.BaseInfos;

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
        string result = string.Empty;
        try
        {
            string output = "wmic".Cmd("cpu get LoadPercentage");
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
        string result = string.Empty;
        try
        {
            string output = @"top -b -n1 | grep ""Cpu(s)"" | awk '{print $2 + $4}'".Bash();
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