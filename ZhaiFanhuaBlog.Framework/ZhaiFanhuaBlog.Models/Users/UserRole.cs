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
public class UserRole : BaseEntity
{
    /// <summary>
    /// 父级角色
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 角色描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? Description { get; set; } = null;
}