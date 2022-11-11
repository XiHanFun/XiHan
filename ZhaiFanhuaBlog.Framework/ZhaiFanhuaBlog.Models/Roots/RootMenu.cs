#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootMenu
// Guid:9add3fb4-8c8d-4f8c-92d2-ec35ee45fc20
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-04 下午 01:23:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Roots;

/// <summary>
/// 系统菜单表
/// </summary>
public class RootMenu : BaseEntity
{
    /// <summary>
    /// 父级菜单
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 菜单描述
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDataType = "nvarchar(50)")]
    public string? Description { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)")]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// 页面路径
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(200)")]
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 菜单排序
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 是否启用（0=未启用，1=启用）
    /// </summary>
    public bool IsEnable { get; set; } = true;
}