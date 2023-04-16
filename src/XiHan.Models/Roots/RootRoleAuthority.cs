#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootRoleAuthority
// long:73293770-9bdc-4646-a03f-fba5cd908868
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:51:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Roots;

/// <summary>
/// 系统角色权限关联表
/// </summary>
[SugarTable(TableName = "RootRoleAuthority")]
public class RootRoleAuthority : BaseDeleteEntity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 系统权限
    /// </summary>
    public long AuthorityId { get; set; }

    /// <summary>
    /// 权限类型（0:可访问，1:可授权）
    /// </summary>
    public int Type { get; set; }
}