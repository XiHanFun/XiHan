#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CUserAccountDto
// Guid:48466d27-6033-4199-b7d5-8928d39f92bf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-03 上午 03:16:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// CUserAccountDto
/// </summary>
public class CUserAccountDto
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
    public string UserEmail { get; set; } = string.Empty;

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
    public string? AvatarPath { get; set; } = string.Empty;

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
}