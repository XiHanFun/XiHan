#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IKafkaConnectionPool
// Guid:2eeec587-73c8-4227-a279-d9be96a4abfa
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 5:13:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Confluent.Kafka;

namespace XiHan.Infrastructure.EventBus.Kafka;

/// <summary>
/// Kafka 连接池
/// </summary>
public interface IKafkaConnectionPool
{
    /// <summary>
    /// 取对象
    /// </summary>
    /// <returns></returns>
    IProducer<string, byte[]> Producer();

    /// <summary>
    /// 将对象放入连接池
    /// </summary>
    /// <param name="producer"></param>
    /// <returns></returns>
    bool Return(IProducer<string, byte[]> producer);
}