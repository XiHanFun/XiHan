// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultInfo
// Guid:c6e6bf2a-d8d5-40f3-8834-019cc8ae4b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 下午 08:49:29
// ----------------------------------------------------------------

using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.Utils.MessagePush.WeChat;

/// <summary>
/// 结果信息
/// </summary>
public class ResultInfo
{
    /// <summary>
    /// 结果代码 成功 0
    /// </summary>
    [JsonProperty(PropertyName = "errcode")]
    public string ErrCode { get; set; } = string.Empty;

    /// <summary>
    /// 结果消息 成功 ok
    /// </summary>
    [JsonProperty(PropertyName = "errmsg")]
    public string ErrMsg { get; set; } = string.Empty;

    /// <summary>
    /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 媒体文件上传后获取的唯一标识，3天内有效
    /// </summary>
    [JsonProperty(PropertyName = "media_id")]
    public string MediaId { get; set; } = string.Empty;

    /// <summary>
    /// 媒体文件上传时间戳
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public string CreatedAt { get; set; } = string.Empty;
}

/// <summary>
/// 文件上传返回结果
/// </summary>
public record WeChatUploadResult(string message, string mediaId);