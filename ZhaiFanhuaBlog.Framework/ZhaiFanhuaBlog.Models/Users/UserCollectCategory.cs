// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserCollectCategory
// Guid:8d28b4f1-d0cc-4857-9150-353deffa880d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:03:34
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 用户收藏分类表
/// </summary>
[SugarTable("UserCollectCategory", "用户收藏分类表")]
public class UserCollectCategory : BaseEntity
{
    /// <summary>
    /// 父级收藏分类
    /// </summary>
    [SugarColumn(IsNullable = true, ColumnDescription = "父级收藏分类")]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 所属用户
    /// </summary>
    [SugarColumn(ColumnDescription = "所属用户")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 收藏分类名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", IsNullable = true, ColumnDescription = "收藏分类名称")]
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// 收藏分类描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true, ColumnDescription = "收藏分类描述")]
    public string? CategoryDescription { get; set; }

    /// <summary>
    /// 收藏总数
    /// </summary>
    [SugarColumn(ColumnDescription = "升级时间")]
    public int CollectCount { get; set; } = 0;
}