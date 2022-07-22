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
        ConsoleHelper.WriteLineInfo("===============系统信息===============");
        ConsoleHelper.WriteLineInfo($@"操作系统：{SystemInfoHelper.OperatingSystem}");
        ConsoleHelper.WriteLineInfo($@"系统描述：{SystemInfoHelper.OSDescription}");
        ConsoleHelper.WriteLineInfo($@"系统版本：{SystemInfoHelper.OSVersion}");
        ConsoleHelper.WriteLineInfo($@"系统平台：{SystemInfoHelper.Platform}");
        ConsoleHelper.WriteLineInfo($@"系统架构：{SystemInfoHelper.OSArchitecture}");
        ConsoleHelper.WriteLineInfo($@"CPU个数：{SystemInfoHelper.ProcessorCount}");
        ConsoleHelper.WriteLineInfo($@"系统目录：{SystemInfoHelper.SystemDirectory}");
        ConsoleHelper.WriteLineInfo($@"磁盘分区：{SystemInfoHelper.DiskPartition}");
        ConsoleHelper.WriteLineInfo($@"运行时间：{SystemInfoHelper.RunningTime}");
        ConsoleHelper.WriteLineInfo($@"交互模式：{SystemInfoHelper.InteractiveMode}");
        ConsoleHelper.WriteLineInfo("===============环境信息===============");
        ConsoleHelper.WriteLineInfo($@"环境框架：{EnvironmentInfoHelper.FrameworkDescription}");
        ConsoleHelper.WriteLineInfo($@"环境版本：{EnvironmentInfoHelper.EnvironmentVersion}");
        ConsoleHelper.WriteLineInfo($@"环境架构：{EnvironmentInfoHelper.ProcessArchitecture}");
        ConsoleHelper.WriteLineInfo($@"环境标识：{EnvironmentInfoHelper.RuntimeIdentifier}");
        ConsoleHelper.WriteLineInfo($@"机器名称：{EnvironmentInfoHelper.MachineName}");
        ConsoleHelper.WriteLineInfo($@"用户域名：{EnvironmentInfoHelper.UserDomainName}");
        ConsoleHelper.WriteLineInfo($@"关联用户：{EnvironmentInfoHelper.UserName}");
        ConsoleHelper.WriteLineInfo("===============应用信息===============");
        ConsoleHelper.WriteLineInfo($@"应用名称：{ApplicationInfoHelper.Name(Assembly.GetExecutingAssembly())}");
        ConsoleHelper.WriteLineInfo($@"当前版本：{ApplicationInfoHelper.Version(Assembly.GetExecutingAssembly())}");
        ConsoleHelper.WriteLineInfo($@"所在路径：{ApplicationInfoHelper.CurrentDirectory}");
        ConsoleHelper.WriteLineInfo($@"运行路径：{ApplicationInfoHelper.ProcessPath}");
        ConsoleHelper.WriteLineInfo($@"当前进程：{ApplicationInfoHelper.CurrentProcessId}");
        ConsoleHelper.WriteLineInfo($@"会话标识：{ApplicationInfoHelper.CurrentProcessSessionId}");
        ConsoleHelper.WriteLineInfo("===============启动成功===============");
        ConsoleHelper.WriteLineSuccess(@"
 ______  _   _       ___   _   _____       ___   __   _   _   _   _   _       ___   _____   _       _____   _____  
|___  / | | | |     /   | | | |  ___|     /   | |  \ | | | | | | | | | |     /   | |  _  \ | |     /  _  \ /  ___| 
   / /  | |_| |    / /| | | | | |__      / /| | |   \| | | |_| | | | | |    / /| | | |_| | | |     | | | | | |     
  / /   |  _  |   / / | | | | |  __|    / / | | | |\   | |  _  | | | | |   / / | | |  _  { | |     | | | | | |  _  
 / /__  | | | |  / /  | | | | | |      / /  | | | | \  | | | | | | |_| |  / /  | | | |_| | | |___  | |_| | | |_| | 
/_____| |_| |_| /_/   |_| |_| |_|     /_/   |_| |_|  \_| |_| |_| \_____/ /_/   |_| |_____/ |_____| \_____/ \_____/ 
        ");
    }
}