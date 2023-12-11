#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ILarkMessagePushService
// Guid:c9b248ce-f261-4abd-88ac-f4dfc35ada28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-06 下午 07:40:59
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses;
using XiHan.Subscriptions.Bots.Lark;

namespace XiHan.Services.Commons.Messages.LarkPush;

/// <summary>
/// ILarkMessagePushService
/// </summary>
public interface ILarkPushService
{
    /// <summary>
    /// 飞书推送文本消息
    /// </summary>
    /// <param name="larkText"></param>
    /// <returns></returns>
    Task<ApiResult> LarkToText(LarkText larkText);

    /// <summary>
    /// 飞书推送富文本消息
    /// </summary>
    /// <param name="larkPost"></param>
    /// <returns></returns>
    Task<ApiResult> LarkToPost(LarkPost larkPost);

    /// <summary>
    /// 飞书推送图片消息
    /// </summary>
    /// <param name="larkImage"></param>
    /// <returns></returns>
    Task<ApiResult> LarkToImage(LarkImage larkImage);

    /// <summary>
    /// 飞书推送消息卡片消息
    /// </summary>
    /// <param name="larkInterActive"></param>
    /// <returns></returns>
    Task<ApiResult> LarkToInterActive(LarkInterActive larkInterActive);
}