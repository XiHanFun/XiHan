#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeChatMessagePush
// Guid:a273c787-f81d-4c4b-875e-c20d8a04ab45
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:44:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.AppSettings;
using ZhaiFanhuaBlog.Utils.Http;
using ZhaiFanhuaBlog.Utils.MessagePush.WeChat;
using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Response;
using File = ZhaiFanhuaBlog.Utils.MessagePush.WeChat.File;

namespace ZhaiFanhuaBlog.Services.Utils.MessagePush;

/// <summary>
/// WeChatMessagePush
/// </summary>
public class WeChatMessagePush : IWeChatMessagePush
{
    /// <summary>
    /// 请求接口
    /// </summary>
    private readonly IHttpHelper _IHttpHelper;

    /// <summary>
    /// 网络挂钩地址
    /// </summary>
    private readonly string _WebHookUrl = string.Empty;

    /// <summary>
    /// 文件上传地址
    /// </summary>
    private readonly string _UploadkUrl = string.Empty;

    /// <summary>
    /// 访问令牌
    /// </summary>
    private readonly string _Key = string.Empty;

    /// <summary>
    /// 机器人实例
    /// </summary>
    private readonly WeChatRobot _WeChatRobot;

    /// <summary>
    /// 构造函数
    /// </summary>
    public WeChatMessagePush(IHttpHelper iHttpHelper)
    {
        _IHttpHelper = iHttpHelper;
        _WebHookUrl = AppSettings.WeChart.WebHookUrl;
        _UploadkUrl = AppSettings.WeChart.UploadkUrl;
        _Key = AppSettings.WeChart.Key;
        _WeChatRobot = new WeChatRobot(_IHttpHelper, _WebHookUrl, _UploadkUrl, _Key);
    }

    /// <summary>
    /// 微信推送文本消息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToText(Text text)
    {
        ResultInfo? result = await _WeChatRobot.TextMessage(text);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文档消息
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToMarkdown(Markdown markdown)
    {
        ResultInfo? result = await _WeChatRobot.MarkdownMessage(markdown);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图片消息
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToImage(Image image)
    {
        ResultInfo? result = await _WeChatRobot.ImageMessage(image);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToNews(News news)
    {
        ResultInfo? result = await _WeChatRobot.NewsMessage(news);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToFile(File file)
    {
        ResultInfo? result = await _WeChatRobot.FileMessage(file);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送文本通知消息
    /// </summary>
    /// <param name="templateCard">文本通知-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToTextNotice(TemplateCardTextNotice templateCard)
    {
        templateCard.CardType = TemplateCardType.text_notice.ToString();
        ResultInfo? result = await _WeChatRobot.TextNoticeMessage(templateCard);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信推送图文展示消息
    /// </summary>
    /// <param name="templateCard">图文展示-模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToNewsNotice(TemplateCardNewsNotice templateCard)
    {
        templateCard.CardType = TemplateCardType.news_notice.ToString();
        ResultInfo? result = await _WeChatRobot.NewsNoticeMessage(templateCard);
        return WeChatMessageReturn(result);
    }

    /// <summary>
    /// 微信上传文件
    /// </summary>
    /// <param name="fileStream">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> WeChatToUploadkFile(FileStream fileStream)
    {
        ResultInfo? result = await _WeChatRobot.UploadkFile(fileStream);
        return WeChatUploadReturn(result);
    }

    /// <summary>
    /// 消息统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto WeChatMessageReturn(ResultInfo? result)
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

    /// <summary>
    /// 上传文件统一格式返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private static BaseResultDto WeChatUploadReturn(ResultInfo? result)
    {
        if (result != null)
        {
            if (result.ErrCode == "0" || result?.ErrMsg == "ok")
            {
                WeChatUploadResult uploadResult = new("上传成功", result.MediaId);
                return BaseResponseDto.OK(uploadResult);
            }
            else
                return BaseResponseDto.BadRequest(result?.ErrMsg ?? "上传失败");
        }
        return BaseResponseDto.InternalServerError();
    }
}