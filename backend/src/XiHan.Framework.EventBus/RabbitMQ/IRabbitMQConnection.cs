#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IRabbitMQConnection
// Guid:cf6a473f-ab90-40c7-82dd-baf886461a7b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 3:22:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using RabbitMQ.Client;

namespace XiHan.Framework.EventBus.RabbitMQ;

/// <summary>
/// RabbitMQ 持久连接接口
/// </summary>
public interface IRabbitMQConnection
{
    /// <summary>
    /// 是否已经连接
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// 尝试重连
    /// </summary>
    /// <returns></returns>
    bool TryConnect();

    /// <summary>
    /// 创建 Model
    /// </summary>
    /// <returns></returns>
    IModel CreateModel();

    /// <summary>
    /// 发布消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exchangeName"></param>
    /// <param name="routingKey"></param>
    void PublishMessage(string message, string exchangeName, string routingKey);

    /// <summary>
    /// 订阅消息
    /// </summary>
    /// <param name="queueName"></param>
    void StartConsuming(string queueName);
}