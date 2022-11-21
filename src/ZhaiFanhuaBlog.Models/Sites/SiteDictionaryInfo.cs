#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteDictionaryInfo
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
/// 网站字典信息表
/// </summary>
public class SiteDictionaryInfo : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 字典类型
    ///</summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string DictType { get; set; } = string.Empty;

    /// <summary>
    /// 字典标签
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string InfoLabel { get; set; } = string.Empty;

    /// <summary>
    /// 字典项值
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)")]
    public string InfoValue { get; set; } = string.Empty;

    /// <summary>
    /// 字典项排序
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int? SortOrder { get; set; }

    /// <summary>
    /// 是否默认值
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? IsDefault { get; set; }

    /// <summary>
    /// 是否启用 1启用 0禁用
    /// </summary>
    public bool IsEnable { get; set; } = true;

    /// <summary>
    /// 字典项描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(100)", IsNullable = true)]
    public string? Discription { get; set; }
}