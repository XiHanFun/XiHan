#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseCreateEntity
// Guid:fdd5de1e-1868-40f7-8014-b2f6921db6e0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:02:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace ZhaiFanhuaBlog.Models.Bases.Entity;

/// <summary>
/// 创建基类，含主键
/// </summary>
public abstract class BaseCreateEntity<TKey> : BaseIdEntity<Guid>
{
    /// <summary>
    /// 创建用户
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "创建用户")]
    public virtual Guid? CreateId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "创建时间")]
    public virtual DateTime? CreateTime { get; set; } = DateTime.Now;
}