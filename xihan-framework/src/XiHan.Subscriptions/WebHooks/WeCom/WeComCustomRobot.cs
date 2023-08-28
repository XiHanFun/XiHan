#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WeComCustomRobot
// Guid:1f9edb73-56c9-4849-88a8-c57488b3582d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-08 上午 02:32:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Requests.Https;
using XiHan.Infrastructures.Responses;
using XiHan.Utils.Extensions;
using XiHan.Utils.Serializes;

namespace XiHan.Subscriptions.WebHooks.WeCom;

/// <summary>
/// 企业微信自定义机器人消息推送
/// https://developer.work.weixin.qq.com/document/path/91770
/// </summary>
public class WeComCustomRobot
{
    private readonly IHttpPollyService _httpPollyService;
    private readonly string _messageUrl;

    // 正式文件上传地址，调用接口凭证, 机器人 webhook 中的 key 参数
    private readonly string _fileUrl;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPollyService"></param>
    /// <param name="weChatConnection"></param>
    public WeComCustomRobot(IHttpPollyService httpPollyService, WeComConnection weChatConnection)
    {
        _httpPollyService = httpPollyService;
        _messageUrl = weChatConnection.WebHookUrl + "?key=" + weChatConnection.Key;
        _fileUrl = weChatConnection.UploadUrl + "?key=" + weChatConnection.Key + "&type=file";
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <returns></returns>
    public async Task<ApiResult> TextMessage(WeComText text)
    {
        // 消息类型
        string msgType = WeComMsgTypeEnum.Text.GetEnumDescriptionByKey();
        // 发送
        ApiResult result = await SendMessage(new { msgType, text });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="markdown">文档</param>
    /// <returns></returns>
    public async Task<ApiResult> MarkdownMessage(WeComMarkdown markdown)
    {
        // 消息类型
        string msgType = WeComMsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        // 发送
        ApiResult result = await SendMessage(new { msgType, markdown });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="image">图片</param>
    /// <returns></returns>
    public async Task<ApiResult> ImageMessage(WeComImage image)
    {
        // 消息类型
        string msgType = WeComMsgTypeEnum.Image.GetEnumDescriptionByKey();
        // 发送
        ApiResult result = await SendMessage(new { msgType, image });
        return result;
    }

    /// <summary>
    /// 发送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<ApiResult> NewsMessage(WeComNews news)
    {
        // 消息类型
        string msgType = WeComMsgTypeEnum.News.GetEnumDescriptionByKey();
        // 发送
        ApiResult result = await SendMessage(new { msgType, news });
        return result;
    }

    /// <summary>
    /// 发送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<ApiResult> FileMessage(WeComFile file)
    {
        // 消息类型
        string msgType = WeComMsgTypeEnum.File.GetEnumDescriptionByKey();
        // 发送
        ApiResult result = await SendMessage(new { msgType, file });
        return result;
    }

    /// <summary>
    /// 发送文本通知模版卡片消息
    /// </summary>
    /// <param name="templateCard">模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> TextNoticeMessage(WeComTemplateCardTextNotice templateCard)
    {
        // 消息类型
        string msgType = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        templateCard.CardType = WeComTemplateCardType.TextNotice.GetEnumDescriptionByKey();
        // 发送
        ApiResult result = await SendMessage(new { msgType, template_card = templateCard });
        return result;
    }

    /// <summary>
    /// 发送图文展示模版卡片消息
    /// </summary>
    /// <param name="templateCard">模版卡片</param>
    /// <returns></returns>
    public async Task<ApiResult> NewsNoticeMessage(WeComTemplateCardNewsNotice templateCard)
    {
        // 消息类型
        string msgType = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        templateCard.CardType = WeComTemplateCardType.NewsNotice.GetEnumDescriptionByKey();
        // 发送
        ApiResult result = await SendMessage(new { msgType, template_card = templateCard });
        return result;
    }

    /// <summary>
    /// 微信执行上传文件
    /// </summary>
    /// <remarks>素材上传得到media_id，该media_id仅三天内有效，且只能对应上传文件的机器人可以使用</remarks>
    /// <remarks>文件大小在5B~20M之间</remarks>
    /// <returns></returns>
    public async Task<ApiResult> UploadFile(FileStream fileStream)
    {
        Dictionary<string, string> headers = new()
        {
            { "filename", fileStream.Name },
            { "filelength", fileStream.Length.ToString() }
        };
        // 发起请求，上传地址
        WeComResultInfoDto? result =
            await _httpPollyService.PostAsync<WeComResultInfoDto>(HttpGroupEnum.Remote, _fileUrl, fileStream, headers);
        // 包装返回信息
        if (result != null)
        {
            if (result.ErrCode == 0 || result.ErrMsg == "ok")
            {
                WeComUploadResultDto uploadResult = new()
                {
                    Message = "上传成功",
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
        string sendMessage = objSend.SerializeToJson();
        // 发起请求，发送消息地址
        WeComResultInfoDto? result = await _httpPollyService.PostAsync<WeComResultInfoDto>(HttpGroupEnum.Remote, _messageUrl, sendMessage);
        // 包装返回信息
        return result != null
            ? result.ErrCode == 0 || result.ErrMsg == "ok" ? ApiResult.Success("发送成功") : ApiResult.BadRequest("发送失败")
            : ApiResult.InternalServerError();
    }
}