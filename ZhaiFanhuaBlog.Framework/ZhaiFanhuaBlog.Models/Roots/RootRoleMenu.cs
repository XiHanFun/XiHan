// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootRoleMenu
// Guid:47b72b2e-41ff-4c0a-be2f-35c1c48641cf
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-04 下午 01:36:37
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统角色菜单关联表
/// </summary>
[SugarTable("RootRoleMenu", "系统角色菜单关联表")]
public class RootRoleMenu : BaseEntity
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [SugarColumn(ColumnDescription = "父级菜单")]
    public Guid MenuId { get; set; }

    /// <summary>
    /// 系统角色
    /// </summary>
    [SugarColumn(ColumnDescription = "系统角色")]
    public Guid RoleId { get; set; }
}