#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ConsoleServerInfo
// Guid:7dd7d459-6a52-4cd3-8298-161cf26b3395
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 03:11:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;
using XiHan.Infrastructure.Apps.Setting;
using XiHan.Utils.Console;
using XiHan.Utils.Formats;
using XiHan.Utils.Info;
using XiHan.Utils.Info.BaseInfos;
using XiHan.Utils.Serialize;

namespace XiHan.Api.Consoles;

/// <summary>
/// ConsoleServerInfo
/// </summary>
public static class ConsoleServerInfo
{
    /// <summary>
    /// 确认服务端信息
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
        $@"处理器信息：{SystemInfoHelper.CpuInfo.SerializeToJson()}".WriteLineInfo();
        $@"内存信息：{SystemInfoHelper.RamInfo.SerializeToJson()}".WriteLineInfo();
        $@"磁盘信息：{SystemInfoHelper.DiskInfo.SerializeToJson()}".WriteLineInfo();
        $@"IPv4地址：{LocalIpHelper.GetLocalIpV4()}".WriteLineInfo();
        $@"IPv6地址：{LocalIpHelper.GetLocalIpV6()}".WriteLineInfo();
        "==============================环境信息==============================".WriteLineInfo();
        $@"环境框架：{EnvironmentInfoHelper.FrameworkDescription}".WriteLineInfo();
        $@"环境版本：{EnvironmentInfoHelper.EnvironmentVersion}".WriteLineInfo();
        $@"环境架构：{EnvironmentInfoHelper.ProcessArchitecture}".WriteLineInfo();
        $@"环境标识：{EnvironmentInfoHelper.RuntimeIdentifier}".WriteLineInfo();
        $@"机器名称：{EnvironmentInfoHelper.MachineName}".WriteLineInfo();
        $@"用户域名：{EnvironmentInfoHelper.UserDomainName}".WriteLineInfo();
        $@"关联用户：{EnvironmentInfoHelper.UserName}".WriteLineInfo();
        "==============================应用信息==============================".WriteLineInfo();
        $@"应用名称：{ApplicationInfoHelper.Name(Assembly.GetExecutingAssembly())}".WriteLineInfo();
        $@"应用版本：{ApplicationInfoHelper.Version(Assembly.GetExecutingAssembly())}".WriteLineInfo();
        $@"所在路径：{ApplicationInfoHelper.CurrentDirectory}".WriteLineInfo();
        $@"运行文件：{ApplicationInfoHelper.ProcessPath}".WriteLineInfo();
        $@"当前进程：{ApplicationInfoHelper.CurrentProcessId}".WriteLineInfo();
        $@"会话标识：{ApplicationInfoHelper.CurrentProcessSessionId}".WriteLineInfo();
        $@"占用空间：{FileSizeFormatHelper.FormatByteToString(DiskHelper.GetDirectorySize(ApplicationInfoHelper.CurrentDirectory))}".WriteLineInfo();
        $@"启动环境：{AppSettings.EnvironmentName.GetValue()}".WriteLineInfo();
        $@"启动端口：{AppSettings.Port.GetValue()}".WriteLineInfo();
        "==============================配置信息==============================".WriteLineInfo();
        "==============数据库==============".WriteLineInfo();
        $@"连接类型：{AppSettings.Database.Type.GetValue()}".WriteLineInfo();
        $@"是否初始化：{AppSettings.Database.Inited.GetValue()}".WriteLineInfo();
        "===============分析===============".WriteLineInfo();
        $@"是否启用：{AppSettings.Miniprofiler.IsEnabled.GetValue()}".WriteLineInfo();
        "===============缓存===============".WriteLineInfo();
        $@"内存式缓存：默认启用；缓存时常：{AppSettings.Cache.SyncTimeout.GetValue()}分钟".WriteLineInfo();
        $@"分布式缓存：{AppSettings.Cache.Distributedcache.IsEnabled.GetValue()}".WriteLineInfo();
        $@"响应式缓存：{AppSettings.Cache.ResponseCache.IsEnabled.GetValue()}".WriteLineInfo();
        "===============跨域===============".WriteLineInfo();
        $@"是否启用：{AppSettings.Cors.IsEnabled.GetValue()}".WriteLineInfo();
        "===============日志===============".WriteLineInfo();
        $@"授权日志：{AppSettings.LogConfig.Authorization.GetValue()}".WriteLineInfo();
        $@"资源日志：{AppSettings.LogConfig.Resource.GetValue()}".WriteLineInfo();
        $@"请求日志：{AppSettings.LogConfig.Action.GetValue()}".WriteLineInfo();
        $@"结果日志：{AppSettings.LogConfig.Result.GetValue()}".WriteLineInfo();
        $@"异常日志：{AppSettings.LogConfig.Exception.GetValue()}".WriteLineInfo();
        "==============================任务信息==============================".WriteLineInfo();
        "==============================启动信息==============================".WriteLineInfo();
    }
}