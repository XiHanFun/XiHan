#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License.See LICENSE in the project root for license information.
// FileName:SysUserCreateDto
// Guid:40210191-1ee8-4a89-9c9b-00df05255ae9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-27 上午 12:47:30
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Models.Bases;
using XiHan.Models.Syses;

namespace XiHan.Services.Syses.Users.Dtos;

/// <summary>
/// SysUserWhereDto
/// </summary>
public class SysUserWhereDto : BaseEntity
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

/// <summary>
/// SysUserCreateDto
/// </summary>
public class SysUserCreateDto : SysUserWhereDto
{
    /// <summary>
    /// 用户密码（MD5加密）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 头像路径
    /// </summary>
    [MaxLength(200, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? AvatarPath { get; set; }

    /// <summary>
    /// 用户签名
    /// </summary>
    [MaxLength(200, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Signature { get; set; }

    /// <summary>
    /// 用户地址
    /// </summary>
    [MaxLength(200, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Address { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 所属角色集合
    /// </summary>
    public List<SysRole> SysRoles { get; set; } = new();

    /// <summary>
    /// 所属角色主键集合
    /// </summary>
    public List<long> SysRoleIds { get; set; } = new();
}

/// <summary>
/// SysUserPasswordDto
/// </summary>
public class SysUserPasswordDto
{
    /// <summary>
    /// 当前密码（MD5加密）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string OldPassword { get; set; } = string.Empty;

    /// <summary>
    /// 重置密码（MD5加密）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string NewPassword { get; set; } = string.Empty;
}