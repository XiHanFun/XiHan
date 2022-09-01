// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RBlogArticleTagDto
// Guid:02259c2a-9313-42d9-b7e8-cb7e0bf2aa2d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 11:25:43
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases.Fields;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// RBlogArticleTagDto
/// </summary>
public class RBlogArticleTagDto : BaseFieldDto
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