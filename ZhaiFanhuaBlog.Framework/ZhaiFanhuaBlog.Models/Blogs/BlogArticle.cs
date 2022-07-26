// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticle
// Guid:dbe0832e-7ed3-41ff-bda4-2a2206174fae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:19:55
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章表
/// </summary>
[SugarTable("BlogArticle", "文章表")]
public class BlogArticle : BaseEntity
{
    /// <summary>
    /// 文章作者
    /// </summary>
    [SugarColumn(ColumnDescription = "文章作者")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 文章分类
    /// </summary>
    [SugarColumn(ColumnDescription = "文章分类")]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", ColumnDescription = "文章标题")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章概要
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(500)", IsNullable = true, ColumnDescription = "文章概要")]
    public string? Summary { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    [SugarColumn(ColumnDataType = "text", ColumnDescription = "文章概要")]
    public string TheContent { get; set; } = string.Empty;

    /// <summary>
    /// 阅读数量
    /// </summary>
    [SugarColumn(ColumnDescription = "阅读数量")]
    public int ReadCount { get; set; } = 0;

    /// <summary>
    /// 点赞数量
    /// </summary>
    [SugarColumn(ColumnDescription = "点赞数量")]
    public int PollCount { get; set; } = 0;

    /// <summary>
    /// 评论数量
    /// </summary>
    [SugarColumn(ColumnDescription = "评论数量")]
    public int CommentCount { get; set; } = 0;

    /// <summary>
    /// 是否置顶 是(true)否(false)
    /// </summary>
    [SugarColumn(ColumnDescription = "是否置顶 是(true)否(false)")]
    public bool IsTop { get; set; } = false;

    /// <summary>
    /// 是否精华 是(true)否(false)
    /// </summary>
    [SugarColumn(ColumnDescription = "是否精华 是(true)否(false)")]
    public bool IsEssence { get; set; } = false;

    /// <summary>
    /// 是否是转发文章 是(true)否(false)
    /// </summary>
    [SugarColumn(ColumnDescription = "是否是转发文章 是(true)否(false)")]
    public bool IsForward { get; set; } = false;

    /// <summary>
    /// 转发文章链接
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "转发文章链接")]
    public string? ForwardUrl { get; set; }
}