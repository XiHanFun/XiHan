#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogPoll
// Guid:2fcf240c-8218-497c-aae9-83f9c5791dfe
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:29:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Blogs;

/// <summary>
/// 博客点赞表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable(TableName = "Blog_Poll")]
public class BlogPoll : BaseCreateEntity
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