#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BaseModifyEntity
// Guid:8d723648-ecdb-4669-99d7-367755791118
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 04:04:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entity;

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
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改用户名称")]
    public virtual string? ModifiedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    /// <remarks>新增不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改时间")]
    public virtual DateTime? ModifiedTime { get; set; } = DateTime.Now;
}