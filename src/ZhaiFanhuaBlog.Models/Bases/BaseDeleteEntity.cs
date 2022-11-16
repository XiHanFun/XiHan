#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseDeleteEntity
// Guid:1814189a-40ec-447b-95ad-8d77a973df7e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:06:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace ZhaiFanhuaBlog.Models.Bases;

/// <summary>
/// 删除基类，含主键，创建，修改
/// </summary>
public abstract class BaseDeleteEntity<Tkey> : BaseModifyEntity<Guid>
{
    /// <summary>
    /// 软删除锁（是否支持软删除：是为1，表示软删除；否为0，表示物理删除）
    /// </summary>
    [SugarColumn(ColumnDescription = "软删除锁（是否支持软删除：是为1，表示软删除；否为0，表示物理删除）")]
    public virtual bool SoftDeleteLock { get; set; } = true;

    /// <summary>
    /// 删除用户
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "删除用户")]
    public virtual Guid? DeleteId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "删除时间")]
    public virtual DateTime? DeleteTime { get; set; }
}