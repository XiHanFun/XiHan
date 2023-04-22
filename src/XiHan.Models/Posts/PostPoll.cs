﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PostPoll
// long:2fcf240c-8218-497c-aae9-83f9c5791dfe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:29:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.ComponentModel;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Posts;

/// <summary>
/// 文章点赞表
/// </summary>
/// <remarks>记录创建信息</remarks>
[SugarTable(TableName = "Post_Poll")]
public class PostPoll : BaseCreateEntity
{
    /// <summary>
    /// 点赞类型(文章或评论)
    /// PollTypeEnum
    /// </summary>
    public int PollType { get; set; }

    /// <summary>
    /// 所属文章或评论
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 赞或踩 赞(true)踩(false)
    /// </summary>
    public bool IsPositive { get; set; } = true;
}

/// <summary>
/// 点赞类型
/// </summary>
public enum PollTypeEnum
{
    /// <summary>
    /// 文章
    /// </summary>
    [Description("文章")]
    Article = 1,

    /// <summary>
    /// 评论
    /// </summary>
    [Description("评论")]
    Comment = 2
}