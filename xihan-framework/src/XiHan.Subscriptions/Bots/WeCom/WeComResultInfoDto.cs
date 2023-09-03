#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:WeComResultInfoDto
// Guid:c6e6bf2a-d8d5-40f3-8834-019cc8ae4b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-11-08 下午 08:49:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json.Serialization;

namespace XiHan.Subscriptions.Bots.WeCom;

/// <summary>
/// 结果信息
/// </summary>
public class WeComResultInfoDto
{
    /// <summary>
    /// 结果代码
    /// 成功 0
    /// </summary>
    [JsonPropertyName("errcode")]
    public int ErrCode { get; set; }

    /// <summary>
    /// 结果消息
    /// 成功 ok
    /// </summary>
    [JsonPropertyName("errmsg")]
    public string ErrMsg { get; set; } = string.Empty;

    /// <summary>
    /// 媒体文件类型，分别有图片(image)、语音(voice)、视频(video)，普通文件(file)
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