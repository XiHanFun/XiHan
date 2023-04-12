#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IWeChatMessagePushService
// Guid:54d10161-b312-4b9f-bde7-603f416f2066
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:43:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Contexts.Results;
using XiHan.Utils.Messages.WeChat;
using File = XiHan.Utils.Messages.WeChat.File;

namespace XiHan.Services.Utils.Messages;

/// <summary>
/// IWeChatMessagePushService
/// </summary>
public interface IWeChatPushService
{
    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToText(Text text);

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToMarkdown(Markdown markdown);

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToImage(Image image);

    /// <summary>
    /// 微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToNews(News news);

    /// <summary>
    /// 微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToFile(File file);

    /// <summary>
    /// 微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToTextNotice(TemplateCardTextNotice templateCard);

    /// <summary>
    /// 微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToNewsNotice(TemplateCardNewsNotice templateCard);

    /// <summary>
    /// 微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToUploadkFile(FileStream fileStream);
}