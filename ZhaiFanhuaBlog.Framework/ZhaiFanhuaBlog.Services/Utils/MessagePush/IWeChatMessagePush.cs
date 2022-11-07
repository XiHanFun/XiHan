// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IWeChartMessagePush
// Guid:54d10161-b312-4b9f-bde7-603f416f2066
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:43:46
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Utils.MessagePush.WeChat;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.Services.Utils.MessagePush;

/// <summary>
/// IWechartMessagePush
/// </summary>
public interface IWeChatMessagePush : IScopeDependency
{
    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToText(Text text);

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToMarkdown(Markdown markdown);

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    Task<BaseResultDto> WeChatToImage(Image image);

    ///// <summary>
    ///// 微信推送卡片菜单消息
    ///// </summary>
    ///// <param name="feedCard"></param>
    ///// <returns></returns>
    //Task<BaseResultDto> WeChatToFeedCard(FeedCard feedCard);
}