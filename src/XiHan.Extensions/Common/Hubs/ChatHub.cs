#region <<版权版本注释>>

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
using XiHan.Infrastructure.Apps;

namespace XiHan.Extensions.Common.Hubs;

/// <summary>
/// 即时通讯
/// </summary>
public class ChatHub : Hub
{
    ///// <summary>
    ///// 客户端连接的时候调用
    ///// </summary>
    ///// <returns></returns>
    //public override Task OnConnectedAsync()
    //{
    //    var name = HttpContextExtension.GetName(App.HttpContext);// Context.User.Identity.Name;
    //    var ip = HttpContextExtension.GetClientUserIp(App.HttpContext);
    //    var ip_info = IpTool.Search(ip);

    //    var userid = HttpContextExtension.GetUId(App.HttpContext);
    //    var user = clientUsers.Any(u => u.ConnnectionId == Context.ConnectionId);
    //    //判断用户是否存在，否则添加集合
    //    if (!user && Context.User.Identity.IsAuthenticated)
    //    {
    //        OnlineUsers users = new(Context.ConnectionId, name, userid, ip)
    //        {
    //            Location = ip_info.City
    //        };
    //        clientUsers.Add(users);
    //        Console.WriteLine($"{DateTime.Now}：{name},{Context.ConnectionId}连接服务端success，当前已连接{clientUsers.Count}个");
    //        //Clients.All.SendAsync("welcome", $"欢迎您：{name},当前时间：{DateTime.Now}");
    //        Clients.All.SendAsync(HubsConstant.MoreNotice, SendNotice());
    //    }

    //    Clients.All.SendAsync(HubsConstant.OnlineNum, clientUsers.Count);
    //    Clients.All.SendAsync(HubsConstant.OnlineUser, clientUsers);
    //    return base.OnConnectedAsync();
    //}

    ///// <summary>
    ///// 连接终止时调用。
    ///// </summary>
    ///// <returns></returns>
    //public override Task OnDisconnectedAsync(Exception? exception)
    //{
    //    var user = clientUsers.Where(p => p.ConnnectionId == Context.ConnectionId).FirstOrDefault();
    //    //判断用户是否存在，否则添加集合
    //    if (user != null)
    //    {
    //        clientUsers.Remove(user);
    //        Clients.All.SendAsync(HubsConstant.OnlineNum, clientUsers.Count);
    //        Clients.All.SendAsync(HubsConstant.OnlineUser, clientUsers);
    //        Console.WriteLine($"用户{user?.Name}离开了，当前已连接{clientUsers.Count}个");
    //    }
    //    return base.OnDisconnectedAsync(exception);
    //}

    /// <summary>
    /// 注册消息
    /// </summary>
    /// <param name="connectId"></param>
    /// <param name="userName"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    [HubMethodName("SendMessage")]
    public async Task SendMessage(string connectId, string userName, string message)
    {
        Console.WriteLine($"connectId={connectId},message={message}");

        await Clients.Client(connectId).SendAsync("receiveChat", new { userName, message });
    }
}