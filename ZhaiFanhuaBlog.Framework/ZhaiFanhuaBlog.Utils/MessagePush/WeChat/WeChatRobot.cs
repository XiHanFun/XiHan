// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeChatRobot
// Guid:1f9edb73-56c9-4849-88a8-c57488b3582d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:32:32
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Utils.Http;
using ZhaiFanhuaBlog.Utils.Serialize;

namespace ZhaiFanhuaBlog.Utils.MessagePush.WeChat;

/// <summary>
/// 微信机器人消息推送
/// </summary>
public class WeChatRobot
{
    /// <summary>
    /// 请求接口
    /// </summary>
    private readonly IHttpHelper _IHttpHelper;

    /// <summary>
    /// Webhook 地址
    /// </summary>
    private readonly string _WebHookUrl = string.Empty;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    /// <param name="webHookUrl"></param>
    public WeChatRobot(IHttpHelper iHttpHelper, string webHookUrl)
    {
        _IHttpHelper = iHttpHelper;
        _WebHookUrl = webHookUrl;
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <returns></returns>
    public async Task<ResultInfo?> TextMessage(Text text)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.text.ToString();
        // 发送
        var result = await Send(new { msgtype, text });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="markdown">文档</param>
    /// <returns></returns>
    public async Task<ResultInfo?> MarkdownMessage(Markdown markdown)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.markdown.ToString();
        // 发送
        var result = await Send(new { msgtype, markdown });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="image">图片</param>
    /// <returns></returns>
    public async Task<ResultInfo?> ImageMessage(Image image)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.image.ToString();
        // 发送
        var result = await Send(new { msgtype, image });
        return result;
    }

    /// <summary>
    /// 微信执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ResultInfo?> Send(object objSend)
    {
        var url = _WebHookUrl;
        var sendMessage = objSend.SerializeToJson();
        // 发起请求
        ResultInfo? result = await _IHttpHelper.PostAsync<ResultInfo>(HttpEnum.LocalHost, url, sendMessage, null);
        return result;
    }
}