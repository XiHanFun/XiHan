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
using XiHan.Common.Utilities.Extensions;
using XiHan.Common.Utilities.Files;
using XiHan.Common.Utilities.Reflections;
using XiHan.Infrastructure.Core.Apps.Configs;

namespace XiHan.Infrastructure.Core.Apps.Infos;

/// <summary>
/// 应用信息供应器
/// </summary>
public static class ApplicationInfoHelper
{
    /// <summary>
    /// 应用名称
    /// </summary>
    public static string ProcessName => Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;

    /// <summary>
    /// 当前版本
    /// </summary>
    public static string Version => Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? string.Empty;

    /// <summary>
    /// 启动端口
    /// </summary>
    public static string Port => AppSettings.Port.GetValue().ToString();

    /// <summary>
    /// 启动环境
    /// </summary>
    public static string EnvironmentName => AppSettings.EnvironmentName.GetValue();

    /// <summary>
    /// 文件路径
    /// </summary>
    public static string BaseDirectory => AppContext.BaseDirectory;

    /// <summary>
    /// 文件地址
    /// </summary>
    public static string FileLocation => Assembly.GetEntryAssembly()?.Location ?? string.Empty;

    /// <summary>
    /// 占用空间
    /// </summary>
    public static string DirectorySize => FileHelper.GetDirectorySize(AppContext.BaseDirectory).FormatFileSizeToString();

    /// <summary>
    /// 运行路径
    /// </summary>
    public static string ProcessPath => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ?? string.Empty;

    /// <summary>
    /// 运行时间
    /// </summary>
    public static string RunTime => (DateTime.Now - Process.GetCurrentProcess().StartTime).FormatTimeSpanToString();

    /// <summary>
    /// 占用内存
    /// </summary>
    public static string ProcessRam => Process.GetCurrentProcess().WorkingSet64.FormatFileSizeToString();

    /// <summary>
    /// 进程标识
    /// </summary>
    public static string ProcessId => Environment.ProcessId.ToString();

    /// <summary>
    /// 会话标识
    /// </summary>
    public static string ProcessSessionId => Process.GetCurrentProcess().SessionId.ToString();

    /// <summary>
    /// 自身依赖的所有包
    /// </summary>
    public static List<NuGetPackage> InstalledNuGetPackages => ReflectionHelper.GetNuGetPackages();
}