// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DingTalkRobot
// Guid:b9ebb234-1ebf-4b97-b308-0c525d2cd190
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 12:48:23
// ----------------------------------------------------------------

using System.Security.Cryptography;
using System.Text;
using ZhaiFanhuaBlog.Utils.Encryptions;
using ZhaiFanhuaBlog.Utils.Http;
using ZhaiFanhuaBlog.Utils.Serialize;

namespace ZhaiFanhuaBlog.Utils.MessagePush.DingTalk;

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
    /// 网络挂钩地址
    /// </summary>
    private readonly string _WebHookUrl = string.Empty;

    /// <summary>
    /// 访问令牌
    /// </summary>
    private readonly string _AccessToken = string.Empty;

    /// <summary>
    /// 正式访问地址
    /// </summary>
    private readonly string _Url = string.Empty;

    /// <summary>
    /// 机密
    /// </summary>
    private readonly string? _Secret = string.Empty;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    /// <param name="webHookUrl"></param>
    /// <param name="accessToken"></param>
    /// <param name="secret"></param>
    public DingTalkRobot(IHttpHelper iHttpHelper, string webHookUrl, string accessToken, string? secret)
    {
        _IHttpHelper = iHttpHelper;
        _WebHookUrl = webHookUrl;
        _AccessToken = accessToken;
        _Url = _WebHookUrl + "?access_token=" + _AccessToken;
        _Secret = secret;
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <param name="atMobiles">被@的人群</param>
    /// <param name="isAtAll">是否@全员</param>
    /// <returns></returns>
    public async Task<ResultInfo?> TextMessage(Text text, List<string>? atMobiles = null, bool isAtAll = false)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.text.ToString();
        // 指定目标人群
        var at = new At()
        {
            AtMobiles = atMobiles,
            IsAtAll = isAtAll
        };
        // 发送
        var result = await Send(new { msgtype, text, at });
        return result;
    }

    /// <summary>
    /// 发送链接消息
    /// </summary>
    /// <param name="link"></param>
    public async Task<ResultInfo?> LinkMessage(Link link)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.link.ToString();
        // 发送
        var result = await Send(new { msgtype, link });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="markdown">Markdown内容</param>
    /// <param name="atMobiles">被@的人群</param>
    /// <param name="isAtAll">是否@全员</param>
    public async Task<ResultInfo?> MarkdownMessage(Markdown markdown, List<string>? atMobiles = null, bool isAtAll = false)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.markdown.ToString();
        // 指定目标人群
        var at = new At()
        {
            AtMobiles = atMobiles,
            IsAtAll = isAtAll
        };
        // 发送
        var result = await Send(new { msgtype, markdown, at });
        return result;
    }

    /// <summary>
    /// 发送任务卡片消息
    /// </summary>
    /// <param name="actionCard">ActionCard内容</param>
    public async Task<ResultInfo?> ActionCardMessage(ActionCard actionCard)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.actionCard.ToString();
        // 发送
        var result = await Send(new { msgtype, actionCard });
        return result;
    }

    /// <summary>
    /// 发送卡片菜单消息
    /// </summary>
    /// <param name="feedCard">FeedCard内容</param>
    public async Task<ResultInfo?> FeedCardMessage(FeedCard feedCard)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.feedCard.ToString();
        // 发送
        var result = await Send(new { msgtype, feedCard });
        return result;
    }

    /// <summary>
    /// 钉钉执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ResultInfo?> Send(object objSend)
    {
        var url = _Url;
        var sendMessage = objSend.SerializeToJson();
        var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        // 安全设置加签，需要使用UTF-8字符集
        if (!string.IsNullOrEmpty(_Secret))
        {
            // 把timestamp + "\n" + 密钥当做签名字符串
            var sign = timeStamp + "\n" + _Secret;
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(_Secret);
            byte[] messageBytes = encoding.GetBytes(sign);
            // 使用HmacSHA256算法计算签名
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                // 然后进行Base64 encode，最后再把签名参数再进行urlEncode
                sign = Convert.ToBase64String(hashmessage).ToUrlEncode();
            }
            // 得到最终的签名
            url += $"&timestamp={timeStamp}&sign={sign}";
        }
        // 发起请求
        ResultInfo? result = await _IHttpHelper.PostAsync<ResultInfo>(HttpEnum.LocalHost, url, sendMessage, null);
        return result;
    }
}