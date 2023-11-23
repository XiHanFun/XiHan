#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogArticleCategory
// Guid:73eb779d-74f7-40ad-a7bd-79617d20c4f2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:22:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Blogs;

/// <summary>
/// 博客文章分类表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable]
public class BlogArticleCategory : BaseModifyEntity
{
    /// <summary>
    /// 父级分类
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 文章总数
    /// </summary>
    [SugarColumn]
    public int ArticleCount { get; set; }

    /// <summary>
    /// 分类描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 博客文章
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.ManyToMany, nameof(BlogArticleCategoryTieup.CategoryId), nameof(BlogArticleCategoryTieup.ArticleId))]
    public List<BlogArticle> Articles { get; set; } = new();
}