#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DingTalkCustomRobot
// Guid:b9ebb234-1ebf-4b97-b308-0c525d2cd190
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 12:48:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.Security.Cryptography;
using System.Text;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.Subscriptions.Robots.DingTalk;

/// <summary>
/// 钉钉自定义机器人消息推送
/// https://open.dingtalk.com/document/orgapp/custom-robot-access
/// </summary>
public class DingTalkCustomRobot
{
    private readonly IHttpPollyHelper _httpPolly;
    private readonly string _url;
    private readonly string _secret;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    /// <param name="dingTalkConnection"></param>
    public DingTalkCustomRobot(IHttpPollyHelper httpPolly, DingTalkConnection dingTalkConnection)
    {
        _httpPolly = httpPolly;
        _url = dingTalkConnection.WebHookUrl + "?access_token=" + dingTalkConnection.AccessToken;
        _secret = dingTalkConnection.Secret;
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <param name="atMobiles">被@的人群</param>
    /// <param name="isAtAll">是否@全员</param>
    /// <returns></returns>
    public async Task<BaseResultDto> TextMessage(DingTalkText text, List<string>? atMobiles = null, bool isAtAll = false)
    {
        // 消息类型
        var msgtype = DingTalkMsgTypeEnum.Text.GetEnumDescriptionByKey();
        // 指定目标人群
        var at = new DingTalkAt
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
    public async Task<BaseResultDto> LinkMessage(DingTalkLink link)
    {
        // 消息类型
        var msgtype = DingTalkMsgTypeEnum.Link.GetEnumDescriptionByKey();
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
    public async Task<BaseResultDto> MarkdownMessage(DingTalkMarkdown markdown, List<string>? atMobiles = null, bool isAtAll = false)
    {
        // 消息类型
        var msgtype = DingTalkMsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        // 指定目标人群
        var at = new DingTalkAt
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
    public async Task<BaseResultDto> ActionCardMessage(DingTalkActionCard actionCard)
    {
        // 消息类型
        var msgtype = DingTalkMsgTypeEnum.ActionCard.GetEnumDescriptionByKey();
        // 发送
        var result = await Send(new { msgtype, actionCard });
        return result;
    }

    /// <summary>
    /// 发送卡片菜单消息
    /// </summary>
    /// <param name="feedCard">FeedCard内容</param>
    public async Task<BaseResultDto> FeedCardMessage(DingTalkFeedCard feedCard)
    {
        // 消息类型
        var msgtype = DingTalkMsgTypeEnum.FeedCard.GetEnumDescriptionByKey();
        // 发送
        var result = await Send(new { msgtype, feedCard });
        return result;
    }

    /// <summary>
    /// 钉钉执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<BaseResultDto> Send(object objSend)
    {
        var url = _url;
        var sendMessage = objSend.SerializeToJson();
        var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        // 安全设置加签，需要使用 UTF-8 字符集
        if (!string.IsNullOrEmpty(_secret))
        {
            // 把 【timestamp + "\n" + 密钥】 当做签名字符串
            var sign = timeStamp + "\n" + _secret;
            var encoding = new UTF8Encoding();
            var keyByte = encoding.GetBytes(_secret);
            var messageBytes = encoding.GetBytes(sign);
            // 使用 HmacSHA256 算法计算签名
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hashmessage = hmacsha256.ComputeHash(messageBytes);
                // 然后进行 Base64 encode，最后再把签名参数再进行 urlEncode
                sign = Convert.ToBase64String(hashmessage).UrlEncode();
            }
            // 得到最终的签名
            url += $"&timestamp={timeStamp}&sign={sign}";
        }
        // 发起请求
        var result = await _httpPolly.PostAsync<DingTalkResultInfoDto>(HttpEnum.Common, url, sendMessage);
        // 包装返回信息
        if (result != null)
        {
            if (result.ErrCode == 0 || result.ErrMsg == "ok")
            {
                return BaseResultDto.Success("发送成功");
            }
            var resultInfos = typeof(DingTalkResultErrCodeEnum).GetEnumInfos();
            var info = resultInfos.Where(e => e.Value == result.ErrCode).FirstOrDefault();
            return BaseResultDto.BadRequest("发送失败，" + info?.Label);
        }
        return BaseResultDto.InternalServerError();
    }
}