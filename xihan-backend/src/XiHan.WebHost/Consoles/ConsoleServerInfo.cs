#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ConsoleServerInfo
// Guid:7dd7d459-6a52-4cd3-8298-161cf26b3395
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-30 上午 03:11:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Infos;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.WebHost.Consoles;

/// <summary>
/// ConsoleServerInfo
/// </summary>
public static class ConsoleServerInfo
{
    /// <summary>
    /// 确认服务器信息
    /// </summary>
    public static void ConfirmServerInfo()
    {
        "==============================系统信息==============================".WriteLineInfo();
        $@"操作系统：{SystemInfoHelper.OperatingSystem}".WriteLineInfo();
        $@"系统描述：{SystemInfoHelper.OsDescription}".WriteLineInfo();
        $@"系统版本：{SystemInfoHelper.OsVersion}".WriteLineInfo();
        $@"系统平台：{SystemInfoHelper.Platform}".WriteLineInfo();
        $@"系统架构：{SystemInfoHelper.OsArchitecture}".WriteLineInfo();
        $@"系统目录：{SystemInfoHelper.SystemDirectory}".WriteLineInfo();
        $@"运行时间：{SystemInfoHelper.RunningTime}".WriteLineInfo();
        $@"交互模式：{SystemInfoHelper.InteractiveMode}".WriteLineInfo();
        $@"主板信息：{SystemInfoHelper.BoardInfo.SerializeTo()}".WriteLineInfo();
        $@"处理器信息：{SystemInfoHelper.CpuInfo.SerializeTo()}".WriteLineInfo();
        $@"内存信息：{SystemInfoHelper.RamInfo.SerializeTo()}".WriteLineInfo();
        $@"磁盘信息：{SystemInfoHelper.DiskInfo.SerializeTo()}".WriteLineInfo();
        $@"网卡信息：{SystemInfoHelper.NetworkInfo.SerializeTo()}".WriteLineInfo();

        "==============================环境信息==============================".WriteLineInfo();
        $@"环境框架：{EnvironmentInfoHelper.FrameworkDescription}".WriteLineInfo();
        $@"环境版本：{EnvironmentInfoHelper.EnvironmentVersion}".WriteLineInfo();
        $@"环境架构：{EnvironmentInfoHelper.ProcessArchitecture}".WriteLineInfo();
        $@"环境标识：{EnvironmentInfoHelper.RuntimeIdentifier}".WriteLineInfo();
        $@"机器名称：{EnvironmentInfoHelper.MachineName}".WriteLineInfo();
        $@"用户域名：{EnvironmentInfoHelper.UserDomainName}".WriteLineInfo();
        $@"关联用户：{EnvironmentInfoHelper.UserName}".WriteLineInfo();

        "==============================启动信息==============================".WriteLineInfo();
    }
}