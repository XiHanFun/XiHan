#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RabbitMQConstants
// Guid:d18a29be-78f1-4f10-963e-942052ee5ed5
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 3:31:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructure.EventBus.RabbitMQ.Consts;

/// <summary>
/// RabbitMQ 常量
/// </summary>
public class RabbitMQConstants
{
    public const string DefaultBrokerName = "xiHan_event_bus";

    public const string DefaultQueueName = "xiHan_event_bus_queue";

    public const string DefaultExchangeName = "xiHan_event_bus_exchange";

    public const int DefaultRetryCount = 5;

    public const int DefaultRetryInterval = 5;
}