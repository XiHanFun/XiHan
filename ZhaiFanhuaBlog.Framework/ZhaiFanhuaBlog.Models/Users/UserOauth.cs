// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserOauth
// Guid:869f7f82-aef4-4c83-a012-d206c6854ae3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:14:54
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户第三方授权表
/// </summary>
[SugarTable("UserOauth", "用户第三方授权表")]
public class UserOauth : BaseEntity
{
    /// <summary>
    /// 所属用户
    /// </summary>
    [SugarColumn(ColumnDescription = "所属用户")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 第三方登陆类型 weibo、qq、wechat 等
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", ColumnDescription = "第三方登陆类型 weibo、qq、wechat 等")]
    public string OauthType { get; set; } = string.Empty;

    /// <summary>
    /// 第三方 uid 、openid 等
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", ColumnDescription = "第三方 uid 、openid 等")]
    public string OauthId { get; set; } = string.Empty;

    /// <summary>
    /// QQ / 微信同一主体下 Unionid 相同
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", ColumnDescription = "QQ / 微信同一主体下 Unionid 相同")]
    public string? UnionId { get; set; }

    /// <summary>
    /// 密码凭证 /access_token (目前更多是存储在缓存里)
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", ColumnDescription = "密码凭证 /access_token (目前更多是存储在缓存里)")]
    public string Credential { get; set; } = string.Empty;
}