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

using ZhaiFanhuaBlog.Extensions.Bases.Response.Results;
using ZhaiFanhuaBlog.Extensions.Response;
using ZhaiFanhuaBlog.Infrastructure.AppService;
using ZhaiFanhuaBlog.Infrastructure.AppSetting;
using ZhaiFanhuaBlog.Utils.Http;
using ZhaiFanhuaBlog.Utils.MessagePush.DingTalk;

namespace ZhaiFanhuaBlog.Services.Utils.MessagePush;

/// <summary>
/// DingTalkMessagePush
/// </summary>
[AppService(ServiceType = typeof(IDingTalkMessagePushService), ServiceLifetime = LifeTime.Scoped)]
public class DingTalkMessagePushService : IDingTalkMessagePushService
{
    /// <summary>
    /// 机器人实例
    /// </summary>
    private readonly DingTalkRobotHelper _DingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    public DingTalkMessagePushService(IHttpHelper iHttpHelper)
    {
        DingTalkConnection conn = new()
        {
            WebHookUrl = AppSettings.MessagePush.DingTalk.WebHookUrl,
            AccessToken = AppSettings.MessagePush.DingTalk.AccessToken,
            Secret = AppSettings.MessagePush.DingTalk.Secret
        };
        _DingTalkRobot = new DingTalkRobotHelper(iHttpHelper, conn);
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
        ResultInfo? result = await _DingTalkRobot.TextMessage(text, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToLink(Link link)
    {
        ResultInfo? result = await _DingTalkRobot.LinkMessage(link);
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
        ResultInfo? result = await _DingTalkRobot.MarkdownMessage(markdown, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToActionCard(ActionCard actionCard)
    {
        ResultInfo? result = await _DingTalkRobot.ActionCardMessage(actionCard);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToFeedCard(FeedCard feedCard)
    {
        ResultInfo? result = await _DingTalkRobot.FeedCardMessage(feedCard);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto DingTalkMessageReturn(ResultInfo? result)
    {
        if (result != null)
        {
            if (result.ErrCode == 0 || result?.ErrMsg == "ok")
                return BaseResponseDto.OK("发送成功");
            else
                return BaseResponseDto.BadRequest(result?.ErrMsg ?? "发送失败");
        }
        return BaseResponseDto.InternalServerError();
    }

    #endregion DingTalk
}