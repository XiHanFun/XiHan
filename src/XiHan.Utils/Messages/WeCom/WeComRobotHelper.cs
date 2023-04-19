#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeComRobotHelper
// Guid:1f9edb73-56c9-4849-88a8-c57488b3582d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:32:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Enums;
using XiHan.Utils.Https;
using XiHan.Utils.Serializes;

namespace XiHan.Utils.Messages.WeCom;

/// <summary>
/// 企业微信机器人消息推送
/// </summary>
public class WeComRobotHelper
{
    /// <summary>
    /// 请求接口
    /// </summary>
    private readonly IHttpPollyHelper HttpPollyHelper;

    /// <summary>
    /// 正式访问地址
    /// </summary>
    private readonly string MessageUrl;

    /// <summary>
    /// 正式文件上传地址
    /// </summary>
    private readonly string FileUrl;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPollyHelper"></param>
    /// <param name="weChatConnection"></param>
    public WeComRobotHelper(IHttpPollyHelper httpPollyHelper, WeComConnection weChatConnection)
    {
        HttpPollyHelper = httpPollyHelper;
        MessageUrl = weChatConnection.WebHookUrl + "?key=" + weChatConnection.Key;
        FileUrl = weChatConnection.UploadkUrl + "?key=" + weChatConnection.Key + "&type=file";
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> TextMessage(Text text)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.Text.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, text });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="markdown">文档</param>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> MarkdownMessage(Markdown markdown)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, markdown });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="image">图片</param>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> ImageMessage(Image image)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.Image.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, image });
        return result;
    }

    /// <summary>
    /// 发送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> NewsMessage(News news)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.News.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, news });
        return result;
    }

    /// <summary>
    /// 发送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> FileMessage(File file)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.File.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, file });
        return result;
    }

    /// <summary>
    /// 发送文本通知模版卡片消息
    /// </summary>
    /// <param name="templateCard">模版卡片</param>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> TextNoticeMessage(TemplateCardTextNotice templateCard)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        templateCard.CardType = TemplateCardType.TextNotice.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, template_card = templateCard });
        return result;
    }

    /// <summary>
    /// 发送图文展示模版卡片消息
    /// </summary>
    /// <param name="templateCard">模版卡片</param>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> NewsNoticeMessage(TemplateCardNewsNotice templateCard)
    {
        // 消息类型
        var msgtype = MsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        templateCard.CardType = TemplateCardType.NewsNotice.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, template_card = templateCard });
        return result;
    }

    /// <summary>
    /// 微信执行上传文件
    /// 素材上传得到media_id，该media_id仅三天内有效，且只能对应上传文件的机器人可以使用
    /// 文件大小在5B~20M之间
    /// </summary>
    /// <returns></returns>
    public async Task<WeComResultInfoDto?> UploadkFile(FileStream fileStream)
    {
        Dictionary<string, string> headers = new()
        {
            { "filename",fileStream.Name },
            { "filelength",fileStream.Length.ToString() },
        };
        // 发起请求，上传地址，调用接口凭证, 机器人webhookurl中的key参数
        var result = await HttpPollyHelper.PostAsync<WeComResultInfoDto>(HttpEnum.Common, FileUrl, fileStream, headers);
        return result;
    }

    /// <summary>
    /// 微信执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<WeComResultInfoDto?> SendMessage(object objSend)
    {
        // 发送对象
        var sendMessage = objSend.SerializeToJson();
        // 发起请求，发送消息地址
        var result = await HttpPollyHelper.PostAsync<WeComResultInfoDto>(HttpEnum.Common, MessageUrl, sendMessage);
        return result;
    }
}