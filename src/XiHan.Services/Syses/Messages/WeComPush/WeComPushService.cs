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

using Mapster;
using XiHan.Common.Apps.Services;
using XiHan.Common.Responses.Results;
using XiHan.Models.Syses;
using XiHan.Models.Syses.Enums;
using XiHan.Services.Bases;
using XiHan.Utils.Enums;
using XiHan.Utils.Https;
using XiHan.Utils.Messages.WeCom;
using File = XiHan.Utils.Messages.WeCom.File;

namespace XiHan.Services.Syses.Messages.WeComPush;

/// <summary>
/// WeComMessagePushService
/// </summary>
[AppService(ServiceType = typeof(IWeComPushService), ServiceLifetime = ServiceLifeTimeEnum.Scoped)]
public class WeComPushService : BaseService<SysWebHook>, IWeComPushService
{
    private readonly WeComRobotHelper _weComRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    public WeComPushService(IHttpPollyHelper httpPolly)
    {
        WeComConnection weComConnection = GetWeComConn().Result;
        _weComRobot = new WeComRobotHelper(httpPolly, weComConnection);
    }

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <returns></returns>
    private async Task<WeComConnection> GetWeComConn()
    {
        var sysWebHook = await GetFirstAsync(e => e.IsEnabled && e.WebHookType == WebHookTypeEnum.WeCom.GetEnumValueByKey());
        var config = new TypeAdapterConfig()
            .ForType<SysWebHook, WeComConnection>()
            .Map(dest => dest.Key, src => src.AccessTokenOrKey)
            .Config;
        WeComConnection weComConnection = sysWebHook.Adapt<WeComConnection>(config);
        return weComConnection;
    }

    #region WeCom

    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToText(Text text)
    {
        var result = await _weComRobot.TextMessage(text);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToMarkdown(Markdown markdown)
    {
        var result = await _weComRobot.MarkdownMessage(markdown);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToImage(Image image)
    {
        var result = await _weComRobot.ImageMessage(image);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToNews(News news)
    {
        var result = await _weComRobot.NewsMessage(news);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToFile(File file)
    {
        var result = await _weComRobot.FileMessage(file);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToTextNotice(TemplateCardTextNotice templateCard)
    {
        var result = await _weComRobot.TextNoticeMessage(templateCard);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToNewsNotice(TemplateCardNewsNotice templateCard)
    {
        var result = await _weComRobot.NewsNoticeMessage(templateCard);
        return WeComMessageReturn(result);
    }

    /// <summary>
    /// 微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeComToUploadkFile(FileStream fileStream)
    {
        var result = await _weComRobot.UploadkFile(fileStream);
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
                return BaseResultDto.Success("发送成功");
            }
            else
            {
                return BaseResultDto.BadRequest(result?.ErrMsg ?? "发送失败");
            }
        }
        return BaseResultDto.InternalServerError();
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
                var uploadResult = new WeComUploadResultDto
                {
                    Message = "上传成功",
                    MediaId = result.MediaId
                };
                return BaseResultDto.Success(uploadResult);
            }
            else
            {
                return BaseResultDto.BadRequest(result?.ErrMsg ?? "上传失败");
            }
        }
        return BaseResultDto.InternalServerError();
    }

    #endregion
}