#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SysDictData
// Guid:15fc58cc-facc-4767-bc32-0561127a7194
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-10-24 上午 11:11:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Models.Bases.Entity;

namespace XiHan.Models.Syses;

/// <summary>
/// 站点字典数据表
/// </summary>
[SugarTable(TableName = "SysDictData")]
public class SysDictData : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 字典类型
    ///</summary>
    [SugarColumn(Length = 50)]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 字典标签
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 字典项值
    /// </summary>
    [SugarColumn(Length = 100)]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 字典项排序
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 样式
    /// </summary>
    [SugarColumn(Length = 50)]
    public string CssClass { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认值
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool IsDefault { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; } = true;

    /// <summary>
    /// 字典项描述
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string? Remark { get; set; }
}