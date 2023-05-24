#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:OauthTypeEnum
// Guid:8d239538-9b29-49b6-bdd2-46f9f19a205a
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-20 下午 03:27:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Syses.Enums;

/// <summary>
/// 开放授权协议类型
/// </summary>
public enum OauthTypeEnum
{
    /// <summary>
    /// QQ
    /// </summary>
    [Description("QQ")]
    QQ = 1,

    /// <summary>
    /// 微信
    /// </summary>
    [Description("微信")]
    WeChat = 2,

    /// <summary>
    /// 支付宝
    /// </summary>
    [Description("支付宝")]
    Alipay = 3,

    /// <summary>
    /// 微博
    /// </summary>
    [Description("微博")]
    WeiBo = 4,

    /// <summary>
    /// 钉钉
    /// </summary>
    [Description("钉钉")]
    DingTalk = 5,

    /// <summary>
    /// Github
    /// </summary>
    [Description("Github")]
    Github = 6,

    /// <summary>
    /// Gitee
    /// </summary>
    [Description("Gitee")]
    Gitee = 7,
}