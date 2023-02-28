#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseAuditEntity
// Guid:850f0f6f-57bf-4149-b16e-cbf88f2ae088
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:07:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entity;

/// <summary>
/// 审核基类，含主键，创建，修改，删除
/// </summary>
public abstract class BaseAuditEntity<TKey> : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 审核用户
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "审核用户")]
    public virtual Guid? AuditId { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "审核时间")]
    public virtual DateTime? AuditTime { get; set; }
}