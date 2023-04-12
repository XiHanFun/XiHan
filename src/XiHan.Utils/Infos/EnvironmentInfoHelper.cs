#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:EnvironmentInfoHelper
// Guid:2015f59c-4a29-456c-acd8-73da55b46c1c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 03:47:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Runtime.InteropServices;

namespace XiHan.Utils.Infos;

/// <summary>
/// 环境信息帮助类
/// </summary>
public static class EnvironmentInfoHelper
{
    /// <summary>
    /// 环境框架
    /// </summary>
    public static string FrameworkDescription => RuntimeInformation.FrameworkDescription.ToString();

    /// <summary>
    /// 环境版本
    /// </summary>
    public static string EnvironmentVersion => Environment.Version.ToString();

    /// <summary>
    /// 运行时架构
    /// </summary>
    public static string ProcessArchitecture => RuntimeInformation.ProcessArchitecture.ToString();

    /// <summary>
    /// 运行时标识符
    /// </summary>
    public static string RuntimeIdentifier => RuntimeInformation.RuntimeIdentifier.ToString();

    /// <summary>
    /// 机器名称
    /// </summary>
    public static string MachineName => Environment.MachineName.ToString();

    /// <summary>
    /// 用户域名
    /// </summary>
    public static string UserDomainName => Environment.UserDomainName.ToString();

    /// <summary>
    /// 关联用户
    /// </summary>
    public static string UserName => Environment.UserName.ToString();
}