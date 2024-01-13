#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:LarkController
// Guid:fccfeb28-624c-41cb-9c5c-0b0652648a6b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-12 下午 07:50:21
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiHan.Infrastructures.Responses;
using XiHan.Services.Commons.Messages.LarkPush;
using XiHan.Subscriptions.Bots.Lark;
using XiHan.WebCore.Common.Swagger;

namespace XiHan.WebHost.Controllers.Commons.Messages;

/// <summary>
/// 飞书消息推送管理
/// <code>包含：文本/富文本/图片/消息卡片</code>
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="larkPushService"></param>
[AllowAnonymous]
[ApiGroup(ApiGroupNameEnum.Common)]
public class LarkController(ILarkPushService larkPushService) : BaseApiController
{
    private readonly ILarkPushService _larkPushService = larkPushService;

    /// <summary>
    /// 文本
    /// </summary>
    /// <returns></returns>
    [HttpPost("Text")]
    public async Task<ApiResult> LarkToText()
    {
        LarkText text = new()
        {
            Text = "看万山红遍，层林尽染；漫江碧透，百舸争流。"
        };
        return await _larkPushService.LarkToText(text);
    }

    /// <summary>
    /// 富文本
    /// </summary>
    /// <returns></returns>
    [HttpPost("Post")]
    public async Task<ApiResult> LarkToPost()
    {
        LarkPost post = new()
        {
            Title = "看万山红遍，层林尽染；漫江碧透，百舸争流。",
            Content = [[
                new TagText
                {
                    Text = "项目有更新"
                },
                new TagA
                {
                    Text = "请查看",
                    Href = "http://www.example.com/"
                },
                new TagAt
                {
                    UserId = "ou_18eac8********17ad4f02e8bbbb",
                    UserName = "某用户"
                },
            ]]
        };
        return await _larkPushService.LarkToPost(post);
    }

    /// <summary>
    /// 图片
    /// </summary>
    /// <returns></returns>
    [HttpPost("Image")]
    public async Task<ApiResult> LarkToImage()
    {
        LarkImage image = new()
        {
            ImageKey = "image",
        };
        return await _larkPushService.LarkToImage(image);
    }

    /// <summary>
    /// 消息卡片
    /// </summary>
    /// <returns></returns>
    [HttpPost("InterActive")]
    public async Task<ApiResult> LarkToInterActive()
    {
        LarkInterActive interActive = new()
        {
            Header = new InterActiveHeader
            {
                Title = new TagTitleOrText
                {
                    Content = "今日旅游推荐"
                }
            },
            Elements = [
                new TagDiv
                {
                    Text = new TagMarkdown
                    {
                        Content = "**西湖**，位于浙江省杭州市西湖区龙井路1号，杭州市区西部，景区总面积49平方千米，汇水面积为21.22平方千米，湖面面积为6.38平方千米。"
                    }
                },
                new TagAction
                {
                    Actions = [
                       new TagButton
                       {
                           Text = new TagMarkdown
                           {
                               Content = "更多景点介绍 :玫瑰:"
                           },
                           Url = "https://www.example.com",
                       }
                   ]
                }
            ]
        };
        return await _larkPushService.LarkToInterActive(interActive);
    }
}