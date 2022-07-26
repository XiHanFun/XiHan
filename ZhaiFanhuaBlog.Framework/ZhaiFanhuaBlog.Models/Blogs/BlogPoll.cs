// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogPoll
// Guid:2fcf240c-8218-497c-aae9-83f9c5791dfe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:29:20
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章点赞表
/// </summary>
[SugarTable("BlogPoll", "文章点赞表")]
public class BlogPoll : BaseEntity
{
    /// <summary>
    /// 点赞者
    /// </summary>
    [SugarColumn(ColumnDescription = "点赞者")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 所属文章
    /// </summary>
    [SugarColumn(ColumnDescription = "所属文章")]
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 赞(true)或踩(false)
    /// </summary>
    [SugarColumn(ColumnDescription = "赞(true)或踩(false)")]
    public bool IsPositive { get; set; } = true;
}