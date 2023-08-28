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
using XiHan.Subscriptions.WebHooks.DingTalk;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Commons.Messages.DingTalkPush;

/// <summary>
/// DingTalkMessagePush
/// </summary>
[AppService(ServiceType = typeof(IDingTalkPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class DingTalkPushService : BaseService<SysRobot>, IDingTalkPushService
{
    private readonly DingTalkCustomRobot _dingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    public DingTalkPushService(IHttpPollyService httpPolly)
    {
        DingTalkConnection dingTalkConnection = GetDingTalkConn().Result ?? throw new CustomException("未添加钉钉推送配置或配置不可用！");
        _dingTalkRobot = new DingTalkCustomRobot(httpPolly, dingTalkConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<DingTalkConnection> GetDingTalkConn()
    {
        SysRobot sysCustomRobot = await GetFirstAsync(e => e.IsEnabled && e.RobotType == RobotTypeEnum.DingTalk.GetEnumValueByKey());
        TypeAdapterConfig config = new TypeAdapterConfig().ForType<SysRobot, DingTalkConnection>()
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
    public async Task<ApiResult> DingTalkToText(DingTalkText text, List<string>? atMobiles = null, bool isAtAll = false)
    {
        return await _dingTalkRobot.TextMessage(text, atMobiles, isAtAll);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToLink(DingTalkLink link)
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
    public async Task<ApiResult> DingTalkToMarkdown(DingTalkMarkdown markdown, List<string>? atMobiles = null, bool isAtAll = false)
    {
        return await _dingTalkRobot.MarkdownMessage(markdown, atMobiles, isAtAll);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToActionCard(DingTalkActionCard actionCard)
    {
        return await _dingTalkRobot.ActionCardMessage(actionCard);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<ApiResult> DingTalkToFeedCard(DingTalkFeedCard feedCard)
    {
        return await _dingTalkRobot.FeedCardMessage(feedCard);
    }

    #endregion DingTalk
}