// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleTag
// Guid:7f74e0d5-2c12-492c-b849-9651624ef6ae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:33:08
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章标签关联表
/// </summary>
[SugarTable("BlogArticleTag", "文章标签关联表")]
public class BlogArticleTag : BaseEntity
{
    /// <summary>
    /// 创建者
    /// </summary>
    [SugarColumn(ColumnDescription = "创建用户")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 关联文章
    /// </summary>
    [SugarColumn(ColumnDescription = "关联文章")]
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 关联标签
    /// </summary>
    [SugarColumn(ColumnDescription = "关联标签")]
    public Guid TagId { get; set; }
}