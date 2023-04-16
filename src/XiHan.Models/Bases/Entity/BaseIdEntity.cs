#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseIdEntity
// long:206c95b0-6e6e-49b4-b1d4-a5862c6c93c4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-04-26 下午 04:37:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Interface;

namespace XiHan.Models.Bases.Entity;

/// <summary>
/// 主键基类
/// </summary>
public abstract class BaseIdEntity : IBaseEntity<long>
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "主键标识")]
    public virtual long BaseId { get; set; } = new();
}