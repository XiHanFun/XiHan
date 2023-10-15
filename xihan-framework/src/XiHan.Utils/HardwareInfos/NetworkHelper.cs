#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:NetworkHelper
// Guid:dc0502e1-f675-41d3-8a67-dbd590e76260
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-05-09 上午 01:11:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net.NetworkInformation;
using XiHan.Utils.Extensions;

namespace XiHan.Utils.HardwareInfos;

/// <summary>
/// 网卡信息帮助类
/// </summary>
public static class NetworkHelper
{
    /// <summary>
    /// 网卡信息
    /// </summary>
    public static List<NetworkInfo> NetworkInfos => GetNetworkInfos();

    /// <summary>
    /// 获取网卡信息
    /// </summary>
    /// <returns></returns>
    public static List<NetworkInfo> GetNetworkInfos()
    {
        List<NetworkInfo> networkInfos = new();

        try
        {
            // 获取可用网卡
            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                .Where(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet || ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                .ToList();

            foreach (var ni in interfaces)
            {
                var properties = ni.GetIPProperties();
                NetworkInfo networkInfo = new()
                {
                    Name = ni.Name,
                    Description = ni.Description,
                    Type = ni.NetworkInterfaceType.ToString(),
                    Speed = ni.Speed.ToString("#,##0") + " bps",
                    PhysicalAddress = BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes()),
                    DnsAddresses = properties.DnsAddresses.Select(ip => ip.ToString()).ToList(),
                    IpAddresses = properties.UnicastAddresses.Select(ip => ip.Address + " / " + ip.IPv4Mask).ToList()
                };
                networkInfos.Add(networkInfo);
            }
        }
        catch (Exception ex)
        {
            ("获取网卡信息出错，" + ex.Message).WriteLineError();
        }

        return networkInfos;
    }
}

/// <summary>
/// 网卡信息
/// </summary>
public class NetworkInfo
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 速度
    /// </summary>
    public string Speed { get; set; } = string.Empty;

    /// <summary>
    /// 物理地址(mac地址)
    /// </summary>
    public string PhysicalAddress { get; set; } = string.Empty;

    /// <summary>
    /// DNS地址
    /// </summary>
    public List<string> DnsAddresses { get; set; } = new();

    /// <summary>
    /// IP地址
    /// </summary>
    public List<string> IpAddresses { get; set; } = new();
}