#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ApplicationInfoHelper
// Guid:3341511d-d412-49ad-a3d4-f06c06a9a451
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 03:51:30
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Diagnostics;
using System.Reflection;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Utils.Extensions;
using XiHan.Utils.HardwareInfos;

namespace XiHan.Infrastructures.Infos;

/// <summary>
/// 应用信息帮助类
/// </summary>
public static class ApplicationInfoHelper
{
    /// <summary>
    /// 启动端口
    /// </summary>
    public static string Port => AppSettings.Port.GetValue().ToString();

    /// <summary>
    /// 启动环境
    /// </summary>
    public static string EnvironmentName => AppSettings.EnvironmentName.GetValue().ToString();

    /// <summary>
    /// 应用名称
    /// </summary>
    public static string Name => Process.GetCurrentProcess().ProcessName;

    /// <summary>
    /// 应用版本
    /// </summary>
    public static string Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;

    /// <summary>
    /// 所在路径
    /// </summary>
    public static string CurrentDirectory => AppContext.BaseDirectory;

    /// <summary>
    /// 占用空间
    /// </summary>
    public static string DirectorySize => FileHelper.GetDirectorySize(CurrentDirectory).FormatByteToString();

    /// <summary>
    /// 运行路径
    /// </summary>
    public static string ProcessPath => Environment.ProcessPath ?? string.Empty;

    /// <summary>
    /// 当前进程
    /// </summary>
    public static string CurrentProcessId => Process.GetCurrentProcess().Id.ToString();

    /// <summary>
    /// 会话标识
    /// </summary>
    public static string CurrentProcessSessionId => Process.GetCurrentProcess().SessionId.ToString();

    /// <summary>
    /// 运行时间
    /// </summary>
    public static string RunTime => (DateTime.Now - Process.GetCurrentProcess().StartTime).FormatTimeSpanToString();
}