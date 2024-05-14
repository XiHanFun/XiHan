#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HostingHostBuilderExtensions
// Guid:cb00037c-e7f8-480a-ab1c-494a3be708d2
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 上午 10:47:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace XiHan.Core.Microsoft.Extensions.Hosting;

/// <summary>
/// 主机构建器扩展方法
/// </summary>
public static class HostingHostBuilderExtensions
{
    /// <summary>
    /// 应用私密信息设置 JSON 路径
    /// </summary>
    public const string AppSettingsSecretJsonPath = "appsettings.secrets.json";

    /// <summary>
    /// 添加应用设置的私密 JSON
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <param name="optional"></param>
    /// <param name="reloadOnChange"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static IHostBuilder AddAppSettingsSecretsJson(
        this IHostBuilder hostBuilder,
        bool optional = true,
        bool reloadOnChange = true,
        string path = AppSettingsSecretJsonPath)
    {
        return hostBuilder.ConfigureAppConfiguration((_, builder) =>
        {
            builder.AddJsonFile(path: path, optional: optional, reloadOnChange: reloadOnChange);
        });
    }
}