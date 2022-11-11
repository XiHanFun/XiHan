#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CRootMenuDto
// Guid:f0930d8b-d56a-445c-9d4f-11edad76e5fc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-06 上午 01:19:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// CRootMenuDto
/// </summary>
public class CRootMenuDto
{
    /// <summary>
    /// 父级菜单
    /// </summary>
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(10, ErrorMessage = "{0}不能多于{1}个字")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 菜单描述
    /// </summary>
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(50, ErrorMessage = "{0}不能多于{1}个字")]
    public string? Description { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(200, ErrorMessage = "{0}不能多于{1}个字")]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// 页面路径
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(200, ErrorMessage = "{0}不能多于{1}个字")]
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 菜单排序
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public int Order { get; set; }

    /// <summary>
    /// 是否启用（0=未启用，1=启用）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsEnable { get; set; } = true;
}