#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserLoginByAccountCDto
// Guid:513585e4-61b7-45b3-b7dc-78c6cd626b70
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 下午 02:24:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHan.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserLoginByAccountCDto
/// </summary>
public class SysUserLoginByAccountCDto
{
    /// <summary>
    /// 用户账号
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 用户密码
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public string Password { get; set; } = string.Empty;
}