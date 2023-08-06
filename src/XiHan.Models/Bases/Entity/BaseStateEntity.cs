#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseStateEntity
// Guid:d7e76ed6-1892-45f7-9cef-6541f17d339f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:08:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entity;

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