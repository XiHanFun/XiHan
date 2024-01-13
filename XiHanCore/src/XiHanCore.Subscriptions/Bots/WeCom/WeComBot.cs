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

using XiHanCore.Infrastructures.Apps;
using XiHanCore.Infrastructures.Requests.Https;
using XiHanCore.Infrastructures.Responses;
using XiHanCore.Utils.Extensions;
using XiHanCore.Utils.Serializes;

namespace XiHanCore.Subscriptions.Bots.WeCom;

/// <summary>
/// 企业微信自定义机器人消息推送
/// https://developer.work.weixin.qq.com/document/path/91770
/// 每个机器人发送的消息不能超过20条/分钟
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="weChatConnection"></param>
public class WeComBot(WeComConnection weChatConnection)
{
    private readonly string _messageUrl = weChatConnection.WebHookUrl + "?key=" + weChatConnection.Key;

    // 文件上传地址，调用接口凭证, 机器人 webhook 中的 key 参数
    private readonly string _uploadUrl = weChatConnection.UploadUrl + "?key=" + weChatConnection.Key;

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="weComText">内容</param>
    /// <returns></returns>
    public async Task<ApiResult> TextMessage(WeComText weComText)
    {
        var msgType = WeComMsgTypeEnum.Text.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            text = weComText
        });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="weComMarkdown">文档</param>
    /// <returns></returns>
    public async Task<ApiResult> MarkdownMessage(WeComMarkdown weComMarkdown)
    {
        var msgType = WeComMsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            markdown = weComMarkdown
        });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="weComImage">图片</param>
    /// <returns></returns>
    public async Task<ApiResult> ImageMessage(WeComImage weComImage)
    {
        var msgType = WeComMsgTypeEnum.Image.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            image = weComImage
        });
        return result;
    }

    /// <summary>
    /// 发送图文消息
    /// </summary>
    /// <param name="weComNews">图文</param>
    /// <returns></returns>
    public async Task<ApiResult> NewsMessage(WeComNews weComNews)
    {
        var msgType = WeComMsgTypeEnum.News.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            news = weComNews
        });
        return result;
    }

    /// <summary>
    /// 发送文件消息
    /// </summary>
    /// <param name="weComFile">文件</param>
    /// <returns></returns>
    public async Task<ApiResult> FileMessage(WeComFile weComFile)
    {
        var msgType = WeComMsgTypeEnum.File.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            file = weComFile
        });
        return result;
    }

    /// <summary>
    /// 发送语音消息
    /// </summary>
    /// <param name="weComVoice">语音</param>
    /// <returns></returns>
    public async Task<ApiResult> VoiceMessage(WeComVoice weComVoice)
    {
        var msgType = WeComMsgTypeEnum.Voice.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            voice = weComVoice
        });
        return result;
    }

    /// <summary>
    /// 发送文本通知模版卡片消息
    /// </summary>
    /// <param name="weComTemplateCardTextNotice">模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> TextNoticeMessage(WeComTemplateCardTextNotice weComTemplateCardTextNotice)
    {
        var msgType = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        weComTemplateCardTextNotice.CardType = WeComTemplateCardType.TextNotice.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            template_card = weComTemplateCardTextNotice
        });
        return result;
    }

    /// <summary>
    /// 发送图文展示模版卡片消息
    /// </summary>
    /// <param name="weComTemplateCardNewsNotice">模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> NewsNoticeMessage(WeComTemplateCardNewsNotice weComTemplateCardNewsNotice)
    {
        var msgType = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        weComTemplateCardNewsNotice.CardType = WeComTemplateCardType.NewsNotice.GetEnumDescriptionByKey();
        var result = await SendMessage(new
        {
            msgtype = msgType,
            template_card = weComTemplateCardNewsNotice
        });
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
            {
                "filename", fileStream.Name
            },
            {
                "filelength", fileStream.Length.ToString()
            }
        };

        var type = uploadType switch
        {
            WeComUploadType.File => "&type=file",
            WeComUploadType.Voice => "&type=voice",
            _ => string.Empty
        };

        // 发起请求，上传地址
        var httpPollyService = App.GetRequiredService<IHttpPollyService>();
        var result = await httpPollyService.PostAsync<WeComResultInfoDto>(HttpGroupEnum.Remote, _uploadUrl + type, fileStream, headers);
        // 包装返回信息
        if (result == null) return ApiResult.InternalServerError();
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

    /// <summary>
    /// 微信执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<ApiResult> SendMessage(object objSend)
    {
        // 发起请求
        var sendMessage = objSend.SerializeTo();
        var httpPollyService = App.GetRequiredService<IHttpPollyService>();
        var result = await httpPollyService.PostAsync<WeComResultInfoDto>(HttpGroupEnum.Remote, _messageUrl, sendMessage);
        // 包装返回信息
        return result != null
            ? result.ErrCode == 0 || result.ErrMsg == "ok" ? ApiResult.Success("发送成功；") : ApiResult.BadRequest("发送失败；")
            : ApiResult.InternalServerError();
    }
}