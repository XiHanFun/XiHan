// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserLogin
// Guid:30e63ed1-9f89-4676-8aa6-9521f6ab3d6d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:11:05
// ----------------------------------------------------------------

using SqlSugar;
using System.Net;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户登录表
/// </summary>
public class UserLogin : BaseEntity
{
    /// <summary>
    /// 所属用户
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 登录Ip地址
    /// </summary>
    [SugarColumn(ColumnDataType = "varbinary(16)")]
    public IPAddress? LoginIp { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", IsNullable = true)]
    public string? Agent { get; set; } = null;
}