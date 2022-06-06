// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRoleAuthority
// Guid:73293770-9bdc-4646-a03f-fba5cd908868
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:51:28
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户角色权限关联表
/// </summary>
public class UserRoleAuthority : BaseEntity
{
    /// <summary>
    /// 角色
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 权限
    /// </summary>
    public Guid AuthorityId { get; set; }
}