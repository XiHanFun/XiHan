#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogArticleCommentPoll
// Guid:2fcf240c-8218-497c-aae9-83f9c5791dfe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:29:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Blogs;

/// <summary>
/// 博客文章评论赞踩表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable]
public class BlogArticleCommentPoll : BaseCreateEntity
{
    /// <summary>
    /// 所属评论
    /// </summary>
    [SugarColumn]
    public long CommentId { get; set; }

    /// <summary>
    /// 赞或踩 赞(true)踩(false)
    /// </summary>
    [SugarColumn]
    public bool IsPositive { get; set; } = true;
}