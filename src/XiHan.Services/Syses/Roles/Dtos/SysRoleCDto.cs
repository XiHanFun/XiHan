#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysRoleCDto
// Guid:e4d833ba-bafa-4861-90f1-0c19de414561
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-11 上午 10:38:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Roles.Dtos;

/// <summary>
/// SysRoleCDto
/// </summary>
public class SysRoleCDto
{
    /// <summary>
    /// 父级角色
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 角色代码
    /// 如：admin
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(5, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression("^[a-z]*$", ErrorMessage = "{0}必须以字母开头,且只能由小写字母组成")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 角色名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 字典项排序
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public int SortOrder { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    [MaxLength(100, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Description { get; set; }
}