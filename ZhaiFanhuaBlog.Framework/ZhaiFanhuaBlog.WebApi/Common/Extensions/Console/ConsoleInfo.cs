// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ConsoleInfo
// Guid:7dd7d459-6a52-4cd3-8298-161cf26b3395
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:11:28
// ----------------------------------------------------------------

using System.Reflection;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.Console;

/// <summary>
/// ConsoleInfo
/// </summary>
public static class ConsoleInfo
{
    /// <summary>
    /// 打印系统信息
    /// </summary>
    public static void ConsoleInfos()
    {
        ConsoleHelper.WriteSuccessLine("ZhaiFanhuaBlog Application Started Successfully！");
        ConsoleHelper.WriteInfoLine("===============系统信息===============");
        ConsoleHelper.WriteInfoLine($@"操作系统：{SystemInfoHelper.OperatingSystem}");
        ConsoleHelper.WriteInfoLine($@"系统描述：{SystemInfoHelper.OSDescription}");
        ConsoleHelper.WriteInfoLine($@"系统版本：{SystemInfoHelper.OSVersion}");
        ConsoleHelper.WriteInfoLine($@"系统平台：{SystemInfoHelper.Platform}");
        ConsoleHelper.WriteInfoLine($@"系统架构：{SystemInfoHelper.OSArchitecture}");
        ConsoleHelper.WriteInfoLine($@"CPU个数：{SystemInfoHelper.ProcessorCount}");
        ConsoleHelper.WriteInfoLine($@"系统目录：{SystemInfoHelper.SystemDirectory}");
        ConsoleHelper.WriteInfoLine($@"磁盘分区：{SystemInfoHelper.DiskPartition}");
        ConsoleHelper.WriteInfoLine($@"运行时间：{SystemInfoHelper.RunningTime}");
        ConsoleHelper.WriteInfoLine($@"交互模式：{SystemInfoHelper.InteractiveMode}");
        ConsoleHelper.WriteInfoLine("===============环境信息===============");
        ConsoleHelper.WriteInfoLine($@"环境框架：{EnvironmentInfoHelper.FrameworkDescription}");
        ConsoleHelper.WriteInfoLine($@"环境版本：{EnvironmentInfoHelper.EnvironmentVersion}");
        ConsoleHelper.WriteInfoLine($@"环境架构：{EnvironmentInfoHelper.ProcessArchitecture}");
        ConsoleHelper.WriteInfoLine($@"环境标识：{EnvironmentInfoHelper.RuntimeIdentifier}");
        ConsoleHelper.WriteInfoLine($@"机器名称：{EnvironmentInfoHelper.MachineName}");
        ConsoleHelper.WriteInfoLine($@"用户域名：{EnvironmentInfoHelper.UserDomainName}");
        ConsoleHelper.WriteInfoLine($@"关联用户：{EnvironmentInfoHelper.UserName}");
        ConsoleHelper.WriteInfoLine("===============应用信息===============");
        ConsoleHelper.WriteInfoLine($@"应用名称：{ApplicationInfoHelper.Name(Assembly.GetExecutingAssembly())}");
        ConsoleHelper.WriteInfoLine($@"当前版本：{ApplicationInfoHelper.Version(Assembly.GetExecutingAssembly())}");
        ConsoleHelper.WriteInfoLine($@"所在路径：{ApplicationInfoHelper.CurrentDirectory}");
        ConsoleHelper.WriteInfoLine($@"运行路径：{ApplicationInfoHelper.ProcessPath}");
        ConsoleHelper.WriteInfoLine($@"当前进程：{ApplicationInfoHelper.CurrentProcessId}");
        ConsoleHelper.WriteInfoLine($@"会话标识：{ApplicationInfoHelper.CurrentProcessSessionId}");
    }
}