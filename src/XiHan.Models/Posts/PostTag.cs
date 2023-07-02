#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:PostTag
// long:fa23fa92-d511-41b1-ac8d-1574fa01a3af
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 06:31:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Posts;

/// <summary>
/// 文章标签表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable(TableName = "Post_Tag")]
public class PostTag : BaseModifyEntity
{
    /// <summary>
    /// 标签别名
    /// </summary>
    [SugarColumn(Length = 20)]
    public string Alias { get; set; } = string.Empty;

    /// <summary>
    /// 标签名称
    /// </summary>
    [SugarColumn(Length = 20)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 标签封面
    /// </summary>
    [SugarColumn(Length = 500)]
    public string Cover { get; set; } = string.Empty;

    /// <summary>
    /// 标签颜色
    /// </summary>
    [SugarColumn(Length = 20)]
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// 标签描述
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 文章总数
    /// </summary>
    public int ArticleCount { get; set; }
}