#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IpAddressModel
// Guid:98352a24-4798-46d7-8760-69ad2ddb0151
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-07 下午 11:58:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.Utils.Ip;

/// <summary>
/// Ip地区信息
/// </summary>
public class IpAddressModel
{
    /// <summary>
    /// Ip地址
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 国家 中国
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// 省份/自治区/直辖市 贵州
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// 地级市 安顺
    /// </summary>
    public string? PrefectureLevelCity { get; set; }

    /// <summary>
    /// 区/县 西秀区
    /// </summary>
    public string? DistrictOrCounty { get; set; }

    /// <summary>
    /// 运营商 联通
    /// </summary>
    public string? Operator { get; set; }

    /// <summary>
    /// 邮政编码 561000
    /// </summary>
    public long? PostalCode { get; set; }

    /// <summary>
    /// 地区区号 0851
    /// </summary>
    public int? AreaCode { get; set; }
}