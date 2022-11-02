using System.Buffers.Text;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ZhaiFanhuaBlog.Utils.Console;
using ZhaiFanhuaBlog.Utils.DingTalk.Input;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.Utils.Http;

namespace ZhaiFanhuaBlog.Utils.DingTalk;

/// <summary>
/// 钉钉机器人消息推送
/// </summary>
public class DingTalkRobot
{
    /// <summary>
    /// 请求接口
    /// </summary>
    private readonly IHttpHelper _IHttpHelper;

    /// <summary>
    /// Webhook 地址
    /// </summary>
    public readonly string _WebHookUrl = string.Empty;

    /// <summary>
    /// 关键字
    /// </summary>
    public readonly string _KeyWord = string.Empty;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    /// <param name="webHookUrl"></param>
    /// <param name="keyWord"></param>
    public DingTalkRobot(IHttpHelper iHttpHelper, string webHookUrl, string keyWord)
    {
        _IHttpHelper = iHttpHelper;
        _WebHookUrl = webHookUrl;
        _KeyWord = keyWord;
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="model">消息内容</param>
    /// <param name="appSecret">机器人机密</param>
    public async Task<string> Send<IBaseModel>(IBaseModel model, string? appSecret = "")
    {
        var url = _WebHookUrl;
        var timeSpan = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() * 1000;
        if (!string.IsNullOrEmpty(appSecret))
        {
            // 当前时间戳加签名
            var sign = Encoding.UTF8.GetBytes(SHAHelper.EncryptSHA256(Encoding.UTF8, timeSpan + "\n" + appSecret)).ToString();
            url += $"&timestamp={timeSpan}&sign={sign}";
        }
        string result = await _IHttpHelper.PostAsync(HttpEnum.LocalHost, url, model, null);
        return result;
    }

    /// <summary>
    /// 发送 Text 消息
    /// </summary>
    /// <param name="textModel"></param>
    /// <param name="atUserList">@用户的手机号组</param>
    /// <param name="atAll">@所有人</param>
    /// <returns></returns>
    public async Task<string> TextMessage(TextModel textModel, List<string>? atUserList = null, bool? atAll = false)
    {
        if (atUserList != null)
        {
            var listToString = string.Join(",", atUserList);
        }
        textModel.Text.Content = _KeyWord + textModel.Text.Content;
        string result = await Send<TextModel>(textModel);
        ConsoleHelper.WriteLineWarning(result);
        return result;
    }

    ///// <summary>
    ///// 发送链接消息
    ///// </summary>
    ///// <param name="request"></param>
    ///// <param name="MessageUrl">消息链接</param>
    ///// <param name="PicUrl">图片链接</param>
    ///// <param name="Title">标题</param>
    ///// <param name="Text">说明</param>
    //public static void LinkMessage(OapiRobotSendRequest request, IBaseMessage msg)
    //{
    //    request.Msgtype = "link";
    //    request.Link_ = msg.Adapt<OapiRobotSendRequest.LinkDomain>();
    //}

    ///// <summary>
    ///// 发送Markdown消息
    ///// </summary>
    ///// <param name="request"></param>
    ///// <param name="Title"></param>
    ///// <param name="Text"></param>
    //public static void MarkdownMessage(OapiRobotSendRequest request, IBaseMessage msg)
    //{
    //    request.Msgtype = "markdown";
    //    request.Markdown_ = msg.Adapt<OapiRobotSendRequest.MarkdownDomain>();
    //}

    ///// <summary>
    ///// 发送任务卡片消息
    ///// </summary>
    ///// <param name="request"></param>
    ///// <param name="Title"></param>
    ///// <param name="Text"></param>
    ///// <param name="SingleTitle"></param>
    ///// <param name="SingleURL"></param>
    //public static void ActionCardMessage(OapiRobotSendRequest request, IBaseMessage msg)
    //{
    //    request.Msgtype = "actionCard";
    //    request.ActionCard_ = msg.Adapt<OapiRobotSendRequest.ActioncardDomain>();
    //}

    ///// <summary>
    ///// 发送卡片菜单消息
    ///// </summary>
    ///// <param name="request"></param>
    ///// <param name="msglist"></param>
    //public static void FeedCardMessage(OapiRobotSendRequest request, IBaseMessage msg)
    //{
    //    request.Msgtype = "feedCard";
    //    request.FeedCard_ = msg.Adapt<OapiRobotSendRequest.FeedcardDomain>();
    //}

    ///// <summary>
    ///// 用户
    ///// </summary>
    ///// <param name="request"></param>
    ///// <param name="atuserlist"></param>
    //public static void AtUser(this OapiRobotSendRequest request, List<string> atuserlist, bool IsAtAll = false)
    //{
    //    OapiRobotSendRequest.AtDomain at = new OapiRobotSendRequest.AtDomain();
    //    if (!IsAtAll)
    //    {
    //        at.AtMobiles = atuserlist;
    //    }
    //    at.IsAtAll = IsAtAll;
    //    request.At_ = at;
    //}
}