#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserPwdMDto
// Guid:75e97e31-ea63-4893-bd04-498197785ba9
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-10 上午 04:08:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Services.Bases.Dtos;

namespace XiHan.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserPwdMDto
/// </summary>
public class SysUserPwdMDto : IBaseIdDto
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public long BaseId { get; set; }

    /// <summary>
    /// 当前密码(MD5加密)
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string OldPassword { get; set; } = string.Empty;

    /// <summary>
    /// 重置密码(MD5加密)
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string NewPassword { get; set; } = string.Empty;
}