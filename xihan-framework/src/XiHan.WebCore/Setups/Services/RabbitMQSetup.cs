#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RabbitMQSetup
// Guid:b0a1be0b-98e4-4a24-92a4-87f636916fce
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-03-19 上午 02:47:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.DependencyInjection;

namespace XiHan.WebCore.Setups.Services;

/// <summary>
/// RabbitMQSetup
/// </summary>
public static class RabbitMqSetup
{
    /// <summary>
    /// RabbitMQ 服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddRabbitMqSetup(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        //var isEnabledRabbitMQ = AppSettings.RabbitMQ.Enabled.GetValue();
        //if (isEnabledRabbitMQ)
        //{
        //    var hostName = AppSettings.RabbitMQ.HostName.GetValue();
        //    var userName = AppSettings.RabbitMQ.UserName.GetValue();
        //    var password = AppSettings.RabbitMQ.Password.GetValue();
        //    var port = AppSettings.RabbitMQ.Port.GetValue();
        //    var retryCount = AppSettings.RabbitMQ.RetryCount.GetValue();

        //    var factory = new ConnectionFactory()
        //    {
        //        HostName = hostName,
        //        DispatchConsumersAsync = true,
        //        UserName = userName,
        //        Password = password,
        //        Port = port
        //    };
        //}

        return services;
    }
}