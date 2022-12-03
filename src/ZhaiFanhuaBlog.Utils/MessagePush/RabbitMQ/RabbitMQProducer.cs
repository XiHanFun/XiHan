#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RabbitMQProducer
// Guid:53425e26-7e41-4c73-ad01-11f3756f7927
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-24 上午 03:00:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using RabbitMQ.Client;
using System.Text;

namespace ZhaiFanhuaBlog.Utils.MessagePush.RabbitMQ;

/// <summary>
/// RabbitMQ生产者
/// </summary>
public class RabbitMQProducer
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="exchangeName"></param>
    /// <param name="queueName"></param>
    /// <param name="routingKey"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Task<bool> ProducerSend(MQConnection conn, string? exchangeName, string? queueName, string? routingKey, string message)
    {
        try
        {
            var connFactory = new ConnectionFactory
            {
                HostName = conn.HostName,
                Port = conn.Port,
                UserName = conn.UserName,
                Password = conn.Password,
                VirtualHost = conn.VirtualHost
            };
            // 创建连接
            using var connection = connFactory.CreateConnection();
            // 创建信道
            using var channel = connection.CreateModel();
            // 声明交换机
            channel.ExchangeDeclare(exchange: exchangeName, type: "fanout", durable: false, autoDelete: false, arguments: null);
            // 创建队列
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            // 绑定交换机、队列、路由
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
            // 发送消息
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchangeName, routingKey: queueName, basicProperties: null, body: body);
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
            throw;
        }
    }
}