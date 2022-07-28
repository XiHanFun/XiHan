// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CBlogPollDto
// Guid:5dd861b5-080e-4bbe-9323-25aecad7970f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 10:45:56
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// CBlogPollDto
/// </summary>
public class CBlogPollDto
{
    /// <summary>
    /// 点赞者
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 所属文章
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 赞(true)或踩(false)
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsPositive { get; set; } = true;
}