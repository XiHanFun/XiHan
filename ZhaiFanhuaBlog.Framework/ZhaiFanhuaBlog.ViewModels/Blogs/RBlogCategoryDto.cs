// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RBlogCategoryDto
// Guid:f55b699d-1985-46f0-85d5-f06cbcd64d39
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 上午 01:16:55
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// RBlogCategoryDto
/// </summary>
public class RBlogCategoryDto : RBaseDto
{
    /// <summary>
    /// 父级分类
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 分类描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 文章总数
    /// </summary>
    public int ArticleCount { get; set; } = 0;
}