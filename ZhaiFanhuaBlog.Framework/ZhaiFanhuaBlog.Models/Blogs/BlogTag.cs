// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogTag
// Guid:fa23fa92-d511-41b1-ac8d-1574fa01a3af
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:31:06
// ----------------------------------------------------------------

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章标签表
/// </summary>
[SugarTable("BlogTag", "文章标签表")]
public class BlogTag : BaseEntity
{
    /// <summary>
    /// 创建者
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者")]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 标签名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)", ColumnDescription = "标签名称")]
    public string TagName { get; set; } = string.Empty;

    /// <summary>
    /// 文章总数
    /// </summary>
    [SugarColumn(ColumnDescription = "文章总数")]
    public int BlogCount { get; set; } = 0;
}