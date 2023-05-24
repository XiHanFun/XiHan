#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DingTalkMessagePushService
// Guid:ac92fd5d-aa9d-4afd-9355-519e52eb5b09
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 下午 07:40:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Bases;
using XiHan.Subscriptions.Robots.DingTalk;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Messages.DingTalkPush;

/// <summary>
/// DingTalkMessagePush
/// </summary>
[AppService(ServiceType = typeof(IDingTalkPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class DingTalkPushService : BaseService<SysCustomRobot>, IDingTalkPushService
{
    private readonly DingTalkCustomRobot _dingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    public DingTalkPushService(IHttpPollyHelper httpPolly)
    {
        DingTalkConnection dingTalkConnection = GetDingTalkConn().Result;
        _dingTalkRobot = new DingTalkCustomRobot(httpPolly, dingTalkConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<DingTalkConnection> GetDingTalkConn()
    {
        var sysCustomRobot = await GetFirstAsync(e => e.IsEnabled && e.CustomRobotType == CustomRobotTypeEnum.DingTalk.GetEnumValueByKey());
        var config = new TypeAdapterConfig()
            .ForType<SysCustomRobot, DingTalkConnection>()
            .Map(dest => dest.AccessToken, src => src.AccessTokenOrKey)
            .Config;
        DingTalkConnection dingTalkConnection = sysCustomRobot.Adapt<DingTalkConnection>(config);
        return dingTalkConnection;
    }

    #region DingTalk

    /// <summary>
    /// 钉钉推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="atMobiles"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToText(DingTalkText text, List<string>? atMobiles = null, bool isAtAll = false)
    {
        return await _dingTalkRobot.TextMessage(text, atMobiles, isAtAll);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToLink(DingTalkLink link)
    {
        return await _dingTalkRobot.LinkMessage(link);
    }

    /// <summary>
    /// 钉钉推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <param name="atMobiles"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToMarkdown(DingTalkMarkdown markdown, List<string>? atMobiles = null, bool isAtAll = false)
    {
        return await _dingTalkRobot.MarkdownMessage(markdown, atMobiles, isAtAll);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToActionCard(DingTalkActionCard actionCard)
    {
        return await _dingTalkRobot.ActionCardMessage(actionCard);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToFeedCard(DingTalkFeedCard feedCard)
    {
        return await _dingTalkRobot.FeedCardMessage(feedCard);
    }

    #endregion DingTalk
}