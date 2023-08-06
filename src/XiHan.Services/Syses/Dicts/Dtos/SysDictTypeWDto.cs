#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictTypeWDto
// Guid:3cd51a41-be16-4d82-901e-40e7dc59496b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-12 下午 04:43:08
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHan.Services.Syses.Dicts.Dtos;

/// <summary>
/// SysDictTypeWDto
/// </summary>
public class SysDictTypeWDto
{
    /// <summary>
    /// 字典编码
    ///</summary>
    [MinLength(5, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression("^[a-z][a-z0-9_]*$", ErrorMessage = "{0}必须以字母开头,且只能由小写字母、加下划线、数字组成")]
    public string? Code { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Name { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? IsEnable { get; set; }

    /// <summary>
    /// 是否系统内置
    /// </summary>
    public bool? IsOfficial { get; set; }
}