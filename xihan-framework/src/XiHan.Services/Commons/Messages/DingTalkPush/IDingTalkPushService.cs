#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IDingTalkMessagePushService
// Guid:c9b248ce-f261-4abd-88ac-f4dfc35ada28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-06 下午 07:40:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses;
using XiHan.Subscriptions.WebHooks.DingTalk;

namespace XiHan.Services.Commons.Messages.DingTalkPush;

/// <summary>
/// IDingTalkMessagePushService
/// </summary>
public interface IDingTalkPushService
{
    /// <summary>
    /// 钉钉推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="atMobiles"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    Task<ApiResult> DingTalkToText(DingTalkText text, List<string>? atMobiles = null, bool isAtAll = false);

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    Task<ApiResult> DingTalkToLink(DingTalkLink link);

    /// <summary>
    /// 钉钉推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <param name="atMobiles"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    Task<ApiResult> DingTalkToMarkdown(DingTalkMarkdown markdown, List<string>? atMobiles = null, bool isAtAll = false);

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    Task<ApiResult> DingTalkToActionCard(DingTalkActionCard actionCard);

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    Task<ApiResult> DingTalkToFeedCard(DingTalkFeedCard feedCard);
}