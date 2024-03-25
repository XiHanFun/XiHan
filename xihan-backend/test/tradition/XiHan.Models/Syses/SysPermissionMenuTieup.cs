#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPermissionMenuTieup
// Guid:47b72b2e-41ff-4c0a-be2f-35c1c48641cf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-08-04 下午 01:36:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统权限菜单关联表(为某权限分配菜单)
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysPermissionMenuTieup : BaseModifyEntity
{
    /// <summary>
    /// 系统权限
    /// </summary>
    [SugarColumn]
    public long PermissionId { get; set; }

    /// <summary>
    /// 系统菜单
    /// </summary>
    [SugarColumn]
    public long MenuId { get; set; }
}