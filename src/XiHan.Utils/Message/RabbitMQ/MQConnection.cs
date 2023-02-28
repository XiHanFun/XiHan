#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MQConnection
// Guid:2be95905-9bde-4006-84ff-54dc5e348dad
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-24 上午 02:46:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.Message.RabbitMQ;

/// <summary>
/// MQConnection
/// </summary>
public class MqConnection
{
    /// <summary>
    /// 主机名称
    /// </summary>
    public string HostName { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// 连接名称
    /// </summary>
    public string? UserName { get; set; } = string.Empty;

    /// <summary>
    /// 连接密码
    /// </summary>
    public string? Password { get; set; } = string.Empty;

    /// <summary>
    /// 虚拟主机
    /// </summary>
    public string? VirtualHost { get; set; } = "/";
}