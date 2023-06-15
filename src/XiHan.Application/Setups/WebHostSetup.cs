#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WebHostSetup
// Guid:24f022b7-c1c4-4b6b-b399-1bf4b25d0d1b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-28 下午 07:20:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Hosting;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Utils.Extensions;

namespace XiHan.Application.Setups;

/// <summary>
/// WebHostSetup
/// </summary>
public static class WebHostSetup
{
    /// <summary>
    /// 主机创建扩展
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IWebHostBuilder AddWebHostSetup(this IWebHostBuilder host)
    {
        "Host Start……".WriteLineInfo();
        if (host == null)
        {
            throw new ArgumentNullException(nameof(host));
        }

        // 端口
        var port = AppSettings.Port.GetValue();
        host.UseUrls($"http://*:{port}");

        "Host Started Successfully！".WriteLineSuccess();
        return host;
    }
}