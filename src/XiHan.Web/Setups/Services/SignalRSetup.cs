#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SignalRSetup
// Guid:c174d608-8454-4068-ba81-95240d034348
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-19 上午 12:34:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Web.Setups.Services;

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
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSignalR(options =>
        {
#if DEBUG
            // 当SignalR连接出现问题时，客户端会收到详细错误信息
            options.EnableDetailedErrors = true;
#endif
            // 要求1分钟内必须收到客户端发的一条消息，如果没有收到，那么服务器端则认为客户端掉了
            options.ClientTimeoutInterval = TimeSpan.FromMinutes(1);
        });

        return services;
    }
}