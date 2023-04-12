#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:WeChatResultInfoDto
// Guid:c6e6bf2a-d8d5-40f3-8834-019cc8ae4b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 下午 08:49:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json.Serialization;

namespace XiHan.Utils.Messages.WeChat;

/// <summary>
/// 结果信息
/// </summary>
public class WeChatResultInfoDto
{
    /// <summary>
    /// 结果代码 成功 0
    /// </summary>
    [JsonPropertyName("errcode")]
    public int ErrCode { get; set; }

    /// <summary>
    /// 结果消息 成功 ok
    /// </summary>
    [JsonPropertyName("errmsg")]
    public string ErrMsg { get; set; } = string.Empty;

    /// <summary>
    /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 媒体文件上传后获取的唯一标识，3天内有效
    /// </summary>
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; } = string.Empty;

    /// <summary>
    /// 媒体文件上传时间戳
    /// </summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = string.Empty;
}

/// <summary>
/// 文件上传返回结果
/// </summary>
public record WeChatUploadResultDto(string Message, string MediaId);