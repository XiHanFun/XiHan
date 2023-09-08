#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysRoleOrganization
// Guid:5a3679ca-996c-4a89-b2e8-830406ce55c8
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-25 上午 11:38:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Attributes;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统角色机构关联表(为某角色分配机构)
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysRoleOrganization : BaseModifyEntity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [SugarColumn]
    public long RoleId { get; set; }

    /// <summary>
    /// 系统机构
    /// </summary>
    [SugarColumn]
    public long OrganizationId { get; set; }
}