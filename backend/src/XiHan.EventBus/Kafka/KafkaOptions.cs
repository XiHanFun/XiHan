#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:KafkaOptions
// Guid:b15d7eca-772a-4580-9c8d-41be1bc3a8c4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/6 5:14:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.EventBus.Kafka;

/// <summary>
/// Kafka 配置项
/// </summary>
public class KafkaOptions
{
    /// <summary>
    /// 连接池大小
    /// </summary>
    public int ConnectionPoolSize { get; set; } = 10;

    /// <summary>
    /// 服务器地址
    /// </summary>
    public string Servers { get; set; } = string.Empty;

    /// <summary>
    /// 主题
    /// </summary>
    public string Topic { get; set; } = string.Empty;

    /// <summary>
    /// 消费者组
    /// </summary>
    public string GroupId { get; set; } = string.Empty;

    /// <summary>
    /// 分区数量
    /// </summary>
    public int NumPartitions { get; set; }
}