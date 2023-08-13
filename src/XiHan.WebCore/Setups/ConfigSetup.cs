#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ConfigSetup
// Guid:99553e0b-280a-4635-9eb8-8a2a7ab453a5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-24 上午 02:20:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Utils.Extensions;

namespace XiHan.WebCore.Setups;

/// <summary>
/// ConfigSetup
/// </summary>
public static class ConfigSetup
{
    /// <summary>
    /// 配置创建扩展
    /// </summary>
    /// <param name="configs"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IConfigurationBuilder AddConfigSetup(this IConfigurationBuilder configs)
    {
        "Configuration Start……".WriteLineInfo();
        if (configs == null) throw new ArgumentNullException(nameof(configs));

        // 配置创建
        AppConfigProvider.RegisterConfig(configs);

        "Configuration Started Successfully！".WriteLineSuccess();
        return configs;
    }
}