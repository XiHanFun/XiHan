﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IMessagePush
// Guid:c9b248ce-f261-4abd-88ac-f4dfc35ada28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 下午 07:40:59
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Utils.DingTalk;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.Services.Utils;

/// <summary>
/// IMessagePush
/// </summary>
public interface IMessagePush : IScopeDependency
{
    /// <summary>
    /// 钉钉推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="atMobiles"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    Task<BaseResultDto> DingTalkToText(Text text, List<string>? atMobiles = null, bool isAtAll = false);

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    Task<BaseResultDto> DingTalkToLink(Link link);

    /// <summary>
    /// 钉钉推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <param name="atMobiles"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    Task<BaseResultDto> DingTalkToMarkdown(Markdown markdown, List<string>? atMobiles = null, bool isAtAll = false);

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    Task<BaseResultDto> DingTalkToActionCard(ActionCard actionCard);

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    Task<BaseResultDto> DingTalkToFeedCard(FeedCard feedCard);
}