#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RRootRoleDto
// Guid:41ceac31-a441-4415-94c4-b56605d7de75
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 下午 11:47:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// RRootRoleDto
/// </summary>
public class RRootRoleDto : BaseResultFieldDto
{
    /// <summary>
    /// 父级角色
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 角色描述
    /// </summary>
    public string? Description { get; set; }
}