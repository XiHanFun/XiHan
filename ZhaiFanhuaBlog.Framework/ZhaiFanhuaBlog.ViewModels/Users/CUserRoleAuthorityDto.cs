// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CUserRoleAuthorityDto
// Guid:b80feac5-81d8-4053-a4a7-9d61edd87fe1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-01 上午 02:29:41
// ----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Users;

/// <summary>
/// CUserRoleAuthorityDto
/// </summary>
public class CUserRoleAuthorityDto
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 用户权限
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid AuthorityId { get; set; }

    /// <summary>
    /// 用户权限类型（0:可访问，1:可授权）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [Range(0, 1, ErrorMessage = "{0}必须是0或1")]
    public int AuthorityType { get; set; }
}