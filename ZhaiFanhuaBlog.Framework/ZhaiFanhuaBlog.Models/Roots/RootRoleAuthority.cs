// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootRoleAuthority
// Guid:73293770-9bdc-4646-a03f-fba5cd908868
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:51:28
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统角色权限关联表
/// </summary>
public class RootRoleAuthority : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 系统权限
    /// </summary>
    public Guid AuthorityId { get; set; }

    /// <summary>
    /// 权限类型（0:可访问，1:可授权）
    /// </summary>
    public int AuthorityType { get; set; } = 0;
}