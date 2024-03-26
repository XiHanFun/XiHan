#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseCreatedEntity
// Guid:63b3a58b-c142-454e-9940-83c424c2b21a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:05:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Core.Models.Entities.Abstracts;

namespace XiHan.Domain.Core.Models.Entities;

/// <summary>
/// 新增抽象类，含主键
/// </summary>
public abstract class BaseCreatedEntity : BaseEntity, IHasCreated<long?>
{
    /// <summary>
    /// 新增用户主键
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增用户主键")]
    public virtual long? CreatedId { get; private set; }

    /// <summary>
    /// 新增用户名称
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, Length = 32, ColumnDescription = "新增用户名称")]
    public virtual string? CreatedBy { get; private set; }

    /// <summary>
    /// 新增时间
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SplitField]
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增时间")]
    public virtual DateTime CreatedTime { get; private set; } = DateTime.Now;
}