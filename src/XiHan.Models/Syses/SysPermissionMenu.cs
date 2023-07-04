﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysPermissionMenu
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
/// 系统权限菜单关联表(为某权限分配菜单)
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable(TableName = "Sys_Permission_Menu")]
public class SysPermissionMenu : BaseModifyEntity
{
    /// <summary>
    /// 系统权限
    /// </summary>
    public long PermissionId { get; set; }

    /// <summary>
    /// 系统菜单
    /// </summary>
    public long MenuId { get; set; }
}