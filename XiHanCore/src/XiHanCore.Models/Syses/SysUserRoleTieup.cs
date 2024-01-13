#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserRoleTieup
// Guid:4bd03482-a95a-4c4e-a544-6e89ecf7c275
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-09 下午 05:14:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Attributes;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Syses;

/// <summary>
/// 系统用户角色关联表(为某用户分配角色)
/// </summary>
/// <remarks>记录新增，修改息</remarks>
[SugarTable, SystemTable]
public class SysUserRoleTieup : BaseModifyEntity
{
    /// <summary>
    /// 用户账户
    /// </summary>
    [SugarColumn]
    public long UserId { get; init; }

    /// <summary>
    /// 系统角色
    /// </summary>
    [SugarColumn]
    public long RoleId { get; init; }
}