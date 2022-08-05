// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RRootAuthorityDto
// Guid:4c8f44cb-945c-43cb-89a3-7f3bdf13a63a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-20 上午 12:32:39
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// 返回系统权限
/// </summary>
public class RRootAuthorityDto : RBaseDto
{
    /// <summary>
    /// 父级权限
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 权限类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 权限描述
    /// </summary>
    public string? Description { get; set; }
}