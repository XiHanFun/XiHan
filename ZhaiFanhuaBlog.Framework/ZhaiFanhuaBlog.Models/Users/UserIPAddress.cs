// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:UserIPAddress
// Guid:da2af06f-0fe9-428c-a8b6-03e166eb41a7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-18 上午 04:48:00
// ----------------------------------------------------------------

using SqlSugar;

namespace ZhaiFanhuaBlog.Models.Users;

/// <summary>
/// 账户IP地区信息
/// </summary>
public class UserIPAddress
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