// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleAuthority
// Guid:73293770-9bdc-4646-a03f-fba5cd908868
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:51:28
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户角色权限关联表
/// </summary>
[SugarTable("UserRoleAuthority", "用户角色权限关联表")]
public class UserRoleAuthority : BaseEntity
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(ColumnDescription = "用户角色")]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 用户权限
    /// </summary>
    [SugarColumn(ColumnDescription = "用户权限")]
    public Guid AuthorityId { get; set; }

    /// <summary>
    /// 权限类型（0:可访问，1:可授权）
    /// </summary>
    [SugarColumn(ColumnDescription = "权限类型（0:可访问，1:可授权）")]
    public int AuthorityType { get; set; } = 0;

    [SugarColumn(IsIgnore = true)]
    public virtual IEnumerable<UserRole>? UserRoles { get; set; }

    [SugarColumn(IsIgnore = true)]
    public virtual IEnumerable<UserAuthority>? UserAuthorities { get; set; }
}