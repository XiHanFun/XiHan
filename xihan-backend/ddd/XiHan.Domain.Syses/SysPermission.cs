#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPermission
// Guid:8b190341-c474-4974-961f-895c2c6a831d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:45:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Common.Shared.Attributes;
using XiHan.Domain.Bases.Entities;
using XiHan.Domain.Syses.Enums;

namespace XiHan.Domain.Syses;

/// <summary>
/// 系统权限表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysPermission : BaseModifiedEntity
{
    /// <summary>
    /// 父级权限
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 权限编码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 权限名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型
    /// </summary>
    [SugarColumn]
    public PermissionTypeEnum PermissionType { get; set; }

    /// <summary>
    /// 权限排序
    /// </summary>
    [SugarColumn]
    public int SortOrder { get; set; }

    /// <summary>
    /// 权限描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }
}