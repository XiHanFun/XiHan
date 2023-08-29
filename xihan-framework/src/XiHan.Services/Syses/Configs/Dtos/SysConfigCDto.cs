#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysConfigCDto
// Guid:a6456028-97a0-415c-9c00-1985bb3e9f3a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/8/5 3:10:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHan.Services.Syses.Configs.Dtos;

/// <summary>
/// SysConfigCDto
/// </summary>
public class SysConfigCDto
{
    /// <summary>
    /// 分类编码
    ///</summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string TypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 配置编码
    ///</summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 配置名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 配置项值
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(512, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 是否系统内置
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsOfficial { get; set; } = false;

    /// <summary>
    /// 字典描述
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Description { get; set; }
}