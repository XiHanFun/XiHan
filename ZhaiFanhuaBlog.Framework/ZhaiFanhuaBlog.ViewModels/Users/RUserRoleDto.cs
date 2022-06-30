// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RUserRoleDto
// Guid:41ceac31-a441-4415-94c4-b56605d7de75
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-30 下午 11:47:14
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// RUserRoleDto
/// </summary>
public class RUserRoleDto : RBaseDto
{
    /// <summary>
    /// 父级角色
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string? Name { get; set; } = null;

    /// <summary>
    /// 角色描述
    /// </summary>
    public string? Description { get; set; } = null;
}