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
using XiHan.Commons.Apps.Services;
using XiHan.Commons.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Bases;
using XiHan.Utils.Enums;
using XiHan.Commons.Apps.Https;
using XiHan.Subscriptions.Messages.DingTalk;

namespace XiHan.Services.Syses.Messages.DingTalkPush;

/// <summary>
/// DingTalkMessagePush
/// </summary>
[AppService(ServiceType = typeof(IDingTalkPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class DingTalkPushService : BaseService<SysWebHook>, IDingTalkPushService
{
    private readonly DingTalkRobotHelper _dingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    public DingTalkPushService(IHttpPollyHelper httpPolly)
    {
        DingTalkConnection dingTalkConnection = GetDingTalkConn().Result;
        _dingTalkRobot = new DingTalkRobotHelper(httpPolly, dingTalkConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<DingTalkConnection> GetDingTalkConn()
    {
        var sysWebHook = await GetFirstAsync(e => e.IsEnabled && e.WebHookType == WebHookTypeEnum.DingTalk.GetEnumValueByKey());
        var config = new TypeAdapterConfig()
            .ForType<SysWebHook, DingTalkConnection>()
            .Map(dest => dest.AccessToken, src => src.AccessTokenOrKey)
            .Config;
        DingTalkConnection dingTalkConnection = sysWebHook.Adapt<DingTalkConnection>(config);
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
    public async Task<BaseResultDto> DingTalkToText(Text text, List<string>? atMobiles = null, bool isAtAll = false)
    {
        var result = await _dingTalkRobot.TextMessage(text, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToLink(Link link)
    {
        var result = await _dingTalkRobot.LinkMessage(link);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <param name="atMobiles"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToMarkdown(Markdown markdown, List<string>? atMobiles = null, bool isAtAll = false)
    {
        var result = await _dingTalkRobot.MarkdownMessage(markdown, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToActionCard(ActionCard actionCard)
    {
        var result = await _dingTalkRobot.ActionCardMessage(actionCard);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToFeedCard(FeedCard feedCard)
    {
        var result = await _dingTalkRobot.FeedCardMessage(feedCard);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto DingTalkMessageReturn(DingTalkResultInfoDto? result)
    {
        if (result != null)
        {
            if (result.ErrCode == 0 || result?.ErrMsg == "ok")
            {
                return BaseResultDto.Success("发送成功");
            }
            else
            {
                return BaseResultDto.BadRequest(result?.ErrMsg ?? "发送失败");
            }
        }
        return BaseResultDto.InternalServerError();
    }

    #endregion DingTalk
}