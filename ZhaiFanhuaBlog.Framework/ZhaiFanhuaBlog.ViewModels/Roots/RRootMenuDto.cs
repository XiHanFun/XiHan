// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RRootMenuDto
// Guid:aa5374b0-571b-491e-9f5f-d8d4394879d7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-06 上午 01:51:46
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.ViewModels.Bases.Results;

namespace ZhaiFanhuaBlog.ViewModels.Roots;

/// <summary>
/// RRootMenuDto
/// </summary>
public class RRootMenuDto : BaseResultFieldDto
{
    /// <summary>
    /// 父级菜单
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 菜单描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// 页面路径
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 菜单排序
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 是否启用（0=未启用，1=启用）
    /// </summary>
    public bool IsEnable { get; set; } = true;

    /// <summary>
    /// 子级菜单
    /// </summary>
    public List<RRootMenuDto>? ChildrenMenu { get; set; }
}