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

using XiHan.Utils.Console;
using XiHan.Utils.Shell;

namespace XiHan.Utils.Info.BaseInfos;

/// <summary>
/// CPU帮助类
/// </summary>
public static class CpuHelper
{
    /// <summary>
    /// 获取CPU个数
    /// </summary>
    public static string GetCpuCount()
    {
        return Environment.ProcessorCount.ToString();
    }

    /// <summary>
    /// Windows系统获取CPU使用率
    /// </summary>
    /// <returns></returns>
    public static string GetWindowsCpuRate()
    {
        string output = "wmic".Cmd("cpu get LoadPercentage");
        return output.Replace("LoadPercentage", string.Empty).Trim() + "%";
    }

    /// <summary>
    /// Unix系统获取CPU使用率
    /// </summary>
    /// <returns></returns>
    public static string GetUnixCpuRate()
    {
        string output = @"top -b -n1 | grep ""Cpu(s)"" | awk '{print $2 + $4}'".Bash();
        return output.Trim() + "%";
    }

    /// <summary>
    /// 获取磁盘信息
    /// </summary>
    /// <returns></returns>
    public static CpuInfo GetCpuInfos()
    {
        CpuInfo cpuInfo = new();
        try
        {
            if (OSPlatformHelper.GetOsIsUnix())
            {
                cpuInfo.CpuCount = GetCpuCount();
                cpuInfo.CpuRate = GetUnixCpuRate();
            }
            else
            {
                cpuInfo.CpuCount = GetCpuCount();
                cpuInfo.CpuRate = GetWindowsCpuRate();
            }
        }
        catch (Exception ex)
        {
            ex.Message.WriteLineError();
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