#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysDictData
// long:15fc58cc-facc-4767-bc32-0561127a7194
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-10-24 上午 11:11:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Domain.Bases.Attributes;
using XiHan.Domain.Bases.Entities;

namespace XiHan.Domain.Syses;

/// <summary>
/// 系统字典项表
/// </summary>
/// <remarks>记录新增，修改信息</remarks>
[SugarTable, SystemTable]
public class SysDictData : BaseModifiedEntity
{
    /// <summary>
    /// 字典编码
    ///</summary>
    [SugarColumn(Length = 64)]
    public string TypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典项标签
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 字典项值
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 字典项排序
    /// </summary>
    [SugarColumn]
    public int SortOrder { get; set; }

    /// <summary>
    /// 字典项样式
    /// </summary>
    [SugarColumn(Length = 64)]
    public string CssClass { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认值
    /// </summary>
    [SugarColumn]
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// 是否启用
    /// </summary>
    [SugarColumn]
    public bool IsEnable { get; set; } = true;

    /// <summary>
    /// 字典项描述
    /// </summary>
    [SugarColumn(Length = 256, IsNullable = true)]
    public string? Description { get; set; }
}