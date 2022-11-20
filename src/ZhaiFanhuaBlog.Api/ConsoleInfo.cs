#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ConsoleInfo
// Guid:7dd7d459-6a52-4cd3-8298-161cf26b3395
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:11:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;
using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.DirFile;
using ZhaiFanhuaBlog.Utils.Formats;
using ZhaiFanhuaBlog.Utils.Info;

namespace ZhaiFanhuaBlog.Api;

/// <summary>
/// ConsoleInfo
/// </summary>
public static class ConsoleInfo
{
    /// <summary>
    /// 打印系统信息
    /// </summary>
    public static void Print()
    {
        "==============================系统信息==============================".WriteLineInfo();
        $@"操作系统：{SystemInfoHelper.OperatingSystem}".WriteLineInfo();
        $@"系统描述：{SystemInfoHelper.OSDescription}".WriteLineInfo();
        $@"系统版本：{SystemInfoHelper.OSVersion}".WriteLineInfo();
        $@"系统核数：{SystemInfoHelper.ProcessorCount}".WriteLineInfo();
        $@"系统平台：{SystemInfoHelper.Platform}".WriteLineInfo();
        $@"系统架构：{SystemInfoHelper.OSArchitecture}".WriteLineInfo();
        $@"系统目录：{SystemInfoHelper.SystemDirectory}".WriteLineInfo();
        $@"磁盘分区：{SystemInfoHelper.DiskPartition}".WriteLineInfo();
        $@"运行时间：{SystemInfoHelper.RunningTime}".WriteLineInfo();
        $@"交互模式：{SystemInfoHelper.InteractiveMode}".WriteLineInfo();
        "==============================环境信息==============================".WriteLineInfo();
        $@"环境框架：{EnvironmentInfoHelper.FrameworkDescription}".WriteLineInfo();
        $@"环境版本：{EnvironmentInfoHelper.EnvironmentVersion}".WriteLineInfo();
        $@"环境架构：{EnvironmentInfoHelper.ProcessArchitecture}".WriteLineInfo();
        $@"环境标识：{EnvironmentInfoHelper.RuntimeIdentifier}".WriteLineInfo();
        $@"机器名称：{EnvironmentInfoHelper.MachineName}".WriteLineInfo();
        $@"用户域名：{EnvironmentInfoHelper.UserDomainName}".WriteLineInfo();
        $@"关联用户：{EnvironmentInfoHelper.UserName}".WriteLineInfo();
        "==============================应用信息==============================".WriteLineInfo();
        ApplicationInfoHelper.Logo.WriteLineHandle();
        ApplicationInfoHelper.Copyright.WriteLineHandle();
        $@"应用名称：{ApplicationInfoHelper.Name(Assembly.GetExecutingAssembly())}".WriteLineInfo();
        $@"当前版本：{ApplicationInfoHelper.Version(Assembly.GetExecutingAssembly())}".WriteLineInfo();
        $@"所在路径：{ApplicationInfoHelper.CurrentDirectory}".WriteLineInfo();
        $@"运行文件：{ApplicationInfoHelper.ProcessPath}".WriteLineInfo();
        $@"当前进程：{ApplicationInfoHelper.CurrentProcessId}".WriteLineInfo();
        $@"会话标识：{ApplicationInfoHelper.CurrentProcessSessionId}".WriteLineInfo();
        $@"占用磁盘空间：{FileSizeFormatHelper.FormatByteToString(DirFileHelper.GetDirectorySize(ApplicationInfoHelper.CurrentDirectory))}".WriteLineInfo();
        $@"本地IPv4地址：{LocalIpInfoHelper.GetLocalIpV4()}".WriteLineInfo();
        $@"本地IPv6地址：{LocalIpInfoHelper.GetLocalIpV6()}".WriteLineInfo();
        $@"应用启动环境：{AppSettings.EnvironmentName}".WriteLineInfo();
        "==============================配置信息==============================".WriteLineInfo();
        "==============数据库==============".WriteLineInfo();
        $@"连接类型：{AppSettings.Database.Type}".WriteLineInfo();
        $@"是否初始化：{AppSettings.Database.Initialization}".WriteLineInfo();
        "===============分析===============".WriteLineInfo();
        $@"是否启用：{AppSettings.Miniprofiler.IsEnabled}".WriteLineInfo();
        "===============缓存===============".WriteLineInfo();
        $@"内存式缓存：默认启用；缓存时常：{AppSettings.Cache.SyncTimeout}分钟".WriteLineInfo();
        $@"分布式缓存：{AppSettings.Cache.Distributedcache.IsEnabled}".WriteLineInfo();
        $@"响应式缓存：{AppSettings.Cache.Responsecache.IsEnabled}".WriteLineInfo();
        "===============跨域===============".WriteLineInfo();
        $@"是否启用：{AppSettings.Cors.IsEnabled}".WriteLineInfo();
        "===============日志===============".WriteLineInfo();
        $@"授权日志：{AppSettings.Logging.Authorization}".WriteLineInfo();
        $@"资源日志：{AppSettings.Logging.Resource}".WriteLineInfo();
        $@"请求日志：{AppSettings.Logging.Action}".WriteLineInfo();
        $@"结果日志：{AppSettings.Logging.Result}".WriteLineInfo();
        $@"异常日志：{AppSettings.Logging.Exception}".WriteLineInfo();
        "==============================启动信息==============================".WriteLineInfo();
    }
}