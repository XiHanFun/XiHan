#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IWeComMessagePushService
// Guid:54d10161-b312-4b9f-bde7-603f416f2066
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-08 上午 02:43:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Results;
using XiHan.Subscriptions.WebHooks.WeCom;

namespace XiHan.Services.Commons.Messages.WeComPush;

/// <summary>
/// IWeComMessagePushService
/// </summary>
public interface IWeComPushService
{
    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<ApiResult> WeComToText(WeComText text);

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    Task<ApiResult> WeComToMarkdown(WeComMarkdown markdown);

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    Task<ApiResult> WeComToImage(WeComImage image);

    /// <summary>
    /// 微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    Task<ApiResult> WeComToNews(WeComNews news);

    /// <summary>
    /// 微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    Task<ApiResult> WeComToFile(WeComFile file);

    /// <summary>
    /// 微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    Task<ApiResult> WeComToTextNotice(WeComTemplateCardTextNotice templateCard);

    /// <summary>
    /// 微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    Task<ApiResult> WeComToNewsNotice(WeComTemplateCardNewsNotice templateCard);

    /// <summary>
    /// 微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <returns></returns>
    Task<ApiResult> WeComToUploadFile(FileStream fileStream);
}