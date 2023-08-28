#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CRootAuthorityDto
// Guid:e063ddee-794e-4927-9617-5f0cc77815b9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-15 下午 06:07:09
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Sys;

/// <summary>
/// CSysConfigurationDto
/// </summary>
public class CSysConfigurationDto
{
    /// <summary>
    /// 网站名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 网站描述
    /// </summary>
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(200, ErrorMessage = "{0}不能多于{1}个字")]
    public string? Description { get; set; }

    /// <summary>
    /// 网站关键字
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(200, ErrorMessage = "{0}不能多于{1}个字")]
    public string KeyWord { get; set; } = string.Empty;

    /// <summary>
    /// 网站域名
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(50, ErrorMessage = "{0}不能多于{1}个字")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// 站长名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(2, ErrorMessage = "{0}不能少于{1}个字"), MaxLength(20, ErrorMessage = "{0}不能多于{1}个字")]
    public string AdminName { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮件
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(50, ErrorMessage = "{0}不能多于{1}个字符")]
    [RegularExpression(@"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$", ErrorMessage = "请输入正确的邮箱地址")]
    public string AdminEmail { get; set; } = string.Empty;

    /// <summary>
    /// 升级时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}