#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PostArticle
// long:dbe0832e-7ed3-41ff-bda4-2a2206174fae
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:19:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Posts;

/// <summary>
/// 文章表
/// </summary>
[SugarTable(TableName = "Post_Article")]
public class PostArticle : BaseEntity
{
    /// <summary>
    /// 文章别名
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 文章标题
    /// </summary>
    [SugarColumn(Length = 100)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章关键词
    /// </summary>
    [SugarColumn(Length = 500)]
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// 文章概要
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? Summary { get; set; }

    /// <summary>
    /// 文章缩略图
    /// </summary>
    [SugarColumn(Length = 500)]
    public string Thumbnail { get; set; } = string.Empty;

    /// <summary>
    /// 文章内容
    /// </summary>
    [SugarColumn(ColumnDataType = "text")]
    public string Content { get; set; } = string.Empty;

    #region 文章设置

    /// <summary>
    /// 文章分类
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 发布状态
    /// PubStatusEnum
    /// </summary>
    public int PubStatus { get; set; }

    /// <summary>
    /// 公开类型
    /// ExposedTypeEnum
    /// </summary>
    public int ExposedType { get; set; }

    /// <summary>
    /// 私密密码（MD5加密）
    /// </summary>
    [SugarColumn(Length = 64, IsNullable = true)]
    public string? Password { get; set; }

    /// <summary>
    /// 文章来源类型
    /// SourceTypeEnum
    /// </summary>
    public int SourceType { get; set; }

    /// <summary>
    /// 转载链接
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string? ReprintUrl { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComment { get; set; } = true;

    /// <summary>
    /// 是否置顶 是(true)否(false)
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 是否精华 是(true)否(false)
    /// </summary>
    public bool IsEssence { get; set; }

    #endregion 文章设置

    #region 文章统计

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

    #endregion 文章统计
}