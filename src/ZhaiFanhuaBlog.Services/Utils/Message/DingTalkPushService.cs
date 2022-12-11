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
using ZhaiFanhuaBlog.Utils.Message.DingTalk;

namespace ZhaiFanhuaBlog.Services.Utils.Message;

/// <summary>
/// DingTalkMessagePush
/// </summary>
[AppService(ServiceType = typeof(IDingTalkPushService), ServiceLifetime = LifeTime.Scoped)]
public class DingTalkPushService : IDingTalkPushService
{
    /// <summary>
    /// 机器人实例
    /// </summary>
    private readonly DingTalkRobotHelper _DingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    public DingTalkPushService(IHttpHelper iHttpHelper)
    {
        DingTalkConnection conn = new()
        {
            WebHookUrl = AppSettings.Message.DingTalk.WebHookUrl.Get(),
            AccessToken = AppSettings.Message.DingTalk.AccessToken.Get(),
            Secret = AppSettings.Message.DingTalk.Secret.Get()
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
        DingTalkResultInfo? result = await _DingTalkRobot.TextMessage(text, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToLink(Link link)
    {
        DingTalkResultInfo? result = await _DingTalkRobot.LinkMessage(link);
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
        DingTalkResultInfo? result = await _DingTalkRobot.MarkdownMessage(markdown, atMobiles, isAtAll);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToActionCard(ActionCard actionCard)
    {
        DingTalkResultInfo? result = await _DingTalkRobot.ActionCardMessage(actionCard);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToFeedCard(FeedCard feedCard)
    {
        DingTalkResultInfo? result = await _DingTalkRobot.FeedCardMessage(feedCard);
        return DingTalkMessageReturn(result);
    }

    /// <summary>
    /// 统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto DingTalkMessageReturn(DingTalkResultInfo? result)
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