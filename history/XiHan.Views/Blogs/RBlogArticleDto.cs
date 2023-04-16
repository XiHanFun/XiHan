#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RBlogArticleDto
// Guid:20a1043b-631d-4b79-a976-48b8289c0e1b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 上午 02:38:26
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// RBlogArticleDto
/// </summary>
public class RBlogArticleDto : BaseResultFieldDto
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
    public string ArtTitle { get; set; } = string.Empty;

    /// <summary>
    /// 文章概要
    /// </summary>
    public string? ArtSummary { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    public string ArtContent { get; set; } = string.Empty;

    /// <summary>
    /// 阅读数量
    /// </summary>
    public int ReadCount { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    public int PollCount { get; set; }

    /// <summary>
    /// 评论数量
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// 是否置顶 是(true)否(false)
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 是否精华 是(true)否(false)
    /// </summary>
    public bool IsEssence { get; set; }

    /// <summary>
    /// 是否是转发文章 是(true)否(false)
    /// </summary>
    public bool IsForward { get; set; }

    /// <summary>
    /// 转发文章链接
    /// </summary>
    public string? ForwardUrl { get; set; }
}