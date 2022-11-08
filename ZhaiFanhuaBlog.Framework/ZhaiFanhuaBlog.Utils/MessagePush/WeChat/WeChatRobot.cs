// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeChatRobot
// Guid:1f9edb73-56c9-4849-88a8-c57488b3582d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:32:32
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// 网络挂钩地址
    /// </summary>
    private readonly string _WebHookUrl = string.Empty;

    /// <summary>
    /// 文件上传地址
    /// </summary>
    private readonly string _UploadkUrl = string.Empty;

    /// <summary>
    /// 访问令牌
    /// </summary>
    private readonly string _Key = string.Empty;

    /// <summary>
    /// 正式访问地址
    /// </summary>
    private readonly string _MessageUrl = string.Empty;

    /// <summary>
    /// 正式文件上传地址
    /// </summary>
    private readonly string _FileUrl = string.Empty;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    /// <param name="webHookUrl"></param>
    /// <param name="uploadkUrl"></param>
    /// <param name="key"></param>
    public WeChatRobot(IHttpHelper iHttpHelper, string webHookUrl, string uploadkUrl, string key)
    {
        _IHttpHelper = iHttpHelper;
        _WebHookUrl = webHookUrl;
        _UploadkUrl = uploadkUrl;
        _Key = key;
        _MessageUrl = _WebHookUrl + "?key=" + _Key;
        _FileUrl = _UploadkUrl + "?key=" + _Key + "&type=";
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
        var result = await SendMessage(new { msgtype, text });
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
        var result = await SendMessage(new { msgtype, markdown });
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
        var result = await SendMessage(new { msgtype, image });
        return result;
    }

    /// <summary>
    /// 发送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<ResultInfo?> NewsMessage(News news)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.news.ToString();
        // 发送
        var result = await SendMessage(new { msgtype, news });
        return result;
    }

    /// <summary>
    /// 发送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<ResultInfo?> FileMessage(File file)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.file.ToString();
        // 发送
        var result = await SendMessage(new { msgtype, file });
        return result;
    }

    /// <summary>
    /// 发送文本通知模版卡片消息
    /// </summary>
    /// <param name="template_card">模版卡片</param>
    /// <returns></returns>
    public async Task<ResultInfo?> TextNoticeMessage(TemplateCardTextNotice template_card)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.template_card.ToString();
        // 发送
        var result = await SendMessage(new { msgtype, template_card });
        return result;
    }

    /// <summary>
    /// 发送图文展示模版卡片消息
    /// </summary>
    /// <param name="template_card">模版卡片</param>
    /// <returns></returns>
    public async Task<ResultInfo?> NewsNoticeMessage(TemplateCardNewsNotice template_card)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.template_card.ToString();
        // 发送
        var result = await SendMessage(new { msgtype, template_card });
        return result;
    }

    /// <summary>
    /// 微信执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ResultInfo?> SendMessage(object objSend)
    {
        // 发送消息地址
        var url = _MessageUrl;
        // 发送对象
        var sendMessage = objSend.SerializeToJson();
        // 发起请求
        ResultInfo? result = await _IHttpHelper.PostAsync<ResultInfo>(HttpEnum.LocalHost, url, sendMessage, null);
        return result;
    }

    /// <summary>
    /// 微信执行上传文件
    /// 素材上传得到media_id，该media_id仅三天内有效，且只能对应上传文件的机器人可以使用
    /// 文件大小在5B~20M之间
    /// </summary>
    /// <returns></returns>
    public async Task<ResultInfo?> UploadkFile(FileStream fileStream)
    {
        // 文件类型，固定传file
        string type = "file";
        // 上传地址，调用接口凭证, 机器人webhookurl中的key参数
        var url = _FileUrl + type;
        Dictionary<string, string> headers = new()
        {
            { "filename",fileStream.Name },
            { "filelength",fileStream.Length.ToString() },
        };
        // 发起请求
        ResultInfo? result = await _IHttpHelper.PostAsync<ResultInfo>(HttpEnum.LocalHost, url, fileStream, headers);
        return result;
    }
}