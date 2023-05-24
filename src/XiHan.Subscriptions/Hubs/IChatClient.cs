#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:IChatClient
// Guid:4e4bd472-94b5-4e53-a02e-9f39156f5961
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-18 上午 01:30:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Subscriptions.Hubs;

/// <summary>
/// IChatClient
/// </summary>
public interface IChatClient
{
    /// <summary>
    /// 接收信息
    /// </summary>
    /// <param name="message">信息内容</param>
    /// <returns></returns>
    Task ReceiveMessage(object message);

    /// <summary>
    /// 接收信息
    /// </summary>
    /// <param name="user">指定接收客户端</param>
    /// <param name="message">信息内容</param>
    /// <returns></returns>
    Task ReceiveMessage(string user, string message);

    /// <summary>
    /// 消息更新
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task ReceiveUpdate(object message);
}