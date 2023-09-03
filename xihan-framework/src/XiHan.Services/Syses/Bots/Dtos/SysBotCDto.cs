#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysBotCDto
// Guid:8524ca21-3829-4357-80f7-12992bbadf2b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/9/4 2:17:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;
using XiHan.Models.Syses.Enums;

namespace XiHan.Services.Syses.Bots.Dtos;

/// <summary>
/// SysBotCDto
/// </summary>
public class SysBotCDto
{
    /// <summary>
    /// 自定义机器人标题
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 自定义机器人类型
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    public BotTypeEnum BotType { get; set; }

    /// <summary>
    /// 网络挂钩地址
    /// 为空时使用系统默认地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? WebHookUrl { get; set; }

    /// <summary>
    /// 访问令牌
    /// 钉钉、飞书为 AccessToken，企业微信为 Key
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MaxLength(128, ErrorMessage = "{0}不能多于{1}个字符")]
    public string AccessTokenOrKey { get; set; } = string.Empty;

    /// <summary>
    /// 机密
    /// 钉钉、飞书用
    /// </summary>
    [MaxLength(128, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Secret { get; set; }

    /// <summary>
    /// 关键字
    /// 钉钉、飞书用
    /// </summary>
    [MaxLength(128, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? KeyWord { get; set; }

    /// <summary>
    /// 上传地址 企业微信用
    /// 为空时使用系统默认地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? UploadUrl { get; set; } = string.Empty;

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnabled { get; set; }
}