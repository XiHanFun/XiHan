#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ChatHub
// Guid:ee669dee-30c7-4d21-8eb4-f24d8dc0f44c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-16 上午 03:59:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.SignalR;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Subscriptions.Hubs.Consts;
using XiHan.Subscriptions.Hubs.Dtos;
using XiHan.Subscriptions.Hubs.Entities;

namespace XiHan.Subscriptions.Hubs;

/// <summary>
/// 即时通讯集线器
/// </summary>
public class ChatHub : Hub<IChatHub>
{
    /// <summary>
    /// 创建用户集合，用于存储所有链接的用户数据
    /// </summary>
    public static readonly List<OnlineUser> OnlineUsers = new();

    private readonly IAppCacheService _appCacheService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    public ChatHub(IAppCacheService appCacheService)
    {
        _appCacheService = appCacheService;
    }

    #region 链接新建与断开

    /// <summary>
    /// 连接方法重写
    /// </summary>
    /// <returns></returns>
    public override async Task OnConnectedAsync()
    {
        // 获取当前请求上下文信息
        var clientInfo = App.ClientInfo;
        var addressInfo = App.AddressInfo;
        var authInfo = App.AuthInfo;

        var onlineUser = new OnlineUser
        {
            ConnectionId = Context.ConnectionId,
            UserId = authInfo.UserId,
            Account = authInfo.Account,
            NickName = authInfo.NickName,
            RealName = authInfo.RealName,
            Ip = addressInfo.RemoteIPv4,
            Location = addressInfo.Country + "|" + addressInfo.State + "|" + addressInfo.PrefectureLevelCity,
            DeviceType = clientInfo.DeviceType,
            Os = clientInfo.OsName + clientInfo.OsVersion,
            Browser = clientInfo.BrowserName + clientInfo.BrowserVersion,
            ConnectionTime = DateTime.Now,
        };
        _ = OnlineUsers.Append(onlineUser);
        _ = _appCacheService.Set($"UserOnline_{onlineUser.UserId}", onlineUser);

        await AddToGroup(HubConst.CommonGroup);

        await base.OnConnectedAsync();
    }

    /// <summary>
    /// 断连方法重写
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var onlineUser = OnlineUsers.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefault();
        if (onlineUser == null) return;

        _ = OnlineUsers.Remove(onlineUser);
        _ = _appCacheService.Remove($"UserOnline_{onlineUser.UserId}");

        await RemoveFromGroup(HubConst.CommonGroup);

        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="onlineUserCDto"></param>
    /// <returns></returns>
    public async Task ForceOffline(OnlineUserCDto onlineUserCDto)
    {
        await Clients.Client(onlineUserCDto.ConnectionId).ForceOffline(HubConst.ForceOffline);
    }

    #endregion

    #region 群组加入与退出

    /// <summary>
    /// 加入指定组
    /// </summary>
    /// <param name="groupName">组名</param>
    /// <returns></returns>
    [HubMethodName("AddToGroup")]
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await SendMessageToGroup(new GroupMessageCDto
        {
            GroupNames = new List<string>() { groupName },
            Title = HubConst.GroupAdded
        });
    }

    /// <summary>
    /// 退出指定组
    /// </summary>
    /// <param name="groupName">组名</param>
    /// <returns></returns>
    [HubMethodName("RemoveFromGroup")]
    public async Task RemoveFromGroup(string groupName)
    {
        await SendMessageToGroup(new GroupMessageCDto
        {
            GroupNames = new List<string>() { groupName },
            Title = HubConst.GroupRemoved
        });
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    #endregion

    #region 发送消息给用户或群组

    /// <summary>
    /// 发送消息给所有用户
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToAllUser")]
    public async Task SendMessageToAllUser(UserMessageCDto message)
    {
        await Clients.All.ReceiveMessage(message);
    }

    /// <summary>
    /// 发送消息给用户
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToUser")]
    public async Task SendMessageToUser(UserMessageCDto message)
    {
        var userlist = new List<string>();
        foreach (var userId in message.UserIds)
        {
            var user = _appCacheService.Get<OnlineUser>($"UserOnline_{userId}");
            if (user != null) userlist.Add(user.ConnectionId);
        }
        await Clients.Clients(userlist).ReceiveMessage(message);
    }

    /// <summary>
    /// 发送消息给群组
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessageToGroup")]
    public async Task SendMessageToGroup(GroupMessageCDto message)
    {
        await Clients.Groups(message.GroupNames).ReceiveMessage(message);
    }

    /// <summary>
    /// 发送错误信息到客户端
    /// </summary>
    /// <returns></returns>
    /// <exception cref="HubException"></exception>
    [HubMethodName("ThrowException")]
    public async Task ThrowException()
    {
        await Task.Run(() =>
        {
            throw new HubException("This error will be sent to the client!");
        });
    }

    #endregion
}