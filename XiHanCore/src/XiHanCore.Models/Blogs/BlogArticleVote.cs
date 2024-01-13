#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogArticleVote
// Guid:2fcf240c-8218-497c-aae9-83f9c5791dfe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:29:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.ComponentModel;
using XiHanCore.Models.Bases.Entities;

namespace XiHanCore.Models.Blogs;

/// <summary>
/// 博客文章赞踩表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable]
public class BlogArticleVote : BaseCreateEntity
{
    /// <summary>
    /// 所属文章
    /// </summary>
    [SugarColumn]
    public long ArticleId { get; set; }

    /// <summary>
    /// 赞或踩
    /// </summary>
    [SugarColumn]
    public VoteTypeEnum VoteType { get; set; }
}

/// <summary>
/// 赞或踩类型
/// </summary>
public enum VoteTypeEnum
{
    /// <summary>
    /// 点赞
    /// </summary>
    [Description("点赞")] UpVote = 0,

    /// <summary>
    /// 点踩
    /// </summary>
    [Description("点踩")] DownVote = 1
}