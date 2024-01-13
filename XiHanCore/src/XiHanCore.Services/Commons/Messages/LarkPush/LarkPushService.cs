#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:LarkMessagePushService
// Guid:ac92fd5d-aa9d-4afd-9355-519e52eb5b09
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-06 下午 07:40:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using XiHanCore.Infrastructures.Apps.Services;
using XiHanCore.Infrastructures.Responses;
using XiHanCore.Models.Syses;
using XiHanCore.Models.Syses.Enums;
using XiHanCore.Services.Bases;
using XiHanCore.Subscriptions.Bots.Lark;
using XiHanCore.Utils.Exceptions;

namespace XiHanCore.Services.Commons.Messages.LarkPush;

/// <summary>
/// LarkMessagePush
/// </summary>
[AppService(ServiceType = typeof(ILarkPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class LarkPushService : BaseService<SysBot>, ILarkPushService
{
    private readonly LarkBot _larkBot;

    /// <summary>
    /// 构造函数
    /// </summary>
    public LarkPushService()
    {
        var larkConnection = GetLarkConn().Result ?? throw new CustomException("未添加飞书推送配置或配置不可用！");
        _larkBot = new LarkBot(larkConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<LarkConnection> GetLarkConn()
    {
        var sysCustomBot = await GetFirstAsync(e => e.IsEnabled && e.BotType == BotTypeEnum.Lark);
        var config = new TypeAdapterConfig().ForType<SysBot, LarkConnection>()
            .Map(dest => dest.AccessToken, src => src.AccessTokenOrKey)
            .Config;
        var larkConnection = sysCustomBot.Adapt<LarkConnection>(config);
        return larkConnection;
    }

    #region Lark

    /// <summary>
    /// 飞书推送文本消息
    /// </summary>
    /// <param name="larkText"></param>
    /// <returns></returns>
    public async Task<ApiResult> LarkToText(LarkText larkText)
    {
        return await _larkBot.TextMessage(larkText);
    }

    /// <summary>
    /// 飞书推送富文本消息
    /// </summary>
    /// <param name="larkPost"></param>
    /// <returns></returns>
    public async Task<ApiResult> LarkToPost(LarkPost larkPost)
    {
        return await _larkBot.PostMessage(larkPost);
    }

    /// <summary>
    /// 飞书推送图片消息
    /// </summary>
    /// <param name="larkImage"></param>
    /// <returns></returns>
    public async Task<ApiResult> LarkToImage(LarkImage larkImage)
    {
        return await _larkBot.ImageMessage(larkImage);
    }

    /// <summary>
    /// 飞书推送消息卡片消息
    /// </summary>
    /// <param name="larkInterActive"></param>
    /// <returns></returns>
    public async Task<ApiResult> LarkToInterActive(LarkInterActive larkInterActive)
    {
        return await _larkBot.InterActiveMessage(larkInterActive);
    }

    #endregion Lark
}