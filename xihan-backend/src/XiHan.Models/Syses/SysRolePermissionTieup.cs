#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysRolePermissionTieup
// Guid:73293770-9bdc-4646-a03f-fba5cd908868
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:51:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统角色权限关联表(为某角色分配权限)
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysRolePermissionTieup : BaseModifyEntity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [SugarColumn]
    public long RoleId { get; set; }

    /// <summary>
    /// 系统权限
    /// </summary>
    [SugarColumn]
    public long PermissionId { get; set; }
}