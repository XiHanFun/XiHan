// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserAccountRole
// Guid:4bd03482-a95a-4c4e-a544-6e89ecf7c275
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 下午 05:14:15
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户账户角色关联表
/// </summary>
[SugarTable("UserAccountRole", "用户账户角色关联表")]
public class UserAccountRole : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 用户账户
    /// </summary>
    [SugarColumn(ColumnDescription = "用户账户")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(ColumnDescription = "用户角色")]
    public Guid RoleId { get; set; }

    [SugarColumn(IsIgnore = true)]
    public virtual IEnumerable<UserAccount>? UserAccounts { get; set; }

    [SugarColumn(IsIgnore = true)]
    public virtual IEnumerable<UserRole>? UserRoles { get; set; }
}