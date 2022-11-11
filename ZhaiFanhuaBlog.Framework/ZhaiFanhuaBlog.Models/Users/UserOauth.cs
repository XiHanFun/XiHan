﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserOauth
// Guid:869f7f82-aef4-4c83-a012-d206c6854ae3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户第三方授权表
/// </summary>
public class UserOauth : BaseEntity
{
    /// <summary>
    /// 所属用户
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 第三方登陆类型 weibo、qq、wechat 等
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string OauthType { get; set; } = string.Empty;

    /// <summary>
    /// 第三方 uid 、openid 等
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string OauthId { get; set; } = string.Empty;

    /// <summary>
    /// QQ / 微信同一主体下 Unionid 相同
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string? UnionId { get; set; }

    /// <summary>
    /// 密码凭证 /access_token (目前更多是存储在缓存里)
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string Credential { get; set; } = string.Empty;
}