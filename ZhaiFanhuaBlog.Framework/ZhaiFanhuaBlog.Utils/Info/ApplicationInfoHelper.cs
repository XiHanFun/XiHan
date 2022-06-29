// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ApplicationInfoHelper
// Guid:3341511d-d412-49ad-a3d4-f06c06a9a451
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:51:30
// ----------------------------------------------------------------

using System.Diagnostics;
using System.Reflection;

namespace ZhaiFanhuaBlog.Utils.Info;

/// <summary>
/// 应用信息帮助类
/// </summary>
public static class ApplicationInfoHelper
{
    /// <summary>
    /// 应用名称
    /// </summary>
    public static string Name(Assembly assembly) => assembly.GetName().Name!.ToString();

    /// <summary>
    /// 应用版本
    /// </summary>
    public static string Version(Assembly assembly) => assembly.GetName().Version!.ToString();

    /// <summary>
    /// 所在路径
    /// </summary>
    public static string CurrentDirectory => Environment.CurrentDirectory.ToString();

    /// <summary>
    /// 运行路径
    /// </summary>
    public static string ProcessPath => Environment.ProcessPath!.ToString();

    /// <summary>
    /// 当前进程
    /// </summary>
    public static string CurrentProcessId => Process.GetCurrentProcess().Id.ToString();

    /// <summary>
    /// 会话标识
    /// </summary>
    public static string CurrentProcessSessionId => Process.GetCurrentProcess().SessionId.ToString();
}