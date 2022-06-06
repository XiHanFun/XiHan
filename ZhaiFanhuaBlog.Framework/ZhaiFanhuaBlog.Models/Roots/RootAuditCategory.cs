// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAuditCategory
// Guid:52830965-a1e7-4d56-b98e-2582f19d22d8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:48:09
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 审核分类表
/// </summary>
public class RootAuditCategory : BaseEntity
{
    /// <summary>
    /// 父级审核分类
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// 审核分类名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string? Name { get; set; } = null;

    /// <summary>
    /// 审核分类描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? Description { get; set; } = null;

    /// <summary>
    /// 审核等级
    /// </summary>
    public int Tier { get; set; }
}