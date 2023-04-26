﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysUserCreateDto
// Guid:40210191-1ee8-4a89-9c9b-00df05255ae9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-27 上午 12:47:30
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using System.ComponentModel.DataAnnotations;
using XiHan.Models.Bases;
using XiHan.Models.Syses;

namespace XiHan.Services.Syses.Dtos;

/// <summary>
/// SysUserCreateDto
/// </summary>
public class SysUserCreateDto : BaseEntity
{
    /// <summary>
    /// 用户名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮件
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$", ErrorMessage = "请输入正确的邮箱地址")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(11, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression(@"^(\d{3,4})\d{7,8}$", ErrorMessage = "请输入正确的手机号")]
    public string Phone { get; set; } = string.Empty;

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
    /// 用户昵称
    /// </summary>
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? NickName { get; set; }

    /// <summary>
    /// 用户签名
    /// </summary>
    [MaxLength(200, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Signature { get; set; }

    /// <summary>
    /// 用户性别 男(true)女(false)
    /// </summary>
    public bool? Gender { get; set; }

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
    public List<SysRole> SysRoles { get; set; } = new List<SysRole>();

    /// <summary>
    /// 所属角色主键集合
    /// </summary>
    public List<long> SysRoleIds { get; set; } = new List<long>();
}