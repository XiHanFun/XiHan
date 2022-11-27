#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticle
// Guid:dbe0832e-7ed3-41ff-bda4-2a2206174fae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:19:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章表
/// </summary>
public class BlogArticle : BaseEntity
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
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string ArtTitle { get; set; } = string.Empty;

    /// <summary>
    /// 文章概要
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(500)", IsNullable = true)]
    public string? ArtSummary { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    [SugarColumn(ColumnDataType = "text")]
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
    [SugarColumn(IsNullable = true)]
    public string? ForwardUrl { get; set; }

    /// <summary>
    /// 是否加密文章 是(true)否(false)
    /// </summary>
    public bool IsEncryption { get; set; }

    /// <summary>
    /// 加密文章密码（MD5加密）
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDataType = "nvarchar(64)")]
    public string? Password { get; set; }
}