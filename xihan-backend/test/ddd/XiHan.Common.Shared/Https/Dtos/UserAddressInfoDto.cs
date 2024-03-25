#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:UserAddressInfoDto
// Guid:c4b1b11a-c5e9-4ff5-b376-19be16570082
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/29 2:00:46
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Common.Shared.Https.Dtos;

/// <summary>
/// 地址信息
/// </summary>
public class UserAddressInfoDto
{
    /// <summary>
    /// 远程IPv4
    /// </summary>
    public string RemoteIPv4 { get; set; } = string.Empty;

    /// <summary>
    /// 远程IPv6
    /// </summary>
    public string RemoteIPv6 { get; set; } = string.Empty;

    /// <summary>
    /// 长地址
    /// </summary>
    public string AddressInfo { get; set; } = string.Empty;

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