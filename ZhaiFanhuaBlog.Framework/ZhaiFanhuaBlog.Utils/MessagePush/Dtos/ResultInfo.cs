// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultInfo
// Guid:c6e6bf2a-d8d5-40f3-8834-019cc8ae4b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 下午 08:49:29
// ----------------------------------------------------------------

using Newtonsoft.Json;

namespace ZhaiFanhuaBlog.Utils.MessagePush.Dtos;

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
}