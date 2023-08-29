#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RUserAccountRoleDto
// Guid:8a96fb06-2712-4560-9c8d-e4493e97e557
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-03 下午 07:10:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.ViewModels.Bases.Results;
using ZhaiFanhuaBlog.ViewModels.Roots;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// RUserAccountRoleDto
/// </summary>
public class RUserAccountRoleDto : BaseResultFieldDto
{
    /// <summary>
    /// 用户账户
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 系统角色
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 用户账户
    /// </summary>
    public virtual IEnumerable<RUserAccountDto>? UserAccounts { get; set; }

    /// <summary>
    /// 系统角色
    /// </summary>
    public virtual IEnumerable<RRootRoleDto>? RootRoles { get; set; }
}