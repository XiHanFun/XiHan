#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:KafkaConnectionPool
// Guid:7da6fca3-4d0c-429e-91f5-c9347c77695d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 5:14:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace XiHan.Infrastructure.EventBus.Kafka;

/// <summary>
/// Kafka producer 连接池管理
/// 可以使用微软官方的对象池进行构造ObjectPool
/// </summary>
public class KafkaConnectionPool : IKafkaConnectionPool
{
    private readonly KafkaOptions _options;

    private readonly ConcurrentQueue<IProducer<string, byte[]>> _producerPool = new();
    private int _currentCount;
    private readonly int _maxSize;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public KafkaConnectionPool(IOptions<KafkaOptions> options)
    {
        _options = options.Value;
        _maxSize = _options.ConnectionPoolSize;
    }

    /// <summary>
    /// 获取连接
    /// </summary>
    /// <returns></returns>
    public IProducer<string, byte[]> Producer()
    {
        if (_producerPool.TryDequeue(out var producer))
        {
            Interlocked.Decrement(ref _currentCount);
            return producer;
        }

        var config = new ProducerConfig()
        {
            BootstrapServers = _options.Servers,
            QueueBufferingMaxMessages = 10,
            MessageTimeoutMs = 5000,
            RequestTimeoutMs = 3000
        };

        producer = new ProducerBuilder<string, byte[]>(config)
            .Build();
        return producer;
    }

    /// <summary>
    /// 归还连接
    /// </summary>
    /// <param name="producer"></param>
    /// <returns></returns>
    public bool Return(IProducer<string, byte[]> producer)
    {
        if (Interlocked.Increment(ref _currentCount) <= _maxSize)
        {
            _producerPool.Enqueue(producer);
            return true;
        }

        producer.Dispose();
        Interlocked.Decrement(ref _currentCount);

        return false;
    }
}