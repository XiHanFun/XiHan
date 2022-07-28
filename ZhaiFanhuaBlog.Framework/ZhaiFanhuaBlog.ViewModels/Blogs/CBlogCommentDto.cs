// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CBlogCommentDto
// Guid:28e7b5d5-d28c-4499-b594-caafc59b755d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 04:14:39
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// CBlogCommentDto
/// </summary>
public class CBlogCommentDto
{
    /// <summary>
    /// 评论者
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 父级评论
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 所属文章
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(1, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(4000, ErrorMessage = "{0}不能多于{1}个字")]
    public string TheContent { get; set; } = string.Empty;
}