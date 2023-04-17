﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ChatHub
// Guid:ee669dee-30c7-4d21-8eb4-f24d8dc0f44c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-16 上午 03:59:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.SignalR;

namespace XiHan.Infrastructure.Hubs;

/// <summary>
/// 即时通讯
/// </summary>
public class ChatHub : Hub
{
    /// <summary>
    /// 发送消息给所有人
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToAllUser")]
    public async Task SendMessageToAllUser(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    /// <summary>
    /// 发送消息给指定人
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToDesignatedUser")]
    public async Task SendMessageToDesignatedUser(string user, string message)
    {
        await Clients.User(user).SendAsync("ReceiveMessage", user, message);
    }

    /// <summary>
    /// 发送消息给呼叫者
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageToCaller(string user, string message)
    {
        await Clients.Caller.SendAsync("ReceiveMessage", user, message);
    }

    #region 群组

    /// <summary>
    /// 加入指定组
    /// </summary>
    /// <param name="groupName">组名</param>
    /// <returns></returns>
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    /// <summary>
    /// 退出指定组
    /// </summary>
    /// <param name="groupName">组名</param>
    /// <returns></returns>
    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    /// <summary>
    /// 发送消息给Top群组
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageToTopGroup(string user, string message)
    {
        await Clients.Group("Top").SendAsync("ReceiveMessage", user, message);
    }

    /// <summary>
    /// 发送消息给指定群组
    /// </summary>
    /// <param name="group"></param>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageToDesignatedGroup(string group, string user, string message)
    {
        await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    #endregion

    /// <summary>
    /// SignalR接收信息
    /// </summary>
    /// <param name="message">信息内容</param>
    /// <returns></returns>
    public async Task ReceiveMessage(string message)
    { }

    /// <summary>
    /// SignalR接收信息
    /// </summary>
    /// <param name="user">指定接收客户端</param>
    /// <param name="message">信息内容</param>
    /// <returns></returns>
    public async Task ReceiveMessage(string user, string message)
    { }

    /// <summary>
    /// 消息更新
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task ReceiveUpdate(object message)
    { }

    /// <summary>
    /// 错误信息发送到客户端
    /// </summary>
    /// <returns></returns>
    /// <exception cref="HubException"></exception>
    public Task ThrowException()
    {
        throw new HubException("This error will be sent to the client!");
    }

    /// <summary>
    /// 连接方法重写
    /// </summary>
    /// <returns></returns>
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
        await base.OnConnectedAsync();
    }

    /// <summary>
    /// 断连方法重写
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", "添加新连接", "disconnect");
        await base.OnDisconnectedAsync(exception);
    }
}