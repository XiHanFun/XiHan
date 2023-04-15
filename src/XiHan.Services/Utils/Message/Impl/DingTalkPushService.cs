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

using XiHan.Infrastructure.Apps.Services;
using XiHan.Infrastructure.Apps.Setting;
using XiHan.Infrastructure.Contexts;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Infrastructure.Enums;
using XiHan.Utils.Https;
using XiHan.Utils.Messages.DingTalk;

namespace XiHan.Services.Utils.Messages.Impl;

/// <summary>
/// DingTalkMessagePush
/// </summary>
[AppService(ServiceType = typeof(IDingTalkPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class DingTalkPushService : IDingTalkPushService
{
    /// <summary>
    /// 机器人实例
    /// </summary>
    private readonly DingTalkRobotHelper DingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    public DingTalkPushService(IHttpHelper iHttpHelper)
    {
        DingTalkConnection conn = new()
        {
            WebHookUrl = AppSettings.Message.DingTalk.WebHookUrl.GetValue(),
            AccessToken = AppSettings.Message.DingTalk.AccessToken.GetValue(),
            Secret = AppSettings.Message.DingTalk.Secret.GetValue()
        };
        DingTalkRobot = new DingTalkRobotHelper(iHttpHelper, conn);
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
        var result = await DingTalkRobot.TextMessage(text, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToLink(Link link)
    {
        var result = await DingTalkRobot.LinkMessage(link);
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
        var result = await DingTalkRobot.MarkdownMessage(markdown, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToActionCard(ActionCard actionCard)
    {
        var result = await DingTalkRobot.ActionCardMessage(actionCard);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToFeedCard(FeedCard feedCard)
    {
        var result = await DingTalkRobot.FeedCardMessage(feedCard);
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
                return BaseResponseDto.Ok("发送成功");
            else
                return BaseResponseDto.BadRequest(result?.ErrMsg ?? "发送失败");
        }
        return BaseResponseDto.InternalServerError();
    }

    #endregion DingTalk
}