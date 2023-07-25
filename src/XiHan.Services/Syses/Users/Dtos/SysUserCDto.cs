#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License.See LICENSE in the project root for license information.
// FileName:SysUserCDto
// Guid:40210191-1ee8-4a89-9c9b-00df05255ae9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-27 上午 12:47:30
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHan.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserCDto
/// </summary>
public class SysUserCDto
{
    /// <summary>
    /// 用户账号
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(10, ErrorMessage = "{0}不能多于{1}个字符")]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$", ErrorMessage = "请输入正确的邮箱地址")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 角色集合
    /// </summary>
    public List<long>? RoleIds { get; set; }
}