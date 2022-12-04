#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PostTag
// Guid:fa23fa92-d511-41b1-ac8d-1574fa01a3af
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:31:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases.Entity;

namespace ZhaiFanhuaBlog.Models.Posts;

/// <summary>
/// 文章标签表
/// </summary>
public class PostTag : BaseEntity
{
    /// <summary>
    /// 标签别名
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string TagAlias { get; set; } = string.Empty;

    /// <summary>
    /// 标签名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string TagName { get; set; } = string.Empty;

    /// <summary>
    /// 标签描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string TagDescription { get; set; } = string.Empty;

    /// <summary>
    /// 文章总数
    /// </summary>
    public int BlogCount { get; set; }
}