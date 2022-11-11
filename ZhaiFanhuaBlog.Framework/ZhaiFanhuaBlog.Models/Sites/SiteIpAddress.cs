#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteIpAddress
// Guid:171a7673-fe61-4257-844e-84fdff2d5e0a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-10-24 上午 10:40:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using ZhaiFanhuaBlog.Models.Bases;

namespace ZhaiFanhuaBlog.Models.Sites;

/// <summary>
/// 系统Ip地区信息
/// </summary>
public class SiteIpAddress : BaseDeleteEntity<Guid>
{
    /// <summary>
    /// Ip地址
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键标识")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 国家 中国
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? Country { get; set; }

    /// <summary>
    /// 省份/自治区/直辖市 贵州
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? State { get; set; }

    /// <summary>
    /// 地级市 安顺
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? PrefectureLevelCity { get; set; }

    /// <summary>
    /// 区/县 西秀区
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(50)", IsNullable = true)]
    public string? DistrictOrCounty { get; set; }

    /// <summary>
    /// 运营商 联通
    /// </summary>
    [SugarColumn(ColumnDataType = "nvarchar(10)", IsNullable = true)]
    public string? Operator { get; set; }

    /// <summary>
    /// 邮政编码 561000
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? PostalCode { get; set; }

    /// <summary>
    /// 地区区号 0851
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int? AreaCode { get; set; }
}