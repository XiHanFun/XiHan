#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysRolePermission
// long:73293770-9bdc-4646-a03f-fba5cd908868
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:51:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统角色权限关联表(为某角色分配权限)
/// </summary>
/// <remarks>记录创建，修改信息</remarks>
[SugarTable(TableName = "Sys_Role_Permission")]
public class SysRolePermission : BaseModifyEntity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 系统权限
    /// </summary>
    public long PermissionId { get; set; }
}