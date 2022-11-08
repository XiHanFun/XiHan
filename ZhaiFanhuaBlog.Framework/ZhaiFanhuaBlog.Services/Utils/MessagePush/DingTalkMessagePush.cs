// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DingTalkMessagePush
// Guid:ac92fd5d-aa9d-4afd-9355-519e52eb5b09
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 下午 07:40:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Utils.Http;
using ZhaiFanhuaBlog.Utils.MessagePush.DingTalk;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;

namespace ZhaiFanhuaBlog.Services.Utils.MessagePush;

/// <summary>
/// DingTalkMessagePush
/// </summary>
public class DingTalkMessagePush : IDingTalkMessagePush
{
    /// <summary>
    /// 请求接口
    /// </summary>
    private readonly IHttpHelper _IHttpHelper;

    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    private readonly string _WebHookUrl = string.Empty;

    /// <summary>
    /// 访问令牌
    /// </summary>
    private readonly string _AccessToken = string.Empty;

    /// <summary>
    /// 机密
    /// </summary>
    private readonly string? _Secret = string.Empty;

    /// <summary>
    /// 机器人实例
    /// </summary>
    private readonly DingTalkRobot _DingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    public DingTalkMessagePush(IHttpHelper iHttpHelper)
    {
        _IHttpHelper = iHttpHelper;
        _WebHookUrl = AppSettings.DingTalk.WebHookUrl;
        _AccessToken = AppSettings.DingTalk.AccessToken;
        _Secret = AppSettings.DingTalk.Secret;
        _DingTalkRobot = new DingTalkRobot(_IHttpHelper, _WebHookUrl, _AccessToken, _Secret);
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
            if (result.ErrCode == "0" || result?.ErrMsg == "ok")
                return BaseResponseDto.OK("发送成功");
            else
                return BaseResponseDto.BadRequest(result?.ErrMsg ?? "发送失败");
        }
        return BaseResponseDto.InternalServerError();
    }

    #endregion DingTalk
}