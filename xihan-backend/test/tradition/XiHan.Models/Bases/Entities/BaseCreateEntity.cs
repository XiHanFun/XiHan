#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseCreateEntity
// Guid:2907474d-7eef-489a-8ac4-6b3d72a0e5de
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:27:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entities;

/// <summary>
/// 新增基类，含主键
/// </summary>
public abstract class BaseCreateEntity : BaseIdEntity
{
    /// <summary>
    /// 新增用户主键
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增用户主键")]
    public virtual long? CreatedId { get; set; }

    /// <summary>
    /// 新增用户名称
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, Length = 32, ColumnDescription = "新增用户名称")]
    public virtual string? CreatedBy { get; set; }

    /// <summary>
    /// 新增时间
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SplitField]
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增时间")]
    public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
}