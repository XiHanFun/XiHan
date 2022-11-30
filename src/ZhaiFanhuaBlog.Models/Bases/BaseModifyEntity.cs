#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseModifyEntity
// Guid:8d723648-ecdb-4669-99d7-367755791118
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:04:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;

namespace ZhaiFanhuaBlog.Models.Bases;

/// <summary>
/// 修改基类，含主键，创建
/// </summary>
public abstract class BaseModifyEntity<Tkey> : BaseCreateEntity<Guid>
{
    /// <summary>
    /// 修改用户
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "修改用户")]
    public virtual Guid? ModifyId { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "修改时间")]
    public virtual DateTime? ModifyTime { get; set; }
}