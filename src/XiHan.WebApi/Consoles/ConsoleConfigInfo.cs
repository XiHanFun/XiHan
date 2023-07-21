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

namespace XiHan.WebApi.Consoles;

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
        $@"应用版本：{ApplicationInfoHelper.Version}".WriteLineInfo();
        $@"所在路径：{ApplicationInfoHelper.BaseDirectory}".WriteLineInfo();
        $@"运行文件：{ApplicationInfoHelper.ProcessPath}".WriteLineInfo();
        $@"当前进程：{ApplicationInfoHelper.ProcessId}".WriteLineInfo();
        $@"会话标识：{ApplicationInfoHelper.ProcessSessionId}".WriteLineInfo();
        $@"占用空间：{ApplicationInfoHelper.DirectorySize}".WriteLineInfo();
        $@"启动环境：{ApplicationInfoHelper.EnvironmentName}".WriteLineInfo();
        $@"启动端口：{ApplicationInfoHelper.Port}".WriteLineInfo();
        "==============================配置信息==============================".WriteLineInfo();
        "数据库：".WriteLineInfo();
        $@"连接类型：{AppSettings.Database.Type.GetValue()}".WriteLineInfo();
        $@"是否初始化：{AppSettings.Database.Initialized.GetValue()}".WriteLineInfo();
        "分析：".WriteLineInfo();
        $@"是否启用：{AppSettings.Miniprofiler.IsEnabled.GetValue()}".WriteLineInfo();
        "缓存：".WriteLineInfo();
        $@"内存式缓存：默认启用；缓存时常：{AppSettings.Cache.SyncTimeout.GetValue()}分钟".WriteLineInfo();
        $@"分布式缓存：{AppSettings.Cache.RedisCache.IsEnabled.GetValue()}".WriteLineInfo();
        $@"响应式缓存：{AppSettings.Cache.ResponseCache.IsEnabled.GetValue()}".WriteLineInfo();
        "跨域：".WriteLineInfo();
        $@"是否启用：{AppSettings.Cors.IsEnabled.GetValue()}".WriteLineInfo();
        "日志：".WriteLineInfo();
        $@"授权日志：{AppSettings.LogConfig.Authorization.GetValue()}".WriteLineInfo();
        $@"资源日志：{AppSettings.LogConfig.Resource.GetValue()}".WriteLineInfo();
        $@"请求日志：{AppSettings.LogConfig.Action.GetValue()}".WriteLineInfo();
        $@"结果日志：{AppSettings.LogConfig.Result.GetValue()}".WriteLineInfo();
        $@"异常日志：{AppSettings.LogConfig.Exception.GetValue()}".WriteLineInfo();
        "==============================任务信息==============================".WriteLineInfo();
        "==============================运行信息==============================".WriteLineInfo();
    }
}