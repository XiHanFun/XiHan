#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PollEnum
// Guid:f199f9e2-a3de-42f0-b253-9167210fc0fc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-05 上午 03:07:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace ZhaiFanhuaBlog.Models.Posts.Enums;

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