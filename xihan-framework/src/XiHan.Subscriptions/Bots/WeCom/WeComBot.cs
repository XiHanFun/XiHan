#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WeComBot
// Guid:1f9edb73-56c9-4849-88a8-c57488b3582d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-08 上午 02:32:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.Subscriptions.Bots.WeCom;

/// <summary>
/// 企业微信自定义机器人消息推送
/// https://developer.work.weixin.qq.com/document/path/91770
/// 每个机器人发送的消息不能超过20条/分钟
/// </summary>
public class WeComBot
{
    private readonly string _messageUrl;

    // 文件上传地址，调用接口凭证, 机器人 webhook 中的 key 参数
    private readonly string _uploadUrl;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="weChatConnection"></param>
    public WeComBot(WeComConnection weChatConnection)
    {
        _messageUrl = weChatConnection.WebHookUrl + "?key=" + weChatConnection.Key;
        _uploadUrl = weChatConnection.UploadUrl + "?key=" + weChatConnection.Key;
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <returns></returns>
    public async Task<ApiResult> TextMessage(WeComText text)
    {
        var msgtype = WeComMsgTypeEnum.Text.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, text });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="markdown">文档</param>
    /// <returns></returns>
    public async Task<ApiResult> MarkdownMessage(WeComMarkdown markdown)
    {
        var msgtype = WeComMsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, markdown });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="image">图片</param>
    /// <returns></returns>
    public async Task<ApiResult> ImageMessage(WeComImage image)
    {
        var msgtype = WeComMsgTypeEnum.Image.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, image });
        return result;
    }

    /// <summary>
    /// 发送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<ApiResult> NewsMessage(WeComNews news)
    {
        var msgtype = WeComMsgTypeEnum.News.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, news });
        return result;
    }

    /// <summary>
    /// 发送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<ApiResult> FileMessage(WeComFile file)
    {
        var msgtype = WeComMsgTypeEnum.File.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, file });
        return result;
    }

    /// <summary>
    /// 发送语音消息
    /// </summary>
    /// <param name="voice">语音</param>
    /// <returns></returns>
    public async Task<ApiResult> VoiceMessage(WeComVoice voice)
    {
        var msgtype = WeComMsgTypeEnum.Voice.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, voice });
        return result;
    }

    /// <summary>
    /// 发送文本通知模版卡片消息
    /// </summary>
    /// <param name="template_card">模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> TextNoticeMessage(WeComTemplateCardTextNotice template_card)
    {
        var msgtype = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        template_card.CardType = WeComTemplateCardType.TextNotice.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, template_card });
        return result;
    }

    /// <summary>
    /// 发送图文展示模版卡片消息
    /// </summary>
    /// <param name="template_card">模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> NewsNoticeMessage(WeComTemplateCardNewsNotice template_card)
    {
        var msgtype = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        template_card.CardType = WeComTemplateCardType.NewsNotice.GetEnumDescriptionByKey();
        var result = await SendMessage(new { msgtype, template_card });
        return result;
    }

    /// <summary>
    /// 微信执行上传文件
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="uploadType">文件上传类型</param>
    /// <returns></returns>
    /// <remarks>
    /// 素材上传得到media_id，该media_id仅三天内有效，且只能对应上传文件的机器人可以使用
    /// 普通文件(file)：文件大小不超过20M
    /// 语音文件(voice)：文件大小不超过2M，播放长度不超过60s，仅支持AMR格式
    /// </remarks>
    public async Task<ApiResult> UploadFile(FileStream fileStream, WeComUploadType uploadType)
    {
        Dictionary<string, string> headers = new()
        {
            { "filename", fileStream.Name },
            { "filelength", fileStream.Length.ToString() }
        };

        var type = uploadType switch
        {
            WeComUploadType.File => "&type=file",
            WeComUploadType.Voice => "&type=voice",
            _ => string.Empty
        };

        // 发起请求，上传地址
        var _httpPollyService = App.GetRequiredService<IHttpPollyService>();
        var result =
            await _httpPollyService.PostAsync<WeComResultInfoDto>(HttpGroupEnum.Remote, _uploadUrl + type, fileStream,
                headers);
        // 包装返回信息
        if (result != null)
        {
            if (result.ErrCode == 0 || result.ErrMsg == "ok")
            {
                WeComUploadResultDto uploadResult = new()
                {
                    Message = "上传成功",
                    Type = result.Type,
                    MediaId = result.MediaId
                };
                return ApiResult.Success(uploadResult);
            }
            else
            {
                return ApiResult.BadRequest("上传失败");
            }
        }

        return ApiResult.InternalServerError();
    }

    /// <summary>
    /// 微信执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ApiResult> SendMessage(object objSend)
    {
        // 发送对象
        var sendMessage = objSend.SerializeToJson();
        // 发起请求，发送消息地址
        var _httpPollyService = App.GetRequiredService<IHttpPollyService>();
        var result =
            await _httpPollyService.PostAsync<WeComResultInfoDto>(HttpGroupEnum.Remote, _messageUrl, sendMessage);
        // 包装返回信息
        return result != null
            ? result.ErrCode == 0 || result.ErrMsg == "ok" ? ApiResult.Success("发送成功") : ApiResult.BadRequest("发送失败")
            : ApiResult.InternalServerError();
    }
}