#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPermissionResourceTieup
// Guid:7350f0eb-f8b3-4d81-ac4f-ca3d9adfd884
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 02:44:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Bases.Attributes;
using XiHan.Domain.Bases.Entities;

namespace XiHan.Domain.Syses;

/// <summary>
/// 系统权限资源关联表(为某权限分配资源)
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysPermissionResourceTieup : BaseModifiedEntity
{
    /// <summary>
    /// 所属权限
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long PermissionId { get; set; }

    /// <summary>
    /// 资源编码
    /// 用于在系统中进行权限控制，表示一个具体的功能模块、操作按钮、数据字段等权限资源
    /// </summary>
    [SugarColumn(Length = 64)]
    public string ResourceCode { get; set; } = string.Empty;

    /// <summary>
    /// 操作编码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string OperationCode { get; set; } = string.Empty;
}