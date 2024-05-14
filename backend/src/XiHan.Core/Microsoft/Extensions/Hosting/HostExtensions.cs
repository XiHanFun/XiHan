#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HostExtensions
// Guid:412a319b-cb16-4808-a91c-4352f7e6b272
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/5/7 0:39:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XiHan.Core.Application;
using XiHan.Core.Threading;

namespace XiHan.Core.Microsoft.Extensions.Hosting;

/// <summary>
/// 主机扩展方法
/// </summary>
public static class HostExtensions
{
    /// <summary>
    /// 异步初始化应用程序
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static async Task InitializeAsync(this IHost host)
    {
        var application = host.Services.GetRequiredService<IXiHanApplicationWithExternalServiceProvider>();
        var applicationLifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStopping.Register(() => AsyncHelper.RunSync(() => application.ShutdownAsync()));
        applicationLifetime.ApplicationStopped.Register(() => application.Dispose());

        await application.InitializeAsync(host.Services);
    }
}