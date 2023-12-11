﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogArticle
// Guid:dbe0832e-7ed3-41ff-bda4-2a2206174fae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:19:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entities;
using XiHan.Models.Blogs.Enums;

namespace XiHan.Models.Blogs;

/// <summary>
/// 博客文章表
/// </summary>
/// <remarks>记录新增，修改，删除，审核，状态信息</remarks>
[SugarTable]
public class BlogArticle : BaseEntity
{
    /// <summary>
    /// 文章别名
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 文章标题
    /// </summary>
    [SugarColumn(Length = 256)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章关键词
    /// </summary>
    [SugarColumn(Length = 128)]
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// 文章概要
    /// </summary>
    [SugarColumn(Length = 512, IsNullable = true)]
    public string? Summary { get; set; }

    /// <summary>
    /// 封面地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string CoverUrl { get; set; } = string.Empty;

    /// <summary>
    /// 文章内容
    /// </summary>
    [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 编辑器类型
    /// </summary>
    [SugarColumn]
    public EditorTypeEnum EditorType { get; set; }

    /// <summary>
    /// 文章分类
    /// </summary>
    [SugarColumn]
    public long CategoryId { get; set; }

    /// <summary>
    /// 发布状态
    /// </summary>
    [SugarColumn]
    public PubStatusEnum PubStatus { get; set; }

    /// <summary>
    /// 公开类型
    /// </summary>
    [SugarColumn]
    public ExposedTypeEnum ExposedType { get; set; }

    /// <summary>
    /// 私密密码(MD5加密)
    /// </summary>
    [SugarColumn(Length = 64, IsNullable = true)]
    public string? Password { get; set; }

    /// <summary>
    /// 文章来源类型
    /// </summary>
    [SugarColumn]
    public SourceTypeEnum SourceType { get; set; }

    /// <summary>
    /// 转载链接
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? ReprintUrl { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    [SugarColumn]
    public bool IsAllowComment { get; set; } = true;

    /// <summary>
    /// 是否置顶 是(true)否(false)
    /// </summary>
    [SugarColumn]
    public bool IsTop { get; set; }

    /// <summary>
    /// 是否精华 是(true)否(false)
    /// </summary>
    [SugarColumn]
    public bool IsEssence { get; set; }

    /// <summary>
    /// 阅读数量
    /// </summary>
    [SugarColumn]
    public int ReadCount { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    [SugarColumn]
    public int VoteCount { get; set; }

    /// <summary>
    /// 点踩数量
    /// </summary>
    [SugarColumn]
    public int OpposeCount { get; set; }

    /// <summary>
    /// 评论数量
    /// </summary>
    [SugarColumn]
    public int CommentCount { get; set; }

    /// <summary>
    /// 发表者Ip
    /// </summary>
    [SugarColumn(Length = 128, IsNullable = true)]
    public string? Ip { get; set; }

    /// <summary>
    /// 文章分类
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.ManyToMany, nameof(BlogArticleCategoryTieup.ArticleId), nameof(BlogArticleCategoryTieup.CategoryId))]
    public List<BlogArticleCategory> Categories { get; set; } = [];

    /// <summary>
    /// 文章标签
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.ManyToMany, nameof(BlogArticleTagTieup.ArticleId), nameof(BlogArticleTagTieup.TagId))]
    public List<BlogArticleTag> Tags { get; set; } = [];

    /// <summary>
    /// 文章赞踩
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(BlogArticlePoll.ArticleId))]
    public List<BlogArticlePoll> Polls { get; set; } = [];

    /// <summary>
    /// 文章评论
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    [Navigate(NavigateType.OneToMany, nameof(BlogArticleComment.ArticleId))]
    public List<BlogArticleComment> Comments { get; set; } = [];
}