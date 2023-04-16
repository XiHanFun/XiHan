#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeChatMessagePushService
// Guid:a273c787-f81d-4c4b-875e-c20d8a04ab45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:44:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Apps.Services;
using XiHan.Infrastructure.Apps.Setting;
using XiHan.Infrastructure.Contexts;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Infrastructure.Enums;
using XiHan.Utils.Https;
using XiHan.Utils.Messages.WeChat;
using File = XiHan.Utils.Messages.WeChat.File;

namespace XiHan.Services.Commons.Messages.Impl;

/// <summary>
/// WeChatMessagePushService
/// </summary>
[AppService(ServiceType = typeof(IWeChatPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class WeChatPushService : IWeChatPushService
{
    /// <summary>
    /// 机器人实例
    /// </summary>
    private readonly WeChatRobotHelper WeChatRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    public WeChatPushService(IHttpPollyHelper iHttpHelper)
    {
        WeChatConnection conn = new()
        {
            WebHookUrl = AppSettings.Message.WeChart.WebHookUrl.GetValue(),
            UploadkUrl = AppSettings.Message.WeChart.UploadkUrl.GetValue(),
            Key = AppSettings.Message.WeChart.Key.GetValue()
        };
        WeChatRobot = new WeChatRobotHelper(iHttpHelper, conn);
    }

    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToText(Text text)
    {
        var result = await WeChatRobot.TextMessage(text);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToMarkdown(Markdown markdown)
    {
        var result = await WeChatRobot.MarkdownMessage(markdown);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToImage(Image image)
    {
        var result = await WeChatRobot.ImageMessage(image);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToNews(News news)
    {
        var result = await WeChatRobot.NewsMessage(news);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToFile(File file)
    {
        var result = await WeChatRobot.FileMessage(file);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToTextNotice(TemplateCardTextNotice templateCard)
    {
        var result = await WeChatRobot.TextNoticeMessage(templateCard);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToNewsNotice(TemplateCardNewsNotice templateCard)
    {
        var result = await WeChatRobot.NewsNoticeMessage(templateCard);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToUploadkFile(FileStream fileStream)
    {
        var result = await WeChatRobot.UploadkFile(fileStream);
        return WeChatUploadReturn(result);
    }

    /// <summary>
    /// 消息统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto WeChatMessageReturn(WeChatResultInfoDto? result)
    {
        if (result != null)
        {
            if (result.ErrCode == 0 || result?.ErrMsg == "ok")
            {
                return BaseResponseDto.Ok("发送成功");
            }
            else
            {
                return BaseResponseDto.BadRequest(result?.ErrMsg ?? "发送失败");
            }
        }
        return BaseResponseDto.InternalServerError();
    }

    /// <summary>
    /// 上传文件统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto WeChatUploadReturn(WeChatResultInfoDto? result)
    {
        if (result != null)
        {
            if (result.ErrCode == 0 || result?.ErrMsg == "ok")
            {
                WeChatUploadResultDto uploadResult = new("上传成功", result.MediaId);
                return BaseResponseDto.Ok(uploadResult);
            }
            else
            {
                return BaseResponseDto.BadRequest(result?.ErrMsg ?? "上传失败");
            }
        }
        return BaseResponseDto.InternalServerError();
    }
}