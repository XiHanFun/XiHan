#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseStateEntity
// Guid:7aa6b236-5776-4b5d-9398-71a65117cec9
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:28:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Domain.Bases.Entities;

/// <summary>
/// 状态基类，含主键，新增，修改，删除，审核
/// </summary>
public abstract class BaseStateEntity : BaseAuditEntity
{
    /// <summary>
    /// 状态项
    /// </summary>
    [SugarColumn(IsIgnore = true, Length = 64, ColumnDescription = "状态项")]
    public virtual string? StateKey { get; init; }

    /// <summary>
    /// 状态值
    /// </summary>
    [SugarColumn(IsIgnore = true, Length = 64, ColumnDescription = "状态值")]
    public virtual string? StateValue { get; init; }
}