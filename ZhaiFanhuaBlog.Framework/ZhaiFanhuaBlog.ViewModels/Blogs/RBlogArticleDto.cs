// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RBlogArticleDto
// Guid:20a1043b-631d-4b79-a976-48b8289c0e1b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 上午 02:38:26
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// RBlogArticleDto
/// </summary>
public class RBlogArticleDto : RBaseDto
{
    /// <summary>
    /// 文章作者
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 文章分类
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章概要
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    public string TheContent { get; set; } = string.Empty;

    /// <summary>
    /// 阅读数量
    /// </summary>
    public int ReadCount { get; set; } = 0;

    /// <summary>
    /// 点赞数量
    /// </summary>
    public int PollCount { get; set; } = 0;

    /// <summary>
    /// 评论数量
    /// </summary>
    public int CommentCount { get; set; } = 0;

    /// <summary>
    /// 是否置顶 是(true)否(false)
    /// </summary>
    public bool IsTop { get; set; } = false;

    /// <summary>
    /// 是否精华 是(true)否(false)
    /// </summary>
    public bool IsEssence { get; set; } = false;

    /// <summary>
    /// 是否是转发文章 是(true)否(false)
    /// </summary>
    public bool IsForward { get; set; } = false;

    /// <summary>
    /// 转发文章链接
    /// </summary>
    public string? ForwardUrl { get; set; }
}