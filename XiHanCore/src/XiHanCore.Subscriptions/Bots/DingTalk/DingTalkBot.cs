#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DingTalkBot
// Guid:b9ebb234-1ebf-4b97-b308-0c525d2cd190
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-08 上午 12:48:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Security.Cryptography;
using System.Text;
using XiHanCore.Infrastructures.Apps;
using XiHanCore.Infrastructures.Requests.Https;
using XiHanCore.Infrastructures.Responses;
using XiHanCore.Utils.Extensions;
using XiHanCore.Utils.Serializes;

namespace XiHanCore.Subscriptions.Bots.DingTalk;

/// <summary>
/// 钉钉自定义机器人消息推送
/// https://open.dingtalk.com/document/orgapp/custom-robot-access
/// 每个机器人每分钟最多发送20条消息到群里，如果超过20条，会限流10分钟
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="dingTalkConnection"></param>
public class DingTalkBot(DingTalkConnection dingTalkConnection)
{
    private readonly string _url = dingTalkConnection.WebHookUrl + "?access_token=" + dingTalkConnection.AccessToken;
    private readonly string? _secret = dingTalkConnection.Secret;
    private readonly string? _keyWord = dingTalkConnection.KeyWord == null ? null : dingTalkConnection.KeyWord + "\n";

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="dingTalkText">内容</param>
    /// <param name="dingTalkAt">指定目标人群</param>
    /// <returns></returns>
    public async Task<ApiResult> TextMessage(DingTalkText dingTalkText, DingTalkAt? dingTalkAt)
    {
        var msgType = DingTalkMsgTypeEnum.Text.GetEnumDescriptionByKey();
        dingTalkText.Content = _keyWord + dingTalkText.Content;
        var result = await Send(new { msgtype = msgType, text = dingTalkText, at = dingTalkAt });
        return result;
    }

    /// <summary>
    /// 发送链接消息
    /// </summary>
    /// <param name="dingTalkLink"></param>
    public async Task<ApiResult> LinkMessage(DingTalkLink dingTalkLink)
    {
        var msgType = DingTalkMsgTypeEnum.Link.GetEnumDescriptionByKey();
        dingTalkLink.Title = _keyWord + dingTalkLink.Title;
        var result = await Send(new { msgtype = msgType, link = dingTalkLink });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="dingTalkMarkdown">Markdown内容</param>
    /// <param name="dingTalkAt">指定目标人群</param>
    public async Task<ApiResult> MarkdownMessage(DingTalkMarkdown dingTalkMarkdown, DingTalkAt? dingTalkAt)
    {
        var msgType = DingTalkMsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        dingTalkMarkdown.Title = _keyWord + dingTalkMarkdown.Title;
        var result = await Send(new { msgtype = msgType, markdown = dingTalkMarkdown, at = dingTalkAt });
        return result;
    }

    /// <summary>
    /// 发送任务卡片消息
    /// 按钮方案二选一，设置单个按钮方案后多个按钮方案会无效
    /// </summary>
    /// <param name="dingTalkActionCard">ActionCard内容</param>
    public async Task<ApiResult> ActionCardMessage(DingTalkActionCard dingTalkActionCard)
    {
        var msgType = DingTalkMsgTypeEnum.ActionCard.GetEnumDescriptionByKey();
        dingTalkActionCard.Title = _keyWord + dingTalkActionCard.Title;
        dingTalkActionCard.Btns?.ForEach(btn => btn.Title = _keyWord + btn.Title);
        var result = await Send(new { msgtype = msgType, actionCard = dingTalkActionCard });
        return result;
    }

    /// <summary>
    /// 发送卡片菜单消息
    /// </summary>
    /// <param name="dingTalkFeedCard">FeedCard内容</param>
    public async Task<ApiResult> FeedCardMessage(DingTalkFeedCard dingTalkFeedCard)
    {
        var msgType = DingTalkMsgTypeEnum.FeedCard.GetEnumDescriptionByKey();
        dingTalkFeedCard.Links?.ForEach(link => link.Title = _keyWord + link.Title);
        var result = await Send(new { msgtype = msgType, feedCard = dingTalkFeedCard });
        return result;
    }

    /// <summary>
    /// 钉钉执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ApiResult> Send(object objSend)
    {
        var url = _url;

        // 安全设置加签，需要使用 UTF-8 字符集
        if (!string.IsNullOrEmpty(_secret))
        {
            // 把 【timestamp + "\n" + 密钥】 当做签名字符串
            var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var sign = timeStamp + "\n" + _secret;
            UTF8Encoding encoding = new();
            var keyByte = encoding.GetBytes(_secret);
            var messageBytes = encoding.GetBytes(sign);
            // 使用 HmacSHA256 算法计算签名
            using (HMACSHA256 hash256 = new(keyByte))
            {
                var hashMessage = hash256.ComputeHash(messageBytes);
                // 然后进行 Base64 encode，最后再把签名参数再进行 urlEncode
                sign = Convert.ToBase64String(hashMessage).UrlEncode();
            }

            // 得到最终的签名
            url += $"&timestamp={timeStamp}&sign={sign}";
        }

        // 发起请求
        var sendMessage = objSend.SerializeTo();
        var httpPollyService = App.GetRequiredService<IHttpPollyService>();
        var result = await httpPollyService.PostAsync<DingTalkResultInfoDto>(HttpGroupEnum.Remote, url, sendMessage);
        // 包装返回信息
        if (result == null) return ApiResult.InternalServerError();

        if (result.ErrCode == 0 || result.ErrMsg == "ok") return ApiResult.Success("发送成功；");

        var resultInfos = typeof(DingTalkResultErrCodeEnum).GetEnumInfos();
        var info = resultInfos.FirstOrDefault(e => e.Value == result.ErrCode);
        return ApiResult.BadRequest("发送失败；" + info?.Label);
    }
}