#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:LarkCustomRobot
// Guid:b9ebb234-1ebf-4b97-b308-0c525d2cd190
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 12:48:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.Security.Cryptography;
using System.Text;
using XiHan.Infrastructures.Requests;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.Subscriptions.Robots.Lark;

/// <summary>
/// 飞书自定义机器人消息推送
/// https://open.feishu.cn/document/ukTMukTMukTM/ucTM5YjL3ETO24yNxkjN
/// </summary>
public class LarkCustomRobot
{
    private readonly IHttpPollyHelper _httpPolly;
    private readonly string _url;
    private readonly string _secret;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    /// <param name="LarkConnection"></param>
    public LarkCustomRobot(IHttpPollyHelper httpPolly, LarkConnection LarkConnection)
    {
        _httpPolly = httpPolly;
        _url = LarkConnection.WebHookUrl + "/" + LarkConnection.AccessToken;
        _secret = LarkConnection.Secret;
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public async Task<ResultDto> TextMessage(LarkText content)
    {
        // 消息类型
        var msg_type = LarkMsgTypeEnum.Text.GetEnumDescriptionByKey();
        // 发送
        var result = await Send(new { msg_type, content });
        return result;
    }

    /// <summary>
    /// 发送富文本消息
    /// </summary>
    /// <param name="post"></param>
    public async Task<ResultDto> PostMessage(LarkPost post)
    {
        // 消息类型
        var msg_type = LarkMsgTypeEnum.Post.GetEnumDescriptionByKey();
        // 发送
        var result = await Send(new { msg_type, post });
        return result;
    }

    /// <summary>
    /// 发送群卡片消息
    /// </summary>
    /// <param name="markdown">Markdown内容</param>
    /// <param name="atMobiles">被@的人群</param>
    /// <param name="isAtAll">是否@全员</param>
    public async Task<ResultDto> ShareChatMessage(LarkMarkdown markdown, List<string>? atMobiles = null, bool isAtAll = false)
    {
        // 消息类型
        var msg_type = LarkMsgTypeEnum.ShareChat.GetEnumDescriptionByKey();
        // 指定目标人群
        var at = new LarkAt
        {
            AtMobiles = atMobiles,
            IsAtAll = isAtAll
        };
        // 发送
        var result = await Send(new { msg_type, markdown, at });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="actionCard">ActionCard内容</param>
    public async Task<ResultDto> ImageMessage(LarkActionCard actionCard)
    {
        // 消息类型
        var msg_type = LarkMsgTypeEnum.Image.GetEnumDescriptionByKey();
        // 发送
        var result = await Send(new { msg_type, actionCard });
        return result;
    }

    /// <summary>
    /// 发送消息卡片
    /// </summary>
    /// <param name="feedCard">FeedCard内容</param>
    public async Task<ResultDto> InterActiveMessage(LarkFeedCard feedCard)
    {
        // 消息类型
        var msg_type = LarkMsgTypeEnum.InterActive.GetEnumDescriptionByKey();
        // 发送
        var result = await Send(new { msg_type, feedCard });
        return result;
    }

    /// <summary>
    /// 飞书执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ResultDto> Send(object objSend)
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
        var result = await _httpPolly.PostAsync<LarkResultInfoDto>(HttpGroupEnum.Common, url, sendMessage);
        // 包装返回信息
        if (result != null)
        {
            if (result.Code == 0 || result.Msg == "success")
            {
                return ResultDto.Success("发送成功");
            }
            var resultInfos = typeof(LarkResultErrCodeEnum).GetEnumInfos();
            var info = resultInfos.Where(e => e.Value == result.Code).FirstOrDefault();
            return ResultDto.BadRequest("发送失败，" + info?.Label);
        }
        return ResultDto.InternalServerError();
    }
}