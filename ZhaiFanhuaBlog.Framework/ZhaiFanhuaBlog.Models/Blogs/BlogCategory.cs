// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCategory
// Guid:73eb779d-74f7-40ad-a7bd-79617d20c4f2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:22:23
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章分类表
/// </summary>
[SugarTable("BlogCategory", "文章分类表")]
public class BlogCategory : BaseEntity
{
    /// <summary>
    /// 父级分类
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "父级分类")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 分类用户
    /// </summary>
    [SugarColumn(ColumnDescription = "分类用户")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)", ColumnDescription = "分类名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 分类描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true, ColumnDescription = "分类描述")]
    public string? Description { get; set; }

    /// <summary>
    /// 文章总数
    /// </summary>
    [SugarColumn(ColumnDescription = "文章总数")]
    public int ArticleCount { get; set; } = 0;
}