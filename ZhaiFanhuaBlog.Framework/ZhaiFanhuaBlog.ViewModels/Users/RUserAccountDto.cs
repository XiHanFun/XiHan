// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RUserAccountDto
// Guid:58a3df51-91bb-434e-b83e-32f9e21e41a1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-03 上午 03:37:16
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.ViewModels.Bases.Fields;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// RUserAccountDto
/// </summary>
public class RUserAccountDto : BaseFieldDto
{
    /// <summary>
    /// 用户名称
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 电子邮件
    /// </summary>
    public string? UserEmail { get; set; }

    /// <summary>
    /// 头像路径
    /// </summary>
    public string? AvatarPath { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 用户签名
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    public bool? Gender { get; set; }

    /// <summary>
    /// 用户地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 注册Ip地址
    /// </summary>
    public string? RegisterIp { get; set; }

    /// <summary>
    /// 上次登录日期
    /// </summary>
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 系统角色
    /// </summary>
    public virtual List<RootRole>? RootRoles { get; set; }
}