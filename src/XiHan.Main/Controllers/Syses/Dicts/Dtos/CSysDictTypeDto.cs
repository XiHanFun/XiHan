#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:CSysDictTypeDto
// Guid:5ecd4d58-269e-4297-972f-a7c8d0540aec
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-14 上午 02:31:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Main.Controllers.Bases;

namespace XiHan.Main.Controllers.Syses.Dicts.Dtos;

/// <summary>
/// 字典类型创建修改实体
/// </summary>
public class CSysDictTypeDto : BaseIdDto
{
    /// <summary>
    /// 字典名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符"), MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 字典类型
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(5, ErrorMessage = "{0}不能少于{1}个字符"), MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression("^[a-z][a-z0-9_]*$", ErrorMessage = "{0}必须以字母开头,且只能由小写字母、加下划线、数字组成")]
    public string Type { get; set; } = string.Empty;

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