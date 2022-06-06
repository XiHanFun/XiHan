// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentPoll
// Guid:6b883c6a-d2a5-45a8-b742-6ae9e0b40e75
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:28:01
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章评论点赞表
/// </summary>
public class BlogCommentPoll : BaseEntity
{
    /// <summary>
    /// 点赞者
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 所属评论
    /// </summary>
    public Guid CommentId { get; set; }

    /// <summary>
    /// 赞(true)或踩(false)
    /// </summary>
    public bool IsPositive { get; set; } = true;
}