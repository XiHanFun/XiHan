#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PostCategory
// long:73eb779d-74f7-40ad-a7bd-79617d20c4f2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:22:23
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Posts;

/// <summary>
/// 文章分类表
/// </summary>
[SugarTable(TableName = "Post_Category")]
public class PostCategory : BaseEntity
{
    /// <summary>
    /// 父级分类
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [SugarColumn(Length = 10)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 分类描述
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string? Remark { get; set; }

    /// <summary>
    /// 文章总数
    /// </summary>
    public int ArticleCount { get; set; }
}