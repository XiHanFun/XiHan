// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccount
// Guid:5c92c656-8955-4343-8e6f-7ba028f1eab4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:30:03
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户账户表
/// </summary>
public class UserAccount : BaseEntity
{
    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮件
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string UserEmail { get; set; } = string.Empty;

    /// <summary>
    /// 用户密码（MD5加密）
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(64)")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)")]
    public string AvatarPath { get; set; } = @"/Images/Accounts/Avatar/defult.png";

    /// <summary>
    /// 用户昵称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", IsNullable = true)]
    public string? NickName { get; set; }

    /// <summary>
    /// 用户签名
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true)]
    public string? Signature { get; set; }

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? Gender { get; set; }

    /// <summary>
    /// 用户地址
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true)]
    public string? Address { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 注册Ip
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string? RegisterIp { get; set; }

    /// <summary>
    /// 上次登录日期
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? LastLoginTime { get; set; }
}