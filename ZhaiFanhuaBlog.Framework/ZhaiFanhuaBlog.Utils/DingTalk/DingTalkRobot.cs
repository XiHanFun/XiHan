using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ZhaiFanhuaBlog.Utils.DingTalk.Input;

namespace ZhaiFanhuaBlog.Utils.DingTalk;

/// <summary>
/// 钉钉机器人消息推送
/// </summary>
public static class DingTalkRobot
{
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="weebhook">webhook链接</param>
    /// <param name="msgType">消息类型</param>
    /// <param name="msg">消息内容</param>
    /// <param name="atuserlist">@用户的手机号组</param>
    /// <param name="atall">@所有人</param>
    public static void SendMessage(string weebhook, MessageTypeEnum msgType, IBaseMessage msg, string secret = "", List<string> atuserlist = null, bool atall = false)
    {
        var _timespan = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() * 1000;
        var url = weebhook;
        if (!string.IsNullOrEmpty(secret))
        {
            var _sign = addSign(_timespan, secret);
            url += $"&timestamp={_timespan}&sign={_sign}";
        }
        IDingTalkClient client = new DefaultDingTalkClient(url);
        OapiRobotSendRequest request = new();
        var _this = typeof(DingTalkRobot);
        var action = _this.GetMethod(msgType.ToString(), BindingFlags.Static | BindingFlags.NonPublic);
        action.Invoke(null, new object[] { request, msg });
        request.AtUser(atuserlist, atall);
        OapiRobotSendResponse response = client.Execute(request);
    }

    /// <summary>
    /// 发送text消息
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="atuserlist"></param>
    /// <returns></returns>
    private static void TextMessage(OapiRobotSendRequest request, IBaseMessage msg)
    {
        request.Msgtype = "text";
        request.Text_ = msg.Adapt<TextDomainInput>();
    }

    /// <summary>
    /// 发送链接消息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="MessageUrl">消息链接</param>
    /// <param name="PicUrl">图片链接</param>
    /// <param name="Title">标题</param>
    /// <param name="Text">说明</param>
    private static void LinkMessage(OapiRobotSendRequest request, IBaseMessage msg)
    {
        request.Msgtype = "link";
        request.Link_ = msg.Adapt<OapiRobotSendRequest.LinkDomain>();
    }

    /// <summary>
    /// 发送Markdown消息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="Title"></param>
    /// <param name="Text"></param>
    private static void MarkdownMessage(OapiRobotSendRequest request, IBaseMessage msg)
    {
        request.Msgtype = "markdown";
        request.Markdown_ = msg.Adapt<OapiRobotSendRequest.MarkdownDomain>();
    }

    /// <summary>
    ///发送任务卡片消息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="Title"></param>
    /// <param name="Text"></param>
    /// <param name="SingleTitle"></param>
    /// <param name="SingleURL"></param>
    private static void ActionCardMessage(OapiRobotSendRequest request, IBaseMessage msg)
    {
        request.Msgtype = "actionCard";
        request.ActionCard_ = msg.Adapt<OapiRobotSendRequest.ActioncardDomain>();
    }

    /// <summary>
    /// 发送卡片菜单消息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="msglist"></param>
    private static void FeedCardMessage(OapiRobotSendRequest request, IBaseMessage msg)
    {
        request.Msgtype = "feedCard";
        request.FeedCard_ = msg.Adapt<OapiRobotSendRequest.FeedcardDomain>();
    }

    /// <summary>
    /// 用户
    /// </summary>
    /// <param name="request"></param>
    /// <param name="atuserlist"></param>
    private static void AtUser(this OapiRobotSendRequest request, List<string> atuserlist, bool IsAtAll = false)
    {
        OapiRobotSendRequest.AtDomain at = new OapiRobotSendRequest.AtDomain();
        if (!IsAtAll)
        {
            at.AtMobiles = atuserlist;
        }
        at.IsAtAll = IsAtAll;
        request.At_ = at;
    }

    /// <summary>
    /// 加签
    /// </summary>
    /// <param name="zTime">当前时间戳</param>
    /// <returns></returns>
    private static string addSign(long zTime, string secret)
    {
        string stringToSign = zTime + "\n" + secret;
        var encoding = new ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(stringToSign);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return HttpUtility.UrlEncode(Convert.ToBase64String(hashmessage), Encoding.UTF8);
        }
    }
}