// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IMessagePush
// Guid:c9b248ce-f261-4abd-88ac-f4dfc35ada28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-06 下午 07:40:59
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.Services.Utils;

/// <summary>
/// IMessagePush
/// </summary>
public interface IMessagePush
{
    /// <summary>
    /// 钉钉推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="atUsers"></param>
    /// <param name="isAtAll"></param>
    /// <returns></returns>
    Task<BaseResultDto> DingTalkToText(string text, List<string> atUsers, bool isAtAll);
}