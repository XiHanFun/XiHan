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
/// 审核类型表
/// </summary>
[SugarTable("RootAuditCategory", "审核分类表")]
public class RootAuditCategory : BaseEntity
{
    /// <summary>
    /// 父级审核分类
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "父级审核分类")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 审核分类名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", ColumnDescription = "审核分类名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 审核分类描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true, ColumnDescription = "审核分类描述")]
    public string? Description { get; set; }

    /// <summary>
    /// 审核等级
    /// </summary>
    [SugarColumn(ColumnDescription = "审核等级")]
    public int Tier { get; set; } = 1;
}