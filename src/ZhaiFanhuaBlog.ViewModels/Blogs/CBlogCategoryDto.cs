#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CBlogCategoryDto
// Guid:76235b52-8c9b-4998-95c4-ef71ccd45e29
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-22 上午 12:58:28
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Blogs;

/// <summary>
/// CBlogCategoryDto
/// </summary>
public class CBlogCategoryDto
{
    /// <summary>
    /// 父级分类
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 分类用户
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(10, ErrorMessage = "{0}不能多于{1}个字")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 分类描述
    /// </summary>
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字")]
    public string? Description { get; set; }
}