#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:LarkResultInfoDto
// Guid:c6e6bf2a-d8d5-40f3-8834-019cc8ae4b28
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-11-08 下午 08:49:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace XiHan.Subscriptions.Robots.Lark;

/// <summary>
/// 结果信息
/// </summary>
public class LarkResultInfoDto
{
    /// <summary>
    /// 结果代码
    /// 成功 0
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// 结果消息
    /// 成功 success
    /// </summary>
    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    /// <summary>
    /// 数据
    /// </summary>
    [JsonPropertyName("data")]
    public object Data { get; set; } = string.Empty;
}

/// <summary>
/// 结果代码
/// </summary>
public enum LarkResultErrCodeEnum
{
    /// <summary>
    /// 请求体格式错误，请求体内容格式是否与各消息类型的示例代码一致，请求体大小不能超过20k
    /// </summary>
    [Description("请求体格式错误，请求体内容格式是否与各消息类型的示例代码一致，请求体大小不能超过20k")]
    BadRequest = 9499,

    /// <summary>
    /// 签名校验失败，请排查以下原因：
    /// 1、时间戳距发送时已超过 1 小时，签名已过期；
    /// 2、服务器时间与标准时间有比较大的偏差，导致签名过期。请注意检查、校准你的服务器时间；
    /// 3、签名不匹配，校验不通过。
    /// </summary>
    [Description("签名校验失败，请排查以下原因：1、时间戳距发送时已超过 1 小时，签名已过期；2、服务器时间与标准时间有比较大的偏差，导致签名过期。请注意检查、校准你的服务器时间；3、签名不匹配，校验不通过。")]
    SignMatchFailOrTimestampIsNotWithinOneHourFromCurrentTime = 19021,

    /// <summary>
    /// IP校验失败
    /// </summary>
    [Description("IP校验失败")] IpNotAllowed = 19022,

    /// <summary>
    /// 自定义关键词校验失败
    /// </summary>
    [Description("自定义关键词校验失败")] KeyWordsNotFound = 19024
}