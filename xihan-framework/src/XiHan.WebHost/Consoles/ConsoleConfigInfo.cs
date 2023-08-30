#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ConsoleConfigInfo
// Guid:951480f1-448b-4955-a689-c32e5e3717c4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/21 2:33:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Infos;
using XiHan.Utils.Extensions;

namespace XiHan.WebHost.Consoles;

/// <summary>
/// ConsoleConfigInfo
/// </summary>
public static class ConsoleConfigInfo
{
    /// <summary>
    /// 确认配置信息
    /// </summary>
    public static void ConfirmConfigInfo()
    {
        "==============================应用信息==============================".WriteLineInfo();
        $@"应用名称：{ApplicationInfoHelper.ProcessName}".WriteLineInfo();
        $@"当前版本：{ApplicationInfoHelper.Version}".WriteLineInfo();
        $@"启动端口：{ApplicationInfoHelper.Port}".WriteLineInfo();
        $@"启动环境：{ApplicationInfoHelper.EnvironmentName}".WriteLineInfo();
        $@"文件路径：{ApplicationInfoHelper.BaseDirectory}".WriteLineInfo();
        $@"文件地址：{ApplicationInfoHelper.FileLocation}".WriteLineInfo();
        $@"占用空间：{ApplicationInfoHelper.DirectorySize}".WriteLineInfo();
        $@"运行路径：{ApplicationInfoHelper.ProcessPath}".WriteLineInfo();
        $@"运行时间：{ApplicationInfoHelper.RunTime}".WriteLineInfo();
        $@"占用内存：{ApplicationInfoHelper.ProcessRam}".WriteLineInfo();
        $@"进程标识：{ApplicationInfoHelper.ProcessId}".WriteLineInfo();
        $@"会话标识：{ApplicationInfoHelper.ProcessSessionId}".WriteLineInfo();

        "==============================配置信息==============================".WriteLineInfo();
        "数据库：".WriteLineInfo();
        $@"初始化数据库：{AppSettings.Database.EnableInitDb.GetValue()}".WriteLineInfo();
        $@"初始化种子数据：{AppSettings.Database.EnableInitSeed.GetValue()}".WriteLineInfo();
        AppSettings.Database.DatabaseConfigs.GetSection().ToList().ForEach(config =>
        {
            $@"连接序号：{config.ConfigId}".WriteLineInfo();
            $@"连接类型：{config.DataBaseType}".WriteLineInfo();
            $@"是否自动关闭连接：{config.IsAutoCloseConnection}".WriteLineInfo();
        });
        "分析：".WriteLineInfo();
        $@"是否启用：{AppSettings.Miniprofiler.IsEnabled.GetValue()}".WriteLineInfo();
        "缓存：".WriteLineInfo();
        $@"内存式缓存：默认启用；缓存时常：{AppSettings.Cache.SyncTimeout.GetValue()}分钟".WriteLineInfo();
        $@"分布式缓存：{AppSettings.Cache.RedisCache.IsEnabled.GetValue()}".WriteLineInfo();
        $@"响应式缓存：{AppSettings.Cache.ResponseCache.IsEnabled.GetValue()}".WriteLineInfo();
        "跨域：".WriteLineInfo();
        $@"是否启用：{AppSettings.Cors.IsEnabled.GetValue()}".WriteLineInfo();

        "==============================运行信息==============================".WriteLineInfo();
    }
}