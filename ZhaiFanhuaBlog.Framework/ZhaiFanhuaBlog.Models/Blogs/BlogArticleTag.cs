// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleTag
// Guid:7f74e0d5-2c12-492c-b849-9651624ef6ae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:33:08
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章标签关联表
/// </summary>
public class BlogArticleTag : BaseEntity
{
    /// <summary>
    /// 创建者
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 关联文章
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 关联标签
    /// </summary>
    public Guid TagId { get; set; }
}