#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OAuthTypeEnum
// Guid:8d239538-9b29-49b6-bdd2-46f9f19a205a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-20 下午 03:27:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Syses.Enums;

/// <summary>
/// 开放授权协议类型
/// </summary>
public enum OAuthTypeEnum
{
    /// <summary>
    /// QQ
    /// </summary>
    [Description("QQ")] QQ = 1,

    /// <summary>
    /// 微信
    /// </summary>
    [Description("微信")] WeChat = 2,

    /// <summary>
    /// 支付宝
    /// </summary>
    [Description("支付宝")] Alipay = 3,

    /// <summary>
    /// Github
    /// </summary>
    [Description("Github")] Github = 4,

    /// <summary>
    /// Gitlab
    /// </summary>
    [Description("Gitlab")] Gitlab = 5,

    /// <summary>
    /// Gitee
    /// </summary>
    [Description("Gitee")] Gitee = 6
}