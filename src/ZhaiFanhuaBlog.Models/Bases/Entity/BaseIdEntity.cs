#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseIdEntity
// Guid:206c95b0-6e6e-49b4-b1d4-a5862c6c93c4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-04-26 下午 04:37:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Interface;

namespace ZhaiFanhuaBlog.Models.Bases.Entity;

/// <summary>
/// 主键基类
/// </summary>
public abstract class BaseIdEntity<TKey> : IBaseEntity<Guid>
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键标识")]
    public virtual Guid BaseId { get; set; } = new Guid();
}