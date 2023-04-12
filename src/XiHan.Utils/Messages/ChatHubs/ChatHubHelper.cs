using Microsoft.AspNetCore.SignalR;

namespace XiHan.Utils.Messages.ChatHubs;

/// <summary>
/// 即时通讯
/// </summary>
public class ChatHubHelper : Hub
{
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}