// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseStateEntity
// Guid:d7e76ed6-1892-45f7-9cef-6541f17d339f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:08:47
// ----------------------------------------------------------------

using SqlSugar;

namespace ZhaiFanhuaBlog.Models.Bases;

/// <summary>
/// 状态基类，含主键，创建，修改，删除
/// </summary>
public abstract class BaseStateEntity<Tkey> : BaseAuditEntity<Guid>
{
    /// <summary>
    /// 类型代码
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public virtual string TypeKey { get; set; } = string.Empty;

    /// <summary>
    /// 状态代码 正常值(1)
    /// </summary>
    public virtual int StateKey { get; set; }
}