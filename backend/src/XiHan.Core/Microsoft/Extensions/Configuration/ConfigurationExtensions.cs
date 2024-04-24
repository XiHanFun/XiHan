#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ConfigurationExtensions
// Guid:fa3ba622-23f7-4a62-88b5-0ecdb6b10e62
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 10:43:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using XiHan.Core.Microsoft.Extensions.Hosting;

namespace XiHan.Core.Microsoft.Extensions.Configuration;

/// <summary>
/// 配置扩展方法
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// 用于将应用程序设置和机密信息添加到配置系统中
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="optional"></param>
    /// <param name="reloadOnChange"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static IConfigurationBuilder AddAppSettingsSecretsJson(
        this IConfigurationBuilder builder,
        bool optional = true,
        bool reloadOnChange = true,
        string path = HostingHostBuilderExtensions.AppSettingsSecretJsonPath)
    {
        return builder.AddJsonFile(path: path, optional: optional, reloadOnChange: reloadOnChange);
    }
}