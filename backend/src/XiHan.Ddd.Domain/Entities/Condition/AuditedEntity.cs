#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AuditedEntity
// Guid:0dac25ea-e09c-4ffb-828f-dd039c435781
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/26 7:09:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Ddd.Domain.Entities.Abstracts;

namespace XiHan.Ddd.Domain.Entities.Condition;

/// <summary>
/// 审核抽象类，含主键，新增，修改，删除
/// </summary>
public abstract class AuditedEntity : DeletedEntity, IHasAudited<long?>
{
    /// <summary>
    /// 审核用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核用户主键")]
    public virtual long? AuditedId { get; private set; }

    /// <summary>
    /// 审核用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "审核用户名称")]
    public virtual string? AuditedBy { get; private set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核时间")]
    public virtual DateTime? AuditedTime { get; private set; }
}