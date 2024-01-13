#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BlogFollows
// Guid:196d9961-eb5f-4e8d-807d-a29b87a0a4f9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-08 下午 06:05:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.ComponentModel;
using XiHan.Models.Bases.Entities;

namespace XiHan.Models.Blogs;

/// <summary>
/// 博客关注表
/// </summary>
/// <remarks>记录新增信息</remarks>
[SugarTable]
public class BlogFollows : BaseCreateEntity
{
    /// <summary>
    /// 被关注用户
    /// </summary>
    [SugarColumn]
    public long FollowerId { get; set; }

    /// <summary>
    /// 粉丝用户
    /// </summary>
    [SugarColumn]
    public long FolloweeId { get; set; }

    /// <summary>
    /// 关注状态
    /// </summary>
    [SugarColumn]
    public FollowStatus FollowStatus { get; set; }
}

/// <summary>
/// FollowStatus
/// </summary>
public enum FollowStatus
{
    /// <summary>
    /// 取消关注
    /// </summary>
    [Description("取消关注")]
    Unfollow = 0,

    /// <summary>
    /// 已关注
    /// </summary>
    [Description("已关注")]
    Followed = 1,

    /// <summary>
    /// 相互关注
    /// </summary>
    [Description("相互关注")]
    Mutual = 2,
}