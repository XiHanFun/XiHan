﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeComMessagePushService
// Guid:a273c787-f81d-4c4b-875e-c20d8a04ab45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:44:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Bases;
using XiHan.Subscriptions.Robots.WeCom;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Messages.WeComPush;

/// <summary>
/// WeComMessagePushService
/// </summary>
[AppService(ServiceType = typeof(IWeComPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class WeComPushService : BaseService<SysCustomRobot>, IWeComPushService
{
    private readonly WeComCustomRobot _weComRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    public WeComPushService(IHttpPollyHelper httpPolly)
    {
        WeComConnection weComConnection = GetWeComConn().Result;
        _weComRobot = new WeComCustomRobot(httpPolly, weComConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<WeComConnection> GetWeComConn()
    {
        var sysCustomRobot = await GetFirstAsync(e => e.IsEnabled && e.CustomRobotType == CustomRobotTypeEnum.WeCom.GetEnumValueByKey());
        var config = new TypeAdapterConfig()
            .ForType<SysCustomRobot, WeComConnection>()
            .Map(dest => dest.Key, src => src.AccessTokenOrKey)
            .Config;
        WeComConnection weComConnection = sysCustomRobot.Adapt<WeComConnection>(config);
        return weComConnection;
    }

    #region WeCom

    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToText(WeComText text)
    {
        return await _weComRobot.TextMessage(text);
    }

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToMarkdown(WeComMarkdown markdown)
    {
        return await _weComRobot.MarkdownMessage(markdown);
    }

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToImage(WeComImage image)
    {
        return await _weComRobot.ImageMessage(image);
    }

    /// <summary>
    /// 微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToNews(WeComNews news)
    {
        return await _weComRobot.NewsMessage(news);
    }

    /// <summary>
    /// 微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToFile(WeComFile file)
    {
        return await _weComRobot.FileMessage(file);
    }

    /// <summary>
    /// 微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToTextNotice(WeComTemplateCardTextNotice templateCard)
    {
        return await _weComRobot.TextNoticeMessage(templateCard);
    }

    /// <summary>
    /// 微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToNewsNotice(WeComTemplateCardNewsNotice templateCard)
    {
        return await _weComRobot.NewsNoticeMessage(templateCard);
    }

    /// <summary>
    /// 微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToUploadkFile(FileStream fileStream)
    {
        return await _weComRobot.UploadkFile(fileStream);
    }

    #endregion
}