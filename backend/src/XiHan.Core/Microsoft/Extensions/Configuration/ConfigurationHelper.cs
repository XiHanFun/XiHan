#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ConfigurationHelper
// Guid:4ed3fb2e-e2db-439b-9f20-1cfb7318822e
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 10:58:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using XiHan.Core.System.Extensions;

namespace XiHan.Core.Microsoft.Extensions.Configuration;

/// <summary>
/// 配置帮助类
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// 绑定配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public static IConfigurationRoot BuildConfiguration(
       ConfigurationBuilderOptions? options = null,
       Action<IConfigurationBuilder>? builderAction = null)
    {
        options ??= new ConfigurationBuilderOptions();

        // 设置基础路径
        if (options.BasePath.IsNullOrEmpty())
        {
            options.BasePath = Directory.GetCurrentDirectory();
        }

        // 加载基础配置文件
        var builder = new ConfigurationBuilder()
            .SetBasePath(options.BasePath!)
            .AddJsonFile(options.FileName + ".json", optional: options.Optional, reloadOnChange: options.ReloadOnChange)
            .AddJsonFile(options.FileName + ".secrets.json", optional: true, reloadOnChange: options.ReloadOnChange);

        // 加载特定环境下的配置文件
        if (!options.EnvironmentName.IsNullOrEmpty())
        {
            builder = builder.AddJsonFile($"{options.FileName}.{options.EnvironmentName}.json", optional: true, reloadOnChange: options.ReloadOnChange);
        }

        // 开发环境，加载用户机密
        if (options.EnvironmentName == "Development")
        {
            if (options.UserSecretsId != null)
            {
                builder.AddUserSecrets(options.UserSecretsId);
            }
            else if (options.UserSecretsAssembly != null)
            {
                builder.AddUserSecrets(options.UserSecretsAssembly, true);
            }
        }

        builder = builder.AddEnvironmentVariables(options.EnvironmentVariablesPrefix);

        if (options.CommandLineArgs != null)
        {
            builder = builder.AddCommandLine(options.CommandLineArgs);
        }

        builderAction?.Invoke(builder);

        return builder.Build();
    }
}