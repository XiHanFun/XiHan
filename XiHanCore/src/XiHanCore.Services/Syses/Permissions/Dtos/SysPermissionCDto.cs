#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPermissionCDto
// Guid:bced2989-202e-421e-a10a-5c4679788e0e
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/11 21:09:22
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHanCore.Services.Syses.Permissions.Dtos;

/// <summary>
/// SysPermissionCDto
/// </summary>
public class SysPermissionCDto
{
    /// <summary>
    /// 父级权限
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 权限代码
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 权限名称
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型
    /// PermissionTypeEnum
    /// </summary>
    public int PermissionType { get; set; }

    /// <summary>
    /// 权限描述
    /// </summary>
    [MaxLength(256, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Description { get; set; }
}