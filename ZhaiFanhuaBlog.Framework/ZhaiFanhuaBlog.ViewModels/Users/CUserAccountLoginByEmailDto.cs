// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CUserAccountLoginByEmailDto
// Guid:76668b0e-0e54-47c0-956d-1d6a5b0ae9be
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-04 上午 12:53:49
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// CUserAccountLoginByEmailDto
/// </summary>
public class CUserAccountLoginByEmailDto
{
    /// <summary>
    /// 电子邮件
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$", ErrorMessage = "请输入正确的邮箱地址")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 用户密码（MD5加密）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string Password { get; set; } = string.Empty;
}