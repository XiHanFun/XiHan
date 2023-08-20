#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WebHostSetup
// Guid:24f022b7-c1c4-4b6b-b399-1bf4b25d0d1b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-12-28 下午 07:20:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Utils.Extensions;

namespace XiHan.WebCore.Setups;

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
        if (host == null) throw new ArgumentNullException(nameof(host));

        // 端口
        var port = AppSettings.Port.GetValue();
        host.UseUrls($"https://*:{port}");

        // 设置接口超时时间和上传大小
        host.ConfigureKestrel(options =>
        {
            options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
            options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(30);
            // 文件上传最大限制 100M
            options.Limits.MaxRequestBodySize = 100 * 1024 * 1024;
            // 启用 HTTP/3
            options.ListenAnyIP(port, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                listenOptions.UseHttps();
            });
        });

        "Host Started Successfully！".WriteLineSuccess();
        return host;
    }
}