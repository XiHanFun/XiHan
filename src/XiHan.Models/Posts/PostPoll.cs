#region <<版权版本注释>>

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
using XiHan.Models.Bases;
using XiHan.Models.Posts.Enums;

namespace XiHan.Models.Posts;

/// <summary>
/// 文章点赞表
/// </summary>
[SugarTable(TableName = "PostPoll")]
public class PostPoll : BaseEntity
{
    /// <summary>
    /// 所属文章或评论
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 点赞类型(文章或评论)
    /// </summary>
    public PollTypeEnum PollType { get; set; }

    /// <summary>
    /// 赞(true)或踩(false)
    /// </summary>
    public bool IsPositive { get; set; } = true;
}