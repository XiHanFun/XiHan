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
        string keyWord = AppSettings.DingTalk.KeyWord;
        string secret = AppSettings.DingTalk.Secret;
        _IHttpHelper = iHttpHelper;
        _DingTalkRobot = new DingTalkRobot(_IHttpHelper, webHookUrl, keyWord, secret);
    }

    /// <summary>
    /// 钉钉推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="atUsers"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> DingTalkToText(string text, List<string> atUsers, bool isAtAll = false)
    {
        ResultInfo? result = await _DingTalkRobot.TextMessage(text, atUsers, isAtAll);
        if (result != null)
        {
            if (result.ErrCode == "0" || result?.ErrMsg == "ok")
                return BaseResponseDto.OK("发送成功");
            else
                return BaseResponseDto.BadRequest(result?.ErrMsg ?? "发送失败");
        }
        return BaseResponseDto.InternalServerError();
    }
}