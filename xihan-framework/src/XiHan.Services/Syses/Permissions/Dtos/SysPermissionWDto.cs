#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPermissionWDto
// Guid:f29fdb7b-a13c-4437-b1c6-6276afd781f1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023/7/12 4:06:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace XiHan.Services.Syses.Permissions.Dtos;

/// <summary>
/// SysPermissionWDto
/// </summary>
public class SysPermissionWDto
{
    /// <summary>
    /// 权限代码
    /// </summary>
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Code { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    [MinLength(4, ErrorMessage = "{0}不能少于{1}个字符")]
    [MaxLength(20, ErrorMessage = "{0}不能多于{1}个字符")]
    public string? Name { get; set; }

    /// <summary>
    /// 权限类型
    /// PermissionTypeEnum
    /// </summary>
    public int? PermissionType { get; set; }
}