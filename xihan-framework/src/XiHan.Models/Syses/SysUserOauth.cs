#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserOauth
// Guid:869f7f82-aef4-4c83-a012-d206c6854ae3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 用户三方登录授权表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SystemTable]
[SugarTable(TableName = "Sys_User_Oauth")]
public class SysUserOauth : BaseCreateEntity
{
    /// <summary>
    /// 三方登陆类型 weibo、qq、wechat 等
    /// OauthTypeEnum
    /// </summary>
    public int OauthType { get; set; }

    /// <summary>
    /// 三方 uid 、openid 等
    /// </summary>
    [SugarColumn(Length = 128)]
    public string OauthId { get; set; } = string.Empty;

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

    #region 账号信息

    /// <summary>
    /// 用户账号
    /// </summary>
    [SugarColumn(Length = 64)]
    public virtual string Account { get; set; } = string.Empty;

    /// <summary>
    /// 用户昵称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(Length = 256)]
    public string AvatarPath { get; set; } = string.Empty;

    /// <summary>
    /// 签名
    /// </summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? Signature { get; set; }

    #endregion

    #region 基本信息

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 64)]
    public virtual string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 性别
    /// 男(true)女(false)
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? Gender { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 32)]
    public string? Phone { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Address { get; set; }

    #endregion
}