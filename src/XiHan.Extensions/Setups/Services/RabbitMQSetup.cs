#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:RabbitMQSetup
// Guid:b0a1be0b-98e4-4a24-92a4-87f636916fce
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-03-19 上午 02:47:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using XiHan.Infrastructure.Apps.Setting;

namespace XiHan.Extensions.Setups.Services;

/// <summary>
/// RabbitMQSetup
/// </summary>
public static class RabbitMQSetup
{
    /// <summary>
    /// RabbitMQ 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddRabbitMQSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        var isEnabledRabbitMQ = AppSettings.RabbitMQ.Enabled.Get();
        if (isEnabledRabbitMQ)
        {
            var hostName = AppSettings.RabbitMQ.HostName.Get();
            var userName = AppSettings.RabbitMQ.UserName.Get();
            var password = AppSettings.RabbitMQ.Password.Get();
            var port = AppSettings.RabbitMQ.Port.Get();
            var retryCount = AppSettings.RabbitMQ.RetryCount.Get();

            var factory = new ConnectionFactory()
            {
                HostName = hostName,
                DispatchConsumersAsync = true,
                UserName = userName,
                Password = password,
                Port = port
            };
        }

        return services;
    }
}