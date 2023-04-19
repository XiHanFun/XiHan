#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeComMessagePushService
// Guid:a273c787-f81d-4c4b-875e-c20d8a04ab45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:44:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Apps.Services;
using XiHan.Infrastructure.Contexts;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Utils.Https;
using XiHan.Utils.Messages.WeCom;
using File = XiHan.Utils.Messages.WeCom.File;

namespace XiHan.Services.Syses.Messages.WeComPush;

/// <summary>
/// WeComMessagePushService
/// </summary>
[AppService(ServiceType = typeof(IWeComPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class WeComPushService : IWeComPushService
{
    /// <summary>
    /// 机器人实例
    /// </summary>
    private readonly WeComRobotHelper WeComRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iHttpHelper"></param>
    public WeComPushService(IHttpPollyHelper iHttpHelper)
    {
        WeComConnection conn = new();
        WeComRobot = new WeComRobotHelper(iHttpHelper, conn);
    }

    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToText(Text text)
    {
        var result = await WeComRobot.TextMessage(text);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToMarkdown(Markdown markdown)
    {
        var result = await WeComRobot.MarkdownMessage(markdown);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToImage(Image image)
    {
        var result = await WeComRobot.ImageMessage(image);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToNews(News news)
    {
        var result = await WeComRobot.NewsMessage(news);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToFile(File file)
    {
        var result = await WeComRobot.FileMessage(file);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToTextNotice(TemplateCardTextNotice templateCard)
    {
        var result = await WeComRobot.TextNoticeMessage(templateCard);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToNewsNotice(TemplateCardNewsNotice templateCard)
    {
        var result = await WeComRobot.NewsNoticeMessage(templateCard);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToUploadkFile(FileStream fileStream)
    {
        var result = await WeComRobot.UploadkFile(fileStream);
        return WeComUploadReturn(result);
    }

    /// <summary>
    /// 消息统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto WeComMessageReturn(WeComResultInfoDto? result)
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
    private static BaseResultDto WeComUploadReturn(WeComResultInfoDto? result)
    {
        if (result != null)
        {
            if (result.ErrCode == 0 || result?.ErrMsg == "ok")
            {
                WeComUploadResultDto uploadResult = new("上传成功", result.MediaId);
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