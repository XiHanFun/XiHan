// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:MessagePush
// Guid:ac92fd5d-aa9d-4afd-9355-519e52eb5b09
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 下午 07:40:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Utils.DingTalk;
using ZhaiFanhuaBlog.Utils.Http;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;

namespace ZhaiFanhuaBlog.Services.Utils;

/// <summary>
/// MessagePush
/// </summary>
public class MessagePush : IMessagePush
{
    private readonly IHttpHelper _IHttpHelper;
    private readonly DingTalkRobot _DingTalkRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    public MessagePush(IHttpHelper iHttpHelper)
    {
        string webHookUrl = AppSettings.DingTalk.WebHookUrl;
        string secret = AppSettings.DingTalk.Secret;
        _IHttpHelper = iHttpHelper;
        _DingTalkRobot = new DingTalkRobot(_IHttpHelper, webHookUrl, secret);
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
        return DingTalkReturn(result);
    }

    /// <summary>
    /// 钉钉推送链接消息
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToLink(Link link)
    {
        ResultInfo? result = await _DingTalkRobot.LinkMessage(link);
        return DingTalkReturn(result);
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
        return DingTalkReturn(result);
    }

    /// <summary>
    /// 钉钉推送任务卡片消息
    /// </summary>
    /// <param name="actionCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToActionCard(ActionCard actionCard)
    {
        ResultInfo? result = await _DingTalkRobot.ActionCardMessage(actionCard);
        return DingTalkReturn(result);
    }

    /// <summary>
    /// 钉钉推送卡片菜单消息
    /// </summary>
    /// <param name="feedCard"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToFeedCard(FeedCard feedCard)
    {
        ResultInfo? result = await _DingTalkRobot.FeedCardMessage(feedCard);
        return DingTalkReturn(result);
    }

    /// <summary>
    /// 统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto DingTalkReturn(ResultInfo? result)
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