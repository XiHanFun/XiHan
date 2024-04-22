#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:XiHanConfigurationBuilderOptions
// Guid:9b242194-1768-4fcd-9120-dd3d862a4131
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 下午 06:03:10
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Reflection;

namespace XiHan.Core.Options;

/// <summary>
/// XiHanConfigurationBuilderOptions
/// </summary>
public class XiHanConfigurationBuilderOptions
{
    /// <summary>
    /// Used to set assembly which is used to get the user secret id for the application.
    /// Use this or <see cref="UserSecretsId"/> (higher priority)
    /// </summary>
    public Assembly? UserSecretsAssembly { get; set; }

    /// <summary>
    /// Used to set user secret id for the application.
    /// Use this (higher priority) or <see cref="UserSecretsAssembly"/>
    /// </summary>
    public string? UserSecretsId { get; set; }

    /// <summary>
    /// Default value: "appsettings".
    /// </summary>
    public string FileName { get; set; } = "appsettings";

    /// <summary>
    /// Whether the file is optional, Default value: true.
    /// </summary>
    public bool Optional { get; set; } = true;

    /// <summary>
    /// Whether the configuration should be reloaded if the file changes, Default value: true.
    /// </summary>
    public bool ReloadOnChange { get; set; } = true;

    /// <summary>
    /// Environment name. Generally used "Development", "Staging" or "Production".
    /// </summary>
    public string? EnvironmentName { get; set; }

    /// <summary>
    /// Base path to read the configuration file indicated by <see cref="FileName"/>.
    /// </summary>
    public string? BasePath { get; set; }

    /// <summary>
    /// Prefix for the environment variables.
    /// </summary>
    public string? EnvironmentVariablesPrefix { get; set; }

    /// <summary>
    /// Command line arguments.
    /// </summary>
    public string[]? CommandLineArgs { get; set; }
}