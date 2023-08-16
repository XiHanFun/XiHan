#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseModifyEntity
// Guid:eb3e5c59-302f-4507-92cb-7b1deab96380
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 下午 05:27:42
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entities;

/// <summary>
/// 修改基类，含主键，新增
/// </summary>
public abstract class BaseModifyEntity : BaseCreateEntity
{
    /// <summary>
    /// 修改用户主键
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改用户主键")]
    public virtual long? ModifiedId { get; set; }

    /// <summary>
    /// 修改用户名称
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, Length = 32, ColumnDescription = "修改用户名称")]
    public virtual string? ModifiedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改时间")]
    public virtual DateTime? ModifiedTime { get; set; } = DateTime.Now;
}