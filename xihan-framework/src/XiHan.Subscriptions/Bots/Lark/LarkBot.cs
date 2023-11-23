#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:LarkBot
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

namespace XiHan.Subscriptions.Bots.Lark;

/// <summary>
/// 飞书自定义机器人消息推送
/// https://open.feishu.cn/document/client-docs/bot-v3/add-custom-bot
/// 自定义机器人的频率控制和普通应用不同，为 100 次/分钟，5 次/秒
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="larkConnection"></param>
public class LarkBot(LarkConnection larkConnection)
{
    private readonly string _url = larkConnection.WebHookUrl + "/" + larkConnection.AccessToken;
    private readonly string? _secret = larkConnection.Secret;
    private readonly string? _keyWord = larkConnection.KeyWord;

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public async Task<ApiResult> TextMessage(LarkText content)
    {
        var msgType = LarkMsgTypeEnum.Text.GetEnumDescriptionByKey();
        var result = await Send(new { msg_type = msgType, content });
        return result;
    }

    /// <summary>
    /// 发送富文本消息
    /// </summary>
    /// <param name="post"></param>
    public async Task<ApiResult> PostMessage(LarkPost post)
    {
        var msgType = LarkMsgTypeEnum.Post.GetEnumDescriptionByKey();
        var result = await Send(new { msg_type = msgType, post });
        return result;
    }

    /// <summary>
    /// 发送群卡片消息
    /// </summary>
    /// <param name="markdown">Markdown内容</param>
    /// <param name="atUserIds">被@的人群</param>
    /// <param name="isAtAll">是否@全员</param>
    public async Task<ApiResult> ShareChatMessage(LarkMarkdown markdown, List<string>? atUserIds = null,
        bool isAtAll = false)
    {
        var msgType = LarkMsgTypeEnum.ShareChat.GetEnumDescriptionByKey();
        // 指定目标人群
        LarkAt at = new()
        {
            AtUserIds = atUserIds
        };
        var result = await Send(new { msg_type = msgType, markdown, at });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="actionCard">ActionCard内容</param>
    public async Task<ApiResult> ImageMessage(LarkActionCard actionCard)
    {
        var msgType = LarkMsgTypeEnum.Image.GetEnumDescriptionByKey();
        var result = await Send(new { msg_type = msgType, actionCard });
        return result;
    }

    /// <summary>
    /// 发送消息卡片
    /// </summary>
    /// <param name="feedCard">FeedCard内容</param>
    public async Task<ApiResult> InterActiveMessage(LarkFeedCard feedCard)
    {
        var msgType = LarkMsgTypeEnum.InterActive.GetEnumDescriptionByKey();
        var result = await Send(new { msg_type = msgType, feedCard });
        return result;
    }

    /// <summary>
    /// 飞书执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ApiResult> Send(object objSend)
    {
        var url = _url;
        var sendMessage = objSend.SerializeTo();

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
        var _httpPollyService = App.GetRequiredService<IHttpPollyService>();
        var result = await _httpPollyService.PostAsync<LarkResultInfoDto>(HttpGroupEnum.Remote, url, sendMessage);
        // 包装返回信息
        if (result != null)
        {
            if (result.Code == 0 || result.Msg == "success") return ApiResult.Success("发送成功");

            var resultInfos = typeof(LarkResultErrCodeEnum).GetEnumInfos();
            var info = resultInfos.FirstOrDefault(e => e.Value == result.Code);
            return ApiResult.BadRequest("发送失败，" + info?.Label);
        }

        return ApiResult.InternalServerError();
    }
}