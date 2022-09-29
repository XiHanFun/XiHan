// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomConsole
// Guid:7dd7d459-6a52-4cd3-8298-161cf26b3395
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:11:28
// ----------------------------------------------------------------

using System.Reflection;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.DirFile;
using ZhaiFanhuaBlog.Utils.Formats;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.Setups.Common.Console;

/// <summary>
/// CustomConsole
/// </summary>
public static class CustomConsole
{
    /// <summary>
    /// 打印系统信息
    /// </summary>
    public static void ConsoleInfos()
    {
        ConsoleHelper.WriteLineInfo("==============================系统信息==============================");
        ConsoleHelper.WriteLineInfo($@"操作系统：{SystemInfoHelper.OperatingSystem}");
        ConsoleHelper.WriteLineInfo($@"系统描述：{SystemInfoHelper.OSDescription}");
        ConsoleHelper.WriteLineInfo($@"系统版本：{SystemInfoHelper.OSVersion}");
        ConsoleHelper.WriteLineInfo($@"系统核数：{SystemInfoHelper.ProcessorCount}");
        ConsoleHelper.WriteLineInfo($@"系统平台：{SystemInfoHelper.Platform}");
        ConsoleHelper.WriteLineInfo($@"系统架构：{SystemInfoHelper.OSArchitecture}");
        ConsoleHelper.WriteLineInfo($@"系统目录：{SystemInfoHelper.SystemDirectory}");
        ConsoleHelper.WriteLineInfo($@"磁盘分区：{SystemInfoHelper.DiskPartition}");
        ConsoleHelper.WriteLineInfo($@"运行时间：{SystemInfoHelper.RunningTime}");
        ConsoleHelper.WriteLineInfo($@"交互模式：{SystemInfoHelper.InteractiveMode}");
        ConsoleHelper.WriteLineInfo("==============================环境信息==============================");
        ConsoleHelper.WriteLineInfo($@"环境框架：{EnvironmentInfoHelper.FrameworkDescription}");
        ConsoleHelper.WriteLineInfo($@"环境版本：{EnvironmentInfoHelper.EnvironmentVersion}");
        ConsoleHelper.WriteLineInfo($@"环境架构：{EnvironmentInfoHelper.ProcessArchitecture}");
        ConsoleHelper.WriteLineInfo($@"环境标识：{EnvironmentInfoHelper.RuntimeIdentifier}");
        ConsoleHelper.WriteLineInfo($@"机器名称：{EnvironmentInfoHelper.MachineName}");
        ConsoleHelper.WriteLineInfo($@"用户域名：{EnvironmentInfoHelper.UserDomainName}");
        ConsoleHelper.WriteLineInfo($@"关联用户：{EnvironmentInfoHelper.UserName}");
        ConsoleHelper.WriteLineInfo("==============================应用信息==============================");
        ConsoleHelper.WriteLineHandle(ApplicationInfoHelper.Logo);
        ConsoleHelper.WriteLineHandle(ApplicationInfoHelper.Copyright);
        ConsoleHelper.WriteLineInfo($@"应用名称：{ApplicationInfoHelper.Name(Assembly.GetExecutingAssembly())}");
        ConsoleHelper.WriteLineInfo($@"当前版本：{ApplicationInfoHelper.Version(Assembly.GetExecutingAssembly())}");
        ConsoleHelper.WriteLineInfo($@"所在路径：{ApplicationInfoHelper.CurrentDirectory}");
        ConsoleHelper.WriteLineInfo($@"运行路径：{ApplicationInfoHelper.ProcessPath}");
        ConsoleHelper.WriteLineInfo($@"当前进程：{ApplicationInfoHelper.CurrentProcessId}");
        ConsoleHelper.WriteLineInfo($@"会话标识：{ApplicationInfoHelper.CurrentProcessSessionId}");
        ConsoleHelper.WriteLineInfo($@"占用磁盘空间：{FileSizeFormatHelper.FormatByteToString(DirFileHelper.GetDirectorySize(ApplicationInfoHelper.CurrentDirectory))}");
        ConsoleHelper.WriteLineInfo($@"本地IPv4地址：{LocalIpInfoHelper.GetLocalIpV4()}");
        ConsoleHelper.WriteLineInfo($@"本地IPv6地址：{LocalIpInfoHelper.GetLocalIpV6()}");
        ConsoleHelper.WriteLineInfo($@"应用启动环境：{AppSettings.EnvironmentName}");
        ConsoleHelper.WriteLineInfo("==============================配置信息==============================");
        ConsoleHelper.WriteLineInfo("==============数据库==============");
        ConsoleHelper.WriteLineInfo($@"连接类型：{AppSettings.Database.Type}");
        ConsoleHelper.WriteLineInfo($@"是否初始化：{AppSettings.Database.Initialization}");
        ConsoleHelper.WriteLineInfo("===============分析===============");
        ConsoleHelper.WriteLineInfo($@"是否启用：{AppSettings.Miniprofiler.IsEnabled}");
        ConsoleHelper.WriteLineInfo("===============缓存===============");
        ConsoleHelper.WriteLineInfo($@"内存式缓存：默认启用；缓存时常：{AppSettings.Cache.SyncTimeout}分钟");
        ConsoleHelper.WriteLineInfo($@"分布式缓存：{AppSettings.Cache.Distributedcache.IsEnabled}");
        ConsoleHelper.WriteLineInfo($@"响应式缓存：{AppSettings.Cache.Responsecache.IsEnabled}");
        ConsoleHelper.WriteLineInfo("===============跨域===============");
        ConsoleHelper.WriteLineInfo($@"是否启用：{AppSettings.Cors.IsEnabled}");
        ConsoleHelper.WriteLineInfo("===============日志===============");
        ConsoleHelper.WriteLineInfo($@"授权日志：{AppSettings.Logging.Authorization}");
        ConsoleHelper.WriteLineInfo($@"资源日志：{AppSettings.Logging.Resource}");
        ConsoleHelper.WriteLineInfo($@"请求日志：{AppSettings.Logging.Action}");
        ConsoleHelper.WriteLineInfo($@"结果日志：{AppSettings.Logging.Result}");
        ConsoleHelper.WriteLineInfo($@"异常日志：{AppSettings.Logging.Exception}");
        ConsoleHelper.WriteLineInfo("==============================启动信息==============================");
    }
}