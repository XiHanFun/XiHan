// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserRole
// Guid:26b82e42-87a7-477b-af80-59456336a22b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 04:42:51
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户角色表
/// </summary>
[SugarTable("UserRole", "用户角色表")]
public class UserRole : BaseEntity
{
    /// <summary>
    /// 父级角色
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "父级角色")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)", ColumnDescription = "角色名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 角色描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true, ColumnDescription = "角色描述")]
    public string? Description { get; set; }
}