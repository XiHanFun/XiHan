#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SignalRSetup
// Guid:c174d608-8454-4068-ba81-95240d034348
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-19 上午 12:34:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;

namespace XiHan.WebCore.Setups.Services;

/// <summary>
/// SignalRSetup
/// </summary>
public static class SignalRSetup
{
    /// <summary>
    /// SignalR 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddSignalRSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSignalR(options =>
        {
#if DEBUG
            // 当SignalR连接出现问题时，客户端会收到详细错误信息
            options.EnableDetailedErrors = true;
#endif
            // 客户端发保持连接请求到服务端最长间隔，默认30秒，改成4分钟，网页需跟着设置connection.keepAliveIntervalInMilliseconds = 12e4;即2分钟
            options.ClientTimeoutInterval = TimeSpan.FromMinutes(4);
            // 服务端发保持连接请求到客户端间隔，默认15秒，改成2分钟，网页需跟着设置connection.serverTimeoutInMilliseconds = 24e4;即4分钟
            options.KeepAliveInterval = TimeSpan.FromMinutes(2);
        });

        return services;
    }
}