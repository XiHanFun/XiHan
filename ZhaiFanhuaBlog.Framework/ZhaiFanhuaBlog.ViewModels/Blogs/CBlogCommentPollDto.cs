#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CBlogCommentPollDto
// Guid:40044230-0315-4300-a582-9b8ef07a7684
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 10:53:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// CBlogCommentPollDto
/// </summary>
public class CBlogCommentPollDto
{
    /// <summary>
    /// 点赞者
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 所属评论
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid CommentId { get; set; }

    /// <summary>
    /// 赞(true)或踩(false)
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsPositive { get; set; } = true;
}