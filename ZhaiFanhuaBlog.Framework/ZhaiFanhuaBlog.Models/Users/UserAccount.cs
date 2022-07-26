// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccount
// Guid:5c92c656-8955-4343-8e6f-7ba028f1eab4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:30:03
// ----------------------------------------------------------------

using SqlSugar;
using System.Net;
using ZhaiFanhuaBlog.Models.Bases;
using ZhaiFanhuaBlog.Utils.Formats;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户账户表
/// </summary>
[SugarTable("UserAccount", "用户账户表")]
public class UserAccount : BaseEntity
{
    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", ColumnDescription = "用户名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮件
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", ColumnDescription = "电子邮件")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 用户密码（MD5加密）
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(64)", ColumnDescription = "用户密码（MD5加密）")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", ColumnDescription = "头像路径")]
    public string AvatarPath { get; set; } = @"/Images/Accounts/Avatar/defult.png";

    /// <summary>
    /// 用户昵称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", IsNullable = true, ColumnDescription = "用户昵称")]
    public string? NickName { get; set; }

    /// <summary>
    /// 用户签名
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true, ColumnDescription = "用户签名")]
    public string? Signature { get; set; }

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "用户性别 男(true)女(false)")]
    public bool? Gender { get; set; }

    /// <summary>
    /// 用户地址
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)", IsNullable = true, ColumnDescription = "用户地址")]
    public string? Address { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "出生日期")]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 上次登录日期
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "上次登录日期")]
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 注册Ip地址
    /// </summary>
    [SugarColumn(ColumnDataType = "varbinary(16)", IsNullable = true, ColumnDescription = "注册Ip地址")]
    public virtual byte[]? RegisterIp
    {
        get => IpFormatHelper.FormatIPAddressToByte(_RegisterIp);
        set => _RegisterIp = IpFormatHelper.FormatByteToIPAddress(value);
    }

    private IPAddress? _RegisterIp;

    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public virtual IEnumerable<UserRole>? UserRoles { get; set; }
}