#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogTag
// Guid:fa23fa92-d511-41b1-ac8d-1574fa01a3af
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:31:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Blogs;

/// <summary>
/// 文章标签表
/// </summary>
public class BlogTag : BaseEntity
{
    /// <summary>
    /// 创建用户
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 标签名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(20)")]
    public string TagName { get; set; } = string.Empty;

    /// <summary>
    /// 文章总数
    /// </summary>
    public int BlogCount { get; set; } = 0;
}