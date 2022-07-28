// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CBlogTagDto
// Guid:debcfa93-ff4a-4425-9299-32225a40a471
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-28 下午 12:17:38
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// CBlogTagDto
/// </summary>
public class CBlogTagDto
{
    /// <summary>
    /// 创建用户
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 标签名称
    /// </summary>
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string TagName { get; set; } = string.Empty;
}