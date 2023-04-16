#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseModifyEntity
// long:8d723648-ecdb-4669-99d7-367755791118
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:04:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace XiHan.Models.Bases.Entity;

/// <summary>
/// 修改基类，含主键，创建
/// </summary>
public abstract class BaseModifyEntity : BaseCreateEntity
{
    /// <summary>
    /// 修改用户
    /// </summary>
    /// <remarks>插入不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改用户")]
    public virtual long? ModifiedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    /// <remarks>插入不会有此字段</remarks>
    [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true, ColumnDescription = "修改时间")]
    public virtual DateTime? ModifiedTime { get; set; }
}