#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ClientModel
// Guid:de1200e8-d2f1-426c-a90b-8e337597bbfd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-14 上午 04:54:13
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Utils.ClientEquipment;

/// <summary>
/// ClientModel
/// </summary>
public class ClientModel
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