// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CBlogArticleDto
// Guid:542a946c-4fa0-45fc-b8d7-4deb117fca5e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 上午 01:10:35
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// CBlogArticleDto
/// </summary>
public class CBlogArticleDto
{
    /// <summary>
    /// 文章作者
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 文章分类
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid CategoryId { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(100, ErrorMessage = "{0}不能多于{1}个字")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 文章概要
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public string TheContent { get; set; } = string.Empty;

    /// <summary>
    /// 是否置顶 是(true)否(false)
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsTop { get; set; } = false;

    /// <summary>
    /// 是否精华 是(true)否(false)
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsEssence { get; set; } = false;

    /// <summary>
    /// 是否是转发文章 是(true)否(false)
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsForward { get; set; } = false;

    /// <summary>
    /// 转发文章链接
    /// </summary>
    [RegularExpression(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$", ErrorMessage = "{0}URL错误")]
    public string? ForwardUrl { get; set; }
}