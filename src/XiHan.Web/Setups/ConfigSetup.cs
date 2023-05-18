#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ConfigSetup
// Guid:99553e0b-280a-4635-9eb8-8a2a7ab453a5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-24 上午 02:20:04
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Configuration;
using XiHan.Commons.Apps.Configs;
using XiHan.Utils.Consoles;

namespace XiHan.Web.Setups;

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
    public static IConfigurationBuilder AddConfigSetup(this IConfigurationBuilder configs)
    {
        "Configuration Start……".WriteLineInfo();
        if (configs == null)
        {
            throw new ArgumentNullException(nameof(configs));
        }

        // 配置创建
        AppConfigManager.RegisterConfig(configs);

        "Configuration Started Successfully！".WriteLineSuccess();
        return configs;
    }
}