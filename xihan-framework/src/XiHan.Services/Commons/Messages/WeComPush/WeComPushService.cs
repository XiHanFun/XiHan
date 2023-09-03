#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WeComMessagePushService
// Guid:a273c787-f81d-4c4b-875e-c20d8a04ab45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-08 上午 02:44:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Bases;
using XiHan.Subscriptions.Bots.WeCom;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Commons.Messages.WeComPush;

/// <summary>
/// WeComMessagePushService
/// </summary>
[AppService(ServiceType = typeof(IWeComPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class WeComPushService : BaseService<SysBot>, IWeComPushService
{
    private readonly WeComBot _weComBot;

    /// <summary>
    /// 构造函数
    /// </summary>
    public WeComPushService()
    {
        WeComConnection weComConnection = GetWeComConn().Result ?? throw new CustomException("未添加企业微信推送配置或配置不可用！");
        _weComBot = new WeComBot(weComConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<WeComConnection> GetWeComConn()
    {
        SysBot sysCustomBot = await GetFirstAsync(e => e.IsEnabled && e.BotType == BotTypeEnum.WeCom);
        TypeAdapterConfig config = new TypeAdapterConfig().ForType<SysBot, WeComConnection>()
            .Map(dest => dest.Key, src => src.AccessTokenOrKey)
            .Config;
        WeComConnection weComConnection = sysCustomBot.Adapt<WeComConnection>(config);
        return weComConnection;
    }

    #region WeCom

    /// <summary>
    /// 企业微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToText(WeComText text)
    {
        return await _weComBot.TextMessage(text);
    }

    /// <summary>
    /// 企业微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToMarkdown(WeComMarkdown markdown)
    {
        return await _weComBot.MarkdownMessage(markdown);
    }

    /// <summary>
    /// 企业微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToImage(WeComImage image)
    {
        return await _weComBot.ImageMessage(image);
    }

    /// <summary>
    /// 企业微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToNews(WeComNews news)
    {
        return await _weComBot.NewsMessage(news);
    }

    /// <summary>
    /// 企业微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToFile(WeComFile file)
    {
        return await _weComBot.FileMessage(file);
    }

    /// <summary>
    /// 企业微信推送语音消息
    /// </summary>
    /// <param name="voice">语音</param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToVoice(WeComVoice voice)
    {
        return await _weComBot.VoiceMessage(voice);
    }

    /// <summary>
    /// 企业微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToTextNotice(WeComTemplateCardTextNotice templateCard)
    {
        return await _weComBot.TextNoticeMessage(templateCard);
    }

    /// <summary>
    /// 企业微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToNewsNotice(WeComTemplateCardNewsNotice templateCard)
    {
        return await _weComBot.NewsNoticeMessage(templateCard);
    }

    /// <summary>
    /// 企业微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <param name="uploadType">文件类型</param>
    /// <returns></returns>
    public async Task<ApiResult> WeComToUploadFile(FileStream fileStream, WeComUploadType uploadType)
    {
        return await _weComBot.UploadFile(fileStream, uploadType);
    }

    #endregion
}