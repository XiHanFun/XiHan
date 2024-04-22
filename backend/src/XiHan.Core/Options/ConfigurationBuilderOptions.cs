#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ConfigurationBuilderOptions
// Guid:9b242194-1768-4fcd-9120-dd3d862a4131
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 06:03:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;

namespace XiHan.Core.Options;

/// <summary>
/// 配置生成器选项
/// </summary>
public class ConfigurationBuilderOptions
{
    /// <summary>
    /// 用于设置用于获取应用程序的用户秘密id的程序集
    /// 使用这个或者<see cref="UserSecretsId"/>(更高优先级)
    /// </summary>
    public Assembly? UserSecretsAssembly { get; set; }

    /// <summary>
    /// 用于设置应用程序的用户秘密id
    /// 使用this(高优先级)或<see cref="UserSecretsAssembly"/>
    /// </summary>
    public string? UserSecretsId { get; set; }

    /// <summary>
    /// 默认值:“appsettings”
    /// </summary>
    public string FileName { get; set; } = "appsettings";

    /// <summary>
    /// 是否为可选项，默认值:true
    /// </summary>
    public bool Optional { get; set; } = true;

    /// <summary>
    /// 如果文件发生变化，是否需要重新加载配置。默认值:true
    /// </summary>
    public bool ReloadOnChange { get; set; } = true;

    /// <summary>
    /// 环境的名字。一般为"Development", "Staging" 或 "Production"
    /// </summary>
    public string? EnvironmentName { get; set; }

    /// <summary>
    /// 读取由<see cref="FileName"/>指示的配置文件的基本路径
    /// </summary>
    public string? BasePath { get; set; }

    /// <summary>
    /// 环境变量的前缀
    /// </summary>
    public string? EnvironmentVariablesPrefix { get; set; }

    /// <summary>
    /// 命令行参数
    /// </summary>
    public string[]? CommandLineArgs { get; set; }
}