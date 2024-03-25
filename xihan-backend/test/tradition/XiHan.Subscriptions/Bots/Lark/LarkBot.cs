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
    private readonly string? _keyWord = larkConnection.KeyWord == null ? null : larkConnection.KeyWord + "\n";

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="larkText">内容</param>
    /// <returns></returns>
    public async Task<ApiResult> TextMessage(LarkText larkText)
    {
        var msgType = LarkMsgTypeEnum.Text.GetEnumDescriptionByKey();
        larkText.Text = _keyWord + larkText.Text;
        var result = await Send(new { msg_type = msgType, content = larkText });
        return result;
    }

    /// <summary>
    /// 发送富文本消息
    /// </summary>
    /// <param name="larkPost">Post内容</param>
    public async Task<ApiResult> PostMessage(LarkPost larkPost)
    {
        var msgType = LarkMsgTypeEnum.Post.GetEnumDescriptionByKey();
        var objTList = new List<List<object>>();

        // 拆解内容
        var contentTList = larkPost.Content;
        foreach (var contentList in contentTList)
        {
            var list = new List<object>();
            foreach (var itemT in contentList)
            {
                switch (itemT)
                {
                    case TagText text:
                        list.Add(text);
                        break;

                    case TagA a:
                        list.Add(a);
                        break;

                    case TagAt at:
                        list.Add(at);
                        break;

                    case TagImg image:
                        list.Add(image);
                        break;

                    default:
                        Console.WriteLine("Unknown type");
                        break;
                }
            }
            objTList.Add(list);
        }
        larkPost.Title = _keyWord + larkPost.Title;
        var zhCn = new { title = larkPost.Title, content = objTList };
        // 设置语言
        var post = new { zh_cn = zhCn };
        var postContent = new { post };
        var result = await Send(new { msg_type = msgType, content = postContent });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="larkImage">Image内容</param>
    public async Task<ApiResult> ImageMessage(LarkImage larkImage)
    {
        var msgType = LarkMsgTypeEnum.Image.GetEnumDescriptionByKey();
        var result = await Send(new { msg_type = msgType, content = larkImage });
        return result;
    }

    /// <summary>
    /// 发送消息卡片
    /// </summary>
    /// <param name="larkInterActive">InterActive内容</param>
    public async Task<ApiResult> InterActiveMessage(LarkInterActive larkInterActive)
    {
        var msgType = LarkMsgTypeEnum.InterActive.GetEnumDescriptionByKey();
        larkInterActive.Header.Title.Content = _keyWord + larkInterActive.Header.Title.Content;
        var result = await Send(new { msg_type = msgType, card = larkInterActive });
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

            // 得到最终的请求体
            objSend.SetPropertyValue("timeStamp", timeStamp);
            objSend.SetPropertyValue("sign", sign);
        }

        // 发起请求
        var sendMessage = objSend.SerializeTo();
        var httpPollyService = App.GetRequiredService<IHttpPollyService>();
        var result = await httpPollyService.PostAsync<LarkResultInfoDto>(HttpGroupEnum.Remote, url, sendMessage);
        // 包装返回信息
        if (result == null) return ApiResult.InternalServerError();
        if (result.Code == 0 || result.Msg == "success") return ApiResult.Success("发送成功；");

        var resultInfos = typeof(LarkResultErrCodeEnum).GetEnumInfos();
        var info = resultInfos.FirstOrDefault(e => e.Value == result.Code);
        return ApiResult.BadRequest("发送失败；" + info?.Label);
    }
}