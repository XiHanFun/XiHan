#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeComCustomRobot
// Guid:1f9edb73-56c9-4849-88a8-c57488b3582d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 上午 02:32:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Enums;
using XiHan.Utils.Serializes;
using XiHan.Infrastructures.Responses.Results;
using XiHan.Infrastructures.Requests.Https;

namespace XiHan.Subscriptions.Robots.WeCom;

/// <summary>
/// 企业微信自定义机器人消息推送
/// https://developer.work.weixin.qq.com/document/path/91770
/// </summary>
public class WeComCustomRobot
{
    private readonly IHttpPollyHelper _httpPolly;
    private readonly string _messageUrl;

    // 正式文件上传地址，调用接口凭证, 机器人 webhookurl 中的 key 参数
    private readonly string _fileUrl;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpPolly"></param>
    /// <param name="weChatConnection"></param>
    public WeComCustomRobot(IHttpPollyHelper httpPolly, WeComConnection weChatConnection)
    {
        _httpPolly = httpPolly;
        _messageUrl = weChatConnection.WebHookUrl + "?key=" + weChatConnection.Key;
        _fileUrl = weChatConnection.UploadkUrl + "?key=" + weChatConnection.Key + "&type=file";
    }

    /// <summary>
    /// 发送文本消息
    /// </summary>
    /// <param name="text">内容</param>
    /// <returns></returns>
    public async Task<BaseResultDto> TextMessage(WeComText text)
    {
        // 消息类型
        var msgtype = WeComMsgTypeEnum.Text.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, text });
        return result;
    }

    /// <summary>
    /// 发送文档消息
    /// </summary>
    /// <param name="markdown">文档</param>
    /// <returns></returns>
    public async Task<BaseResultDto> MarkdownMessage(WeComMarkdown markdown)
    {
        // 消息类型
        var msgtype = WeComMsgTypeEnum.Markdown.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, markdown });
        return result;
    }

    /// <summary>
    /// 发送图片消息
    /// </summary>
    /// <param name="image">图片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> ImageMessage(WeComImage image)
    {
        // 消息类型
        var msgtype = WeComMsgTypeEnum.Image.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, image });
        return result;
    }

    /// <summary>
    /// 发送图文消息
    /// </summary>
    /// <param name="news">图文</param>
    /// <returns></returns>
    public async Task<BaseResultDto> NewsMessage(WeComNews news)
    {
        // 消息类型
        var msgtype = WeComMsgTypeEnum.News.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, news });
        return result;
    }

    /// <summary>
    /// 发送文件消息
    /// </summary>
    /// <param name="file">文件</param>
    /// <returns></returns>
    public async Task<BaseResultDto> FileMessage(WeComFile file)
    {
        // 消息类型
        var msgtype = WeComMsgTypeEnum.File.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, file });
        return result;
    }

    /// <summary>
    /// 发送文本通知模版卡片消息
    /// </summary>
    /// <param name="templateCard">模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> TextNoticeMessage(WeComTemplateCardTextNotice templateCard)
    {
        // 消息类型
        var msgtype = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        templateCard.CardType = WeComTemplateCardType.TextNotice.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, template_card = templateCard });
        return result;
    }

    /// <summary>
    /// 发送图文展示模版卡片消息
    /// </summary>
    /// <param name="templateCard">模版卡片</param>
    /// <returns></returns>
    public async Task<BaseResultDto> NewsNoticeMessage(WeComTemplateCardNewsNotice templateCard)
    {
        // 消息类型
        var msgtype = WeComMsgTypeEnum.TemplateCard.GetEnumDescriptionByKey();
        templateCard.CardType = WeComTemplateCardType.NewsNotice.GetEnumDescriptionByKey();
        // 发送
        var result = await SendMessage(new { msgtype, template_card = templateCard });
        return result;
    }

    /// <summary>
    /// 微信执行上传文件
    /// </summary>
    /// <remarks>素材上传得到media_id，该media_id仅三天内有效，且只能对应上传文件的机器人可以使用</remarks>
    /// <remarks>文件大小在5B~20M之间</remarks>
    /// <returns></returns>
    public async Task<BaseResultDto> UploadkFile(FileStream fileStream)
    {
        Dictionary<string, string> headers = new()
        {
            { "filename",fileStream.Name },
            { "filelength",fileStream.Length.ToString() },
        };
        // 发起请求，上传地址
        var result = await _httpPolly.PostAsync<WeComResultInfoDto>(HttpEnum.Common, _fileUrl, fileStream, headers);
        // 包装返回信息
        if (result != null)
        {
            if (result.ErrCode == 0 || result.ErrMsg == "ok")
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
                return BaseResultDto.BadRequest("上传失败");
            }
        }
        return BaseResultDto.InternalServerError();
    }

    /// <summary>
    /// 微信执行发送消息
    /// </summary>
    /// <param name="objSend"></param>
    /// <returns></returns>
    private async Task<BaseResultDto> SendMessage(object objSend)
    {
        // 发送对象
        var sendMessage = objSend.SerializeToJson();
        // 发起请求，发送消息地址
        var result = await _httpPolly.PostAsync<WeComResultInfoDto>(HttpEnum.Common, _messageUrl, sendMessage);
        // 包装返回信息
        if (result != null)
        {
            if (result.ErrCode == 0 || result.ErrMsg == "ok")
            {
                return BaseResultDto.Success("发送成功");
            }
            else
            {
                return BaseResultDto.BadRequest("发送失败");
            }
        }
        return BaseResultDto.InternalServerError();
    }
}