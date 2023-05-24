﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysPermissionData
// Guid:7350f0eb-f8b3-4d81-ac4f-ca3d9adfd884
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 02:44:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 系统权限数据关联表(为某权限分配数据)
/// </summary>
/// <remarks>记录创建，修改信息</remarks>
[SugarTable(TableName = "Sys_Permission_Data")]
public class SysPermissionData : BaseModifyEntity
{
    /// <summary>
    /// 所属用户
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 所属角色
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? RoleId { get; set; }

    /// <summary>
    /// 资源编码
    /// 用于在系统中进行权限控制，表示一个具体的功能模块、操作按钮、数据字段等权限资源
    /// </summary>
    [SugarColumn(Length = 100)]
    public string ResourceCode { get; set; } = string.Empty;

    /// <summary>
    /// 操作编码
    /// </summary>
    [SugarColumn(Length = 100)]
    public string OperationCode { get; set; } = string.Empty;
}