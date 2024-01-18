#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseTenantIdEntity
// Guid:806ff1b8-b9d1-4ff5-b212-f42c7fdaf23a
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:29:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Bases.Abstracts;

namespace XiHan.Domain.Bases.Entities;

/// <summary>
/// 租户实体基类
/// </summary>
public class BaseTenantIdEntity : ITenantIdEntity<long>
{
    /// <summary>
    /// 租户标识
    /// </summary>
    [SugarColumn(ColumnDescription = "租户标识", IsOnlyIgnoreUpdate = true)]
    public virtual long TenantId { get; set; }
}