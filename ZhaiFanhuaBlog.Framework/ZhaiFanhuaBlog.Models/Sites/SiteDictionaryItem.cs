#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteDictionaryItem
// Guid:15fc58cc-facc-4767-bc32-0561127a7194
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-10-24 上午 11:11:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Sites;

/// <summary>
/// 网站字典项目表
/// </summary>
public class SiteDictionaryItem : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 字典Id
    /// </summary>
    public Guid DictionaryId { get; set; }

    /// <summary>
    /// 字典项
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string ItemKey { get; set; } = string.Empty;

    /// <summary>
    /// 字典项值
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string ItemValue { get; set; } = string.Empty;

    /// <summary>
    /// 字典项描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", IsNullable = true)]
    public string? Discription { get; set; }

    /// <summary>
    /// 字典项排序
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 字典项状态 1启用 0禁用
    /// </summary>
    public bool Enable { get; set; }
}