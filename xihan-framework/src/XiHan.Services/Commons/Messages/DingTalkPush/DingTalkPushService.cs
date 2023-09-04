#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DingTalkMessagePushService
// Guid:ac92fd5d-aa9d-4afd-9355-519e52eb5b09
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-06 下午 07:40:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Bases;
using XiHan.Subscriptions.Bots.DingTalk;
using XiHan.Utils.Exceptions;

namespace XiHan.Services.Commons.Messages.DingTalkPush;

/// <summary>
/// DingTalkMessagePush
/// </summary>
[AppService(ServiceType = typeof(IDingTalkPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class DingTalkPushService : BaseService<SysBot>, IDingTalkPushService
{
    private readonly DingTalkBot _dingTalkBot;

    /// <summary>
    /// 构造函数
    /// </summary>
    public DingTalkPushService()
    {
        var dingTalkConnection = GetDingTalkConn().Result ?? throw new CustomException("未添加钉钉推送配置或配置不可用！");
        _dingTalkBot = new DingTalkBot(dingTalkConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<DingTalkConnection> GetDingTalkConn()
    {
        var sysCustomBot = await GetFirstAsync(e => e.IsEnabled && e.BotType == BotTypeEnum.DingTalk);
        var config = new TypeAdapterConfig().ForType<SysBot, DingTalkConnection>()
            .Map(dest => dest.AccessToken, src => src.AccessTokenOrKey)
            .Config;
        var dingTalkConnection = sysCustomBot.Adapt<DingTalkConnection>(config);
        return dingTalkConnection;
    }

    #region DingTalk

    /// <summary>
    /// 钉钉推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="at"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToText(DingTalkText text, DingTalkAt? at)
    {
        return await _dingTalkBot.TextMessage(text, at);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToLink(DingTalkLink link)
    {
        return await _dingTalkBot.LinkMessage(link);
    }

    /// <summary>
    /// 钉钉推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <param name="at"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToMarkdown(DingTalkMarkdown markdown, DingTalkAt? at)
    {
        return await _dingTalkBot.MarkdownMessage(markdown, at);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToActionCard(DingTalkActionCard actionCard)
    {
        return await _dingTalkBot.ActionCardMessage(actionCard);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToFeedCard(DingTalkFeedCard feedCard)
    {
        return await _dingTalkBot.FeedCardMessage(feedCard);
    }

    #endregion DingTalk
}