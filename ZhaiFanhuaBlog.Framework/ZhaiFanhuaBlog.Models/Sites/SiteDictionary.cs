#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteDictionaryItem
// Guid:9f923080-701d-4eec-9171-2112f128fdaf
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-10-24 上午 11:10:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Sites;

/// <summary>
/// 网站字典表
/// </summary>
public class SiteDictionary : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// 字典编码
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 字典名称
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 字典描述
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? Description { get; set; }
}