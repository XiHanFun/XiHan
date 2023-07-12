#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserWDto
// Guid:dd0da638-fc76-4803-80eb-d186c170333f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-10 上午 04:07:19
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserWDto
/// </summary>
public class SysUserWDto
{
    /// <summary>
    /// 用户账号
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 用户昵称
    /// </summary>
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? NickName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(10, ErrorMessage = "{0}不能多于{1}个字符")]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    public bool? Gender { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$", ErrorMessage = "请输入正确的邮箱地址")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    [MaxLength(11, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression(@"^(\d{3,4})\d{7,8}$", ErrorMessage = "请输入正确的手机号码")]
    public string? Phone { get; set; } = string.Empty;
}