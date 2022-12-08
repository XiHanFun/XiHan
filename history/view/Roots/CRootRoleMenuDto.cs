#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CRootRoleMenuDto
// Guid:90b96c06-28b3-4ca1-b578-f05ba42efd66
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-06 上午 02:22:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel.DataAnnotations;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// CRootRoleMenuDto
/// </summary>
public class CRootRoleMenuDto
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 系统菜单
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [RegularExpression(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", ErrorMessage = "{0}Guid错误")]
    public Guid MenuId { get; set; }
}