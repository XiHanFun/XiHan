#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseCreateEntity
// long:fdd5de1e-1868-40f7-8014-b2f6921db6e0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:02:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entity;

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
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增用户名称")]
    public virtual string? CreatedBy { get; set; }

    /// <summary>
    /// 新增时间
    /// </summary>
    /// <remarks>修改不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreUpdate = true, ColumnDescription = "新增时间")]
    public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
}