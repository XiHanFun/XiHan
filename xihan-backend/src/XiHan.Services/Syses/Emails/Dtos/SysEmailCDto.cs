#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysEmailCDto
// Guid:8524ca21-3829-4357-80f7-12992bbadf2b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/4 2:17:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHan.Services.Syses.Emails.Dtos;

/// <summary>
/// SysEmailCDto
/// </summary>
public class SysEmailCDto
{
    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool IsEnabled { get; set; }

    /// <summary>
    /// 服务器
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(32, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public int Port { get; set; }

    /// <summary>
    /// 是否SSL加密
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public bool UseSsl { get; set; }

    /// <summary>
    /// 发自邮箱
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string FromMail { get; set; } = string.Empty;

    /// <summary>
    /// 发自密码
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string FromPassword { get; set; } = string.Empty;

    /// <summary>
    /// 发自名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(64, ErrorMessage = "{0}不能多于{1}个字符")]
    public string FromUserName { get; set; } = string.Empty;
}