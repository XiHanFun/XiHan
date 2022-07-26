// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CUserAccountLoginByNameDto
// Guid:276a21dc-7735-480b-b880-ccbcf908fae8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-04 上午 12:53:29
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// CUserAccountLoginByNameDto
/// </summary>
public class CUserAccountLoginByNameDto
{
    /// <summary>
    /// 用户名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(6, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 用户密码（MD5加密）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "密码至少为8个字符，至少1个大写字母，1个小写字母，1个数字和1个特殊字符")]
    public string Password { get; set; } = string.Empty;
}