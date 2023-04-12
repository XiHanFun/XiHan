#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DingTalkResultInfoDto
// Guid:c6e6bf2a-d8d5-40f3-8834-019cc8ae4b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 下午 08:49:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Text.Json.Serialization;

namespace XiHan.Utils.Messages.DingTalk;

/// <summary>
/// 结果信息
/// </summary>
public class DingTalkResultInfoDto
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
}