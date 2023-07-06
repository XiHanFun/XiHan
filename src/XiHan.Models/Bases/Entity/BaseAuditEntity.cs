#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseAuditEntity
// Guid:850f0f6f-57bf-4149-b16e-cbf88f2ae088
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:07:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entity;

/// <summary>
/// 审核基类，含主键，新增，修改，删除
/// </summary>
public abstract class BaseAuditEntity : BaseDeleteEntity
{
    /// <summary>
    /// 审核用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核用户主键")]
    public virtual long? AuditedId { get; set; }

    /// <summary>
    /// 审核用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核用户名称")]
    public virtual string? AuditedBy { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "审核时间")]
    public virtual DateTime? AuditedTime { get; set; }
}