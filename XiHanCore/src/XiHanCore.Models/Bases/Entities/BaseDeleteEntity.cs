#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseDeleteEntity
// Guid:a6e08e1f-8ebe-4421-9a78-bb21efda05cf
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:28:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Filters;

namespace XiHanCore.Models.Bases.Entities;

/// <summary>
/// 删除基类，含主键，新增，修改
/// </summary>
public abstract class BaseDeleteEntity : BaseModifyEntity, ISoftDeleteFilter
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    [SugarColumn(ColumnDescription = "是否已删除")]
    public virtual bool IsDeleted { get; set; } = false;

    /// <summary>
    /// 删除用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除用户主键")]
    public virtual long? DeletedId { get; set; }

    /// <summary>
    /// 删除用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "删除用户名称")]
    public virtual string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除时间")]
    public virtual DateTime? DeletedTime { get; set; }
}