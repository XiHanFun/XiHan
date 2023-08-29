#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogComment
// Guid:60383ed1-8cd3-43d1-85e8-8b3dc45cdc7e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:25:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Blogs;

/// <summary>
/// 博客评论表
/// </summary>
/// <remarks>记录新增，修改，删除，审核，状态信息</remarks>
[SugarTable(TableName = "Blog_Comment")]
public class BlogComment : BaseEntity
{
    /// <summary>
    /// 父级评论
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 所属文章
    /// </summary>
    public long ArticleId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [SugarColumn(ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 评论点赞数
    /// </summary>
    public int PollCount { get; set; }

    /// <summary>
    /// 是否置顶 是(true)否(false)，只能置顶没有父级评论的项
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 评论者Ip
    /// </summary>
    [SugarColumn(Length = 128, IsNullable = true)]
    public string? Ip { get; set; }
}