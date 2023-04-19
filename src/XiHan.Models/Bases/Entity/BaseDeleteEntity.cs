#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseDeleteEntity
// long:1814189a-40ec-447b-95ad-8d77a973df7e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:06:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entity;

/// <summary>
/// 删除基类，含主键，创建，修改
/// </summary>
public abstract class BaseDeleteEntity : BaseModifyEntity
{
    /// <summary>
    /// 是否已经软删除
    /// </summary>
    [SugarColumn(ColumnDescription = "是否已经软删除")]
    public virtual bool IsSoftDeleted { get; set; } = false;

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <remarks>插入不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除用户")]
    public virtual long? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    /// <remarks>插入不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "删除时间")]
    public virtual DateTime? DeletedTime { get; set; }
}