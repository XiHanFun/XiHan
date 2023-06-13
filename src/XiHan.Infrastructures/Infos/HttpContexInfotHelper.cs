#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HttpContextHelper
// Guid:07ebcfda-13ac-4019-a8ba-b03f21d6a8c2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 上午 01:51:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using XiHan.Infrastructures.Extensions;

namespace XiHan.Infrastructures.Infos;

/// <summary>
/// 请求上下文信息帮助类
/// </summary>
public class HttpContexInfotHelper
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpContext"></param>
    public HttpContexInfotHelper(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        ClientInfo = httpContext.GetClientInfo();
        AddressInfo = httpContext.GetAddressInfo();
    }

    /// <summary>
    /// 客户端信息
    /// </summary>
    public UserClientInfo? ClientInfo { get; set; }

    /// <summary>
    /// 地址信息
    /// </summary>
    public UserAddressInfo? AddressInfo { get; set; }
}

/// <summary>
/// Ip 地区信息
/// </summary>
public class UserAddressInfo
{
    /// <summary>
    /// 国家
    /// 中国
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// 省份/自治区/直辖市
    /// 贵州
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// 地级市
    /// 安顺
    /// </summary>
    public string? PrefectureLevelCity { get; set; }

    /// <summary>
    /// 区/县
    /// 西秀区
    /// </summary>
    public string? DistrictOrCounty { get; set; }

    /// <summary>
    /// 运营商
    /// 联通
    /// </summary>
    public string? Operator { get; set; }

    /// <summary>
    /// 邮政编码
    /// 561000
    /// </summary>
    public long? PostalCode { get; set; }

    /// <summary>
    /// 地区区号
    /// 0851
    /// </summary>
    public int? AreaCode { get; set; }
}

/// <summary>
/// 客户端信息
/// </summary>
public class UserClientInfo
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// 系统名称
    /// </summary>
    public string? OsName { get; set; }

    /// <summary>
    /// 系统版本
    /// </summary>
    public string? OsVersion { get; set; }

    /// <summary>
    /// 浏览器名称
    /// </summary>
    public string? UaName { get; set; }

    /// <summary>
    /// 浏览器版本
    /// </summary>
    public string? UaVersion { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// 引荐
    /// </summary>
    public string? Referer { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    public string? Agent { get; set; }

    /// <summary>
    /// 远程IPv4
    /// </summary>
    public string? RemoteIPv4 { get; init; }

    /// <summary>
    /// 远程IPv6
    /// </summary>
    public string? RemoteIPv6 { get; init; }
}