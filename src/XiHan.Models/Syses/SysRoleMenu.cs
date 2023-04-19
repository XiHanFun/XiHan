#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysRoleMenu
// long:47b72b2e-41ff-4c0a-be2f-35c1c48641cf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-04 下午 01:36:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统角色菜单关联表
/// </summary>
[SugarTable(TableName = "Sys_Role_Menu")]
public class SysRoleMenu : BaseDeleteEntity
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 系统角色
    /// </summary>
    public long RoleId { get; set; }
}