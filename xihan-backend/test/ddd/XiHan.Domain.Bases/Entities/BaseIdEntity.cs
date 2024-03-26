#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseIdEntity
// Guid:4d83ab9f-40ec-4d8e-b63e-db06932921fb
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:26:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Domain.Bases.Entities.Interfaces;

namespace XiHan.Domain.Bases.Entities;

/// <summary>
/// 主键抽象类
/// </summary>
public abstract class BaseIdEntity : IBaseId<long>
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDescription = "主键标识")]
    public virtual long BaseId { get; set; }
}