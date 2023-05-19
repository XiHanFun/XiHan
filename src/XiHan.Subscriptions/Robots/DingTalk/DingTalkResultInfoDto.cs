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

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace XiHan.Subscriptions.Robots.DingTalk;

/// <summary>
/// 结果信息
/// </summary>
public class DingTalkResultInfoDto
{
    /// <summary>
    /// 结果代码
    /// </summary>
    [JsonPropertyName("errcode")]
    public int ErrCode { get; set; }

    /// <summary>
    /// 结果消息
    /// </summary>
    [JsonPropertyName("errmsg")]
    public string ErrMsg { get; set; } = string.Empty;
}

/// <summary>
/// 结果代码
/// </summary>
public enum DingTalkResultErrCodeEnum
{
    /// <summary>
    /// 消息校验未通过，请查看机器人的安全设置
    /// </summary>
    /// <remarks>
    /// keywords not in content 消息内容中不包含任何关键词
    /// invalid timestamp timestamp无效
    /// sign not match 签名不匹配
    /// ip X.X.X.X not in whitelist IP地址不在白名单
    /// </remarks>
    [Description("消息校验未通过，请查看机器人的安全设置")]
    MessageVerificationFailed = 310000,

    /// <summary>
    /// 群已被解散，请向其他群发消息
    /// </summary>
    [Description("群已被解散，请向其他群发消息")]
    GroupDisbanded = 400013,

    /// <summary>
    /// access_token不存在，请确认access_token拼写是否正确
    /// </summary>
    [Description("access_token不存在，请确认access_token拼写是否正确")]
    AccessTokenNotExist = 400101,

    /// <summary>
    /// 机器人已停用，请联系管理员启用机器人
    /// </summary>
    [Description("机器人已停用，请联系管理员启用机器人")]
    RobotDeactivated = 400102,

    /// <summary>
    /// 不支持的消息类型，请使用文档中支持的消息类型
    /// </summary>
    [Description("不支持的消息类型，请使用文档中支持的消息类型")]
    UnsupportedMessageType = 400105,

    /// <summary>
    /// 机器人不存在，请确认机器人是否在群中
    /// </summary>
    [Description("机器人不存在，请确认机器人是否在群中")]
    RobotNotExist = 400106,

    /// <summary>
    /// 发送速度太快而限流，请降低发送速度
    /// </summary>
    [Description("发送速度太快而限流，请降低发送速度")]
    SendingSpeedTooFast = 410100,

    /// <summary>
    /// 含有不安全的外链，请确认发送的内容合法
    /// </summary>
    [Description("含有不安全的外链，请确认发送的内容合法")]
    UnsafeOuterChain = 430101,

    /// <summary>
    /// 含有不合适的文本，请确认发送的内容合法
    /// </summary>
    [Description("含有不合适的文本，请确认发送的内容合法")]
    ContainsInappropriateText = 430102,

    /// <summary>
    /// 含有不合适的图片，请确认发送的内容合法
    /// </summary>
    [Description("含有不合适的图片，请确认发送的内容合法")]
    ContainsInappropriateImages = 430103,

    /// <summary>
    /// 含有不合适的内容，请确认发送的内容合法
    /// </summary>
    [Description("含有不合适的内容，请确认发送的内容合法")]
    ContainsInappropriateContent = 430104,
}