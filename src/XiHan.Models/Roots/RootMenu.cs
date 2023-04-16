#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootMenu
// long:9add3fb4-8c8d-4f8c-92d2-ec35ee45fc20
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-04 下午 01:23:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases;

namespace XiHan.Models.Roots;

/// <summary>
/// 系统菜单表
/// </summary>
[SugarTable(TableName = "RootMenu")]
public class RootMenu : BaseEntity
{
    /// <summary>
    /// 父级菜单
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    [SugarColumn(Length = 10)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 菜单图标
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// 路由地址
    /// </summary>
    [SugarColumn(Length = 200)]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// 路由参数
    ///</summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? Query { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(Length = 200, IsNullable = true)]
    public string? ComponentPath { get; set; }

    /// <summary>
    /// 菜单排序
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 是否为外部链接
    ///</summary>
    public bool IsLink { get; set; }

    /// <summary>
    /// 是否缓存
    ///</summary>
    public bool IsCache { get; set; }

    /// <summary>
    /// 是否显示
    ///</summary>
    public bool IsShow { get; set; }

    /// <summary>
    /// 菜单描述
    /// </summary>
    [SugarColumn(IsNullable = true, Length = 50)]
    public string? Remark { get; set; }
}