#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseModifiedEntity
// Guid:481dc3b6-686a-4590-b871-779a31ea3f82
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:06:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Core.Entities.Abstracts;

namespace XiHan.Domain.Core.Entities;

/// <summary>
/// 修改抽象类，含主键，新增
/// </summary>
public abstract class BaseModifiedEntity : BaseCreatedEntity, IHasModified<long?>
{
    /// <summary>
    /// 修改用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改用户主键")]
    public virtual long? ModifiedId { get; private set; }

    /// <summary>
    /// 修改用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "修改用户名称")]
    public virtual string? ModifiedBy { get; private set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改时间")]
    public virtual DateTime? ModifiedTime { get; private set; }
}