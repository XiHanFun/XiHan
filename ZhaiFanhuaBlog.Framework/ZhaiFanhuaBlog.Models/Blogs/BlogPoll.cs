// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogPoll
// Guid:2fcf240c-8218-497c-aae9-83f9c5791dfe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:29:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章点赞表
/// </summary>
public class BlogPoll : BaseEntity
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