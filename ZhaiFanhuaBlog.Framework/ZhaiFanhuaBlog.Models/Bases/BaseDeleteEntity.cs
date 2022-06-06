// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseDeleteEntity
// Guid:1814189a-40ec-447b-95ad-8d77a973df7e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:06:13
// ----------------------------------------------------------------

using SqlSugar;

namespace ZhaiFanhuaBlog.Models.Bases;

/// <summary>
/// 删除基类，含主键，创建，修改
/// </summary>
public abstract class BaseDeleteEntity<Tkey> : BaseModifyEntity<Guid>
{
    /// <summary>
    /// 删除用户
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public virtual Guid? DeleteId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public virtual DateTime? DeleteTime { get; set; } = null;
}