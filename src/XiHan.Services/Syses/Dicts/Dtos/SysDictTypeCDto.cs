#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictTypeCDto
// Guid:2343c661-15b8-45ba-ae2a-715ce9dbec4c
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-10 上午 02:15:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictTypeCDto
/// </summary>
public class SysDictTypeCDto
{
    /// <summary>
    /// 字典名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string DictName { get; set; } = string.Empty;

    /// <summary>
    /// 字典编码
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(5, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression("^[a-z][a-z0-9_]*$", ErrorMessage = "{0}必须以字母开头,且只能由小写字母、加下划线、数字组成")]
    public string DictCode { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsEnable { get; set; } = true;

    /// <summary>
    /// 是否系统内置
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsOfficial { get; set; }

    /// <summary>
    /// 字典描述
    /// </summary>
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Description { get; set; }
}