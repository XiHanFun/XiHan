#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserOAuth
// Guid:869f7f82-aef4-4c83-a012-d206c6854ae3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;
using XiHanCore.Models.Syses.Enums;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 用户三方登录授权表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable, SystemTable]
public class SysUserOAuth : BaseCreateEntity
{
    /// <summary>
    /// 三方登陆类型 weibo、qq、wechat 等
    /// </summary>
    [SugarColumn]
    public OAuthTypeEnum OAuthType { get; set; }

    /// <summary>
    /// 三方 uid 、openid 等
    /// </summary>
    [SugarColumn(Length = 128)]
    public string OAuthId { get; set; } = string.Empty;

    /// <summary>
    /// QQ / 微信同一主体下 Union 相同
    /// </summary>
    [SugarColumn(Length = 128)]
    public string? UnionId { get; set; }

    /// <summary>
    /// 密码凭证 /access_token (目前更多是存储在缓存里)
    /// </summary>
    [SugarColumn(Length = 128)]
    public string Credential { get; set; } = string.Empty;
}