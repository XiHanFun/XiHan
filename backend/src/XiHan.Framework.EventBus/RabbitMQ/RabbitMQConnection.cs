#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:RabbitMQConnection
// Guid:f4084e83-9f9a-4c39-85c6-37d9d9afa78b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 4:43:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace XiHan.Framework.EventBus.RabbitMQ;

/// <summary>
/// RabbitMQ 持久连接
/// </summary>
public class RabbitMQConnection : IRabbitMQConnection
{
    private readonly ILogger<RabbitMQConnection> _logger;
    private readonly IConnectionFactory _connectionFactory;
    private readonly int _retryCount;
    private IConnection _connection;
    private readonly bool _disposed;
    private readonly object sync_root = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="connectionFactory"></param>
    /// <param name="retryCount"></param>
    public RabbitMQConnection(ILogger<RabbitMQConnection> logger,
        IConnectionFactory connectionFactory,
        int retryCount = 5)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _retryCount = retryCount;
    }

    /// <summary>
    /// 是否已经连接
    /// </summary>
    public bool IsConnected
    {
        get
        {
            return _connection != null && _connection.IsOpen && !_disposed;
        }
    }

    /// <summary>
    /// 尝试重连
    /// </summary>
    /// <returns></returns>
    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ客户端正在尝试连接");

        lock (sync_root)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                        {
                            _logger.LogWarning(ex, $"RabbitMQ客户端在{time.TotalSeconds:n1} ({ex.Message})之后无法连接");
                        }
            );

            policy.Execute(() =>
            {
                _connection = _connectionFactory
                      .CreateConnection();
            });

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation($"RabbitMQ客户端获取到'{_connection.Endpoint.HostName}'的持久连接，并订阅失败事件");

                return true;
            }
            else
            {
                _logger.LogCritical("致命错误:无法创建和打开RabbitMQ连接");

                return false;
            }
        }
    }

    /// <summary>
    /// 创建 Model
    /// </summary>
    /// <returns></returns>
    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("没有可用的RabbitMQ连接来执行此操作");
        }

        return _connection.CreateModel();
    }

    /// <summary>
    /// 发布消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exchangeName"></param>
    /// <param name="routingKey"></param>
    public void PublishMessage(string message, string exchangeName, string routingKey)
    {
        if (!IsConnected)
        {
            _logger.LogWarning("RabbitMQ客户端没有连接到代理来发布消息");
            return;
        }

        using var channel = CreateModel();
        channel.ExchangeDeclare(exchange: exchangeName, type: "direct");

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);

        _logger.LogInformation($"在路由密钥“{routingKey}”的交换机“{exchangeName}”上已发布消息“{message}”。");
    }

    /// <summary>
    /// 订阅消息
    /// </summary>
    /// <param name="queueName"></param>
    public void StartConsuming(string queueName)
    {
        using var channel = CreateModel();
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var header = ea.BasicProperties.Headers;
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await Task.CompletedTask;

            _logger.LogInformation($"从队列“{queueName}”收到消息“{message}”");
        };
        _connection.CreateModel();
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    #region 内部方法

    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed)
            return;

        _logger.LogWarning("RabbitMQ连接被关闭。尝试重新连接…");

        TryConnect();
    }

    private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (_disposed)
            return;

        _logger.LogWarning("RabbitMQ连接抛出异常。尝试重新连接…");

        TryConnect();
    }

    private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed)
            return;

        _logger.LogWarning("RabbitMQ连接处于关闭状态。尝试重新连接…");

        TryConnect();
    }

    #endregion
}