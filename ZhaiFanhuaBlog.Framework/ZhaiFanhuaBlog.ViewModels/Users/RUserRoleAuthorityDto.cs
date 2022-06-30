// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RUserRoleAuthorityDto
// Guid:6a7a2b89-3373-4fc3-8836-c40cf08db2d6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-01 上午 03:31:07
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// RUserRoleAuthorityDto
/// </summary>
public class RUserRoleAuthorityDto : BaseEntity
{
    /// <summary>
    /// 角色
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 权限
    /// </summary>
    public Guid AuthorityId { get; set; }

    /// <summary>
    /// 权限类型（0:可访问，1:可授权）
    /// </summary>
    public int AuthorityType { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    public virtual IEnumerable<UserRole>? UserRoles { get; set; }

    /// <summary>
    /// 用户权限
    /// </summary>
    public virtual IEnumerable<UserAuthority>? UserAuthorities { get; set; }
}