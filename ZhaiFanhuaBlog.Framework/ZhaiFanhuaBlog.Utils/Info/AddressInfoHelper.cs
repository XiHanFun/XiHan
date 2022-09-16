// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AddressInfoHelper
// Guid:297d7804-a5c1-4ab7-bdf6-faa0a7f9aa76
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 下午 10:20:57
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.Utils.Info;

/// <summary>
/// 地区信息
/// </summary>
public class AddressInfoHelper
{
    /// <summary>
    /// 国家 中国
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// 省份/自治区/直辖市 贵州
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// 地级市 安顺
    /// </summary>
    public string PrefectureLevelCity { get; set; } = string.Empty;

    /// <summary>
    /// 区/县 西秀区
    /// </summary>
    public string DistrictOrCounty { get; set; } = string.Empty;

    /// <summary>
    /// 运营商 联通
    /// </summary>
    public string Operator { get; set; } = string.Empty;

    /// <summary>
    /// 邮政编码 561000
    /// </summary>
    public long PostalCode { get; set; }

    /// <summary>
    /// 地区区号 0851
    /// </summary>
    public int AreaCode { get; set; }
}