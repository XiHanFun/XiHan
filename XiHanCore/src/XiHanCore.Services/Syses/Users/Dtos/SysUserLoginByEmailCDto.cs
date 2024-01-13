#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserLoginByEmailCDto
// Guid:93262b14-e2a2-461c-9e32-fb12bbe156b8
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-20 上午 10:33:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHanCore.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserLoginByEmailCDto
/// </summary>
public class SysUserLoginByEmailCDto
{
    /// <summary>
    /// 电子邮件
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$",
        ErrorMessage = "请输入正确的邮箱地址")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 用户密码
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\s]).{8,}$",
        ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string Password { get; set; } = string.Empty;
}