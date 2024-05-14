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

namespace XiHan.EventBus.RabbitMQ.Consts;

/// <summary>
/// RabbitMQ 常量
/// </summary>
public class RabbitMQConstants
{
    /// <summary>
    /// 默认的 RabbitMQ 连接名称
    /// </summary>
    public const string DefaultBrokerName = "xiHan_event_bus";

    /// <summary>
    /// 默认的 RabbitMQ 队列名称
    /// </summary>
    public const string DefaultQueueName = "xiHan_event_bus_queue";

    /// <summary>
    /// 默认的 RabbitMQ 交换机名称
    /// </summary>
    public const string DefaultExchangeName = "xiHan_event_bus_exchange";

    /// <summary>
    /// 默认的 RabbitMQ 路由键
    /// </summary>
    public const int DefaultRetryCount = 5;

    /// <summary>
    /// 默认的 RabbitMQ 重试间隔
    /// </summary>
    public const int DefaultRetryInterval = 5;
}