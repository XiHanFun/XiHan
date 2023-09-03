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
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.Subscriptions.Bots.DingTalk;

/// <summary>
/// 钉钉自定义机器人消息推送
/// https://open.dingtalk.com/document/orgapp/custom-robot-access
/// 每个机器人每分钟最多发送20条消息到群里，如果超过20条，会限流10分钟
/// </summary>
public class DingTalkBot
{
    private readonly string _url;
    private readonly string? _secret;
    private readonly string? _keyWord;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dingTalkConnection"></param>
    public DingTalkBot(DingTalkConnection dingTalkConnection)
    {
        _url = dingTalkConnection.WebHookUrl + "?access_token=" + dingTalkConnection.AccessToken;
        _secret = dingTalkConnection.Secret;
        _keyWord = dingTalkConnection.KeyWord;
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <param name="at">指定目标人群</param>
    /// <returns></returns>
    public async Task<ApiResult> TextMessage(DingTalkText text, DingTalkAt? at)
    {
        string msgType = DingTalkMsgTypeEnum.Text.GetEnumDescriptionByKey();
        text.Content = _keyWord + text.Content;
        ApiResult result = await Send(new { msgType, text, at });
        return result;
    }

    /// <summary>
    /// 发送链接消息
    /// </summary>
    /// <param name="link"></param>
    public async Task<ApiResult> LinkMessage(DingTalkLink link)
    {
        string msgType = DingTalkMsgTypeEnum.Link.GetEnumDescriptionByKey();
        link.Title = _keyWord + link.Title;
        ApiResult result = await Send(new { msgType, link });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="markdown">Markdown内容</param>
    /// <param name="at">指定目标人群</param>
    public async Task<ApiResult> MarkdownMessage(DingTalkMarkdown markdown, DingTalkAt? at)
    {
        string msgType = DingTalkMsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        markdown.Title = _keyWord + markdown.Title;
        ApiResult result = await Send(new { msgType, markdown, at });
        return result;
    }

    /// <summary>
    /// 发送任务卡片消息
    /// 按钮方案二选一，设置单个按钮方案后多个按钮方案会无效
    /// </summary>
    /// <param name="actionCard">ActionCard内容</param>
    public async Task<ApiResult> ActionCardMessage(DingTalkActionCard actionCard)
    {
        string msgType = DingTalkMsgTypeEnum.ActionCard.GetEnumDescriptionByKey();
        actionCard.Title = _keyWord + actionCard.Title;
        actionCard.Btns?.ForEach(btn => btn.Title = _keyWord + btn.Title);
        ApiResult result = await Send(new { msgType, actionCard });
        return result;
    }

    /// <summary>
    /// 发送卡片菜单消息
    /// </summary>
    /// <param name="feedCard">FeedCard内容</param>
    public async Task<ApiResult> FeedCardMessage(DingTalkFeedCard feedCard)
    {
        string msgType = DingTalkMsgTypeEnum.FeedCard.GetEnumDescriptionByKey();
        feedCard.Links?.ForEach(link => link.Title = _keyWord + link.Title);
        ApiResult result = await Send(new { msgType, feedCard });
        return result;
    }

    /// <summary>
    /// 钉钉执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ApiResult> Send(object objSend)
    {
        string url = _url;
        string sendMessage = objSend.SerializeToJson();

        // 安全设置加签，需要使用 UTF-8 字符集
        if (!string.IsNullOrEmpty(_secret))
        {
            // 把 【timestamp + "\n" + 密钥】 当做签名字符串
            long timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            string sign = timeStamp + "\n" + _secret;
            UTF8Encoding encoding = new();
            byte[] keyByte = encoding.GetBytes(_secret);
            byte[] messageBytes = encoding.GetBytes(sign);
            // 使用 HmacSHA256 算法计算签名
            using (HMACSHA256 hash256 = new(keyByte))
            {
                byte[] hashMessage = hash256.ComputeHash(messageBytes);
                // 然后进行 Base64 encode，最后再把签名参数再进行 urlEncode
                sign = Convert.ToBase64String(hashMessage).UrlEncode();
            }

            // 得到最终的签名
            url += $"&timestamp={timeStamp}&sign={sign}";
        }

        // 发起请求
        var _httpPollyService = App.GetRequiredService<IHttpPollyService>();
        DingTalkResultInfoDto? result = await _httpPollyService.PostAsync<DingTalkResultInfoDto>(HttpGroupEnum.Remote, url, sendMessage);
        // 包装返回信息
        if (result == null)
        {
            return ApiResult.InternalServerError();
        }

        if (result.ErrCode == 0 || result.ErrMsg == "ok")
        {
            return ApiResult.Success("发送成功");
        }

        IEnumerable<EnumDescDto> resultInfos = typeof(DingTalkResultErrCodeEnum).GetEnumInfos();
        EnumDescDto? info = resultInfos.FirstOrDefault(e => e.Value == result.ErrCode);
        return ApiResult.BadRequest("发送失败，" + info?.Label);
    }
}