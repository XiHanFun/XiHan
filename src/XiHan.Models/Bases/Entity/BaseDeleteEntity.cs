#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseDeleteEntity
// Guid:1814189a-40ec-447b-95ad-8d77a973df7e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:06:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Interface;

namespace XiHan.Models.Bases.Entity;

/// <summary>
/// 删除基类，含主键，新增，修改
/// </summary>
public abstract class BaseDeleteEntity : BaseModifyEntity, ISoftDelete
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
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除用户名称")]
    public virtual string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除时间")]
    public virtual DateTime? DeletedTime { get; set; }
}