#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:FollowStatus
// Guid:57c1b511-ab04-41a3-835e-7a6c7c1f8667
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-09-09 上午 10:48:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Models.Blogs.Enums;

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