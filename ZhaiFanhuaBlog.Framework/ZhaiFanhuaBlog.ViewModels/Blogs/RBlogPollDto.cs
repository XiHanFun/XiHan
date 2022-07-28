// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RBlogPollDto
// Guid:9ecc57d1-d54c-4210-b463-ec730feb9056
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 10:47:35
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// RBlogPollDto
/// </summary>
public class RBlogPollDto : RBaseDto
{
    /// <summary>
    /// 点赞者
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 所属文章
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 赞(true)或踩(false)
    /// </summary>
    public bool IsPositive { get; set; } = true;
}