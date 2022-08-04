// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootMenuRole
// Guid:47b72b2e-41ff-4c0a-be2f-35c1c48641cf
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-04 下午 01:36:37
// ----------------------------------------------------------------

using SqlSugar;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统角色菜单表
/// </summary>
[SugarTable("RootMenu", "系统角色菜单表")]
public class RootMenuRole
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [SugarColumn(ColumnDescription = "父级菜单")]
    public Guid MenuId { get; set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    [SugarColumn(ColumnDescription = "用户角色")]
    public Guid RoleId { get; set; }
}