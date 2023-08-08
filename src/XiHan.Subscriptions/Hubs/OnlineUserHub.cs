#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnlineUserHub
// Guid:ee669dee-30c7-4d21-8eb4-f24d8dc0f44c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-16 上午 03:59:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.SignalR;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Consts;

namespace XiHan.Subscriptions.Hubs;

/// <summary>
/// 在线用户集线器
/// </summary>
public class OnlineUserHub : Hub<IOnlineUserHub>
{
    private readonly IHubContext<OnlineUserHub, IOnlineUserHub> _onlineUserHubContext;
    private readonly IAppCacheService _appCacheService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineUserHubContext"></param>
    /// <param name="appCacheService"></param>
    public OnlineUserHub(IHubContext<OnlineUserHub, IOnlineUserHub> onlineUserHubContext, IAppCacheService appCacheService)
    {
        _onlineUserHubContext = onlineUserHubContext;
        _appCacheService = appCacheService;
    }

    #region 链接新建与断开

    /// <summary>
    /// 连接方法重写
    /// </summary>
    /// <returns></returns>
    public override async Task OnConnectedAsync()
    {
        var content = Context.GetHttpContext();
        // 获取当前请求上下文信息
        var clientInfo = App.ClientInfo;
        var addressInfo = App.AddressInfo;
        var authInfo = App.AuthInfo;

        var onlineUser = new OnlineUser
        {
            ConnectionId = Context.ConnectionId,
            UserId = authInfo.UserId,
            Account = authInfo.Account,
            RealName = authInfo.re
        };

        await AddToGroup(HubConst.CommonGroup);
        await Clients.Group(HubConst.CommonGroup).ReceiveMessage(HubConst.Connected);
        await base.OnConnectedAsync();
    }

    /// <summary>
    /// 断连方法重写
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.Group(HubConst.CommonGroup).ReceiveMessage(HubConst.Disconnected);
        await RemoveFromGroup(HubConst.CommonGroup);
        await base.OnDisconnectedAsync(exception);
    }

    #endregion

    #region 发送消息给用户或群组

    /// <summary>
    /// 发送消息给用户
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToUser")]
    public async Task SendMessageToUser(string user, string message)
    {
        await Clients.User(user).ReceiveMessage(user, message);
    }

    /// <summary>
    /// 发送消息给呼叫用户
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToCaller")]
    public async Task SendMessageToCaller(string message)
    {
        await Clients.Caller.ReceiveMessage(message);
    }

    /// <summary>
    /// 发送消息给所有用户
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToAllUser")]
    public async Task SendMessageToAllUser(string message)
    {
        await Clients.All.ReceiveMessage(message);
    }

    /// <summary>
    /// 发送消息给群组
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToGroup")]
    public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).ReceiveMessage(message);
    }

    /// <summary>
    /// 发送消息给公共群组
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToCommonGroup")]
    public async Task SendMessageToCommonGroup(string message)
    {
        await Clients.Group(HubConst.CommonGroup).ReceiveMessage(message);
    }

    /// <summary>
    /// 发送错误信息到客户端
    /// </summary>
    /// <returns></returns>
    /// <exception cref="HubException"></exception>
    [HubMethodName("ThrowException")]
    public Task ThrowException()
    {
        throw new HubException("This error will be sent to the client!");
    }

    #endregion

    #region 群组加入与退出

    /// <summary>
    /// 加入指定组
    /// </summary>
    /// <param name="groupNameName">组名</param>
    /// <returns></returns>
    [HubMethodName("AddToGroup")]
    public async Task AddToGroup(string groupNameName)
    {
        await SendMessageToGroup(groupNameName, HubConst.GroupAdded);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupNameName);
    }

    /// <summary>
    /// 退出指定组
    /// </summary>
    /// <param name="groupNameName">组名</param>
    /// <returns></returns>
    [HubMethodName("RemoveFromGroup")]
    public async Task RemoveFromGroup(string groupNameName)
    {
        await SendMessageToGroup(groupNameName, HubConst.GroupRemoved);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupNameName);
    }

    #endregion
}