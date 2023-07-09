#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictDataCDto
// Guid:3acc9c8d-84e3-4d87-8218-9869ac21a634
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-10 上午 02:14:31
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictDataCDto
/// </summary>
public class SysDictDataCDto : BaseIdDto
{
    /// <summary>
    /// 字典ID
    ///</summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典项标签
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 字典项值
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(10, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 自定义 SQL
    /// </summary>
    [MaxLength(2000, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? CustomSql { get; set; }

    /// <summary>
    /// 字典项排序
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public int SortOrder { get; set; }

    /// <summary>
    /// 字典项样式
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    public string CssClass { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认值
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsEnable { get; set; } = true;

    /// <summary>
    /// 字典项描述
    /// </summary>
    [MaxLength(100, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Description { get; set; }
}