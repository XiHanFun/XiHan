#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:NetworkHelper
// Guid:dc0502e1-f675-41d3-8a67-dbd590e76260
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:11:29
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net.NetworkInformation;

namespace XiHan.Commons.Infos.BaseInfos;

/// <summary>
/// 网卡信息帮助类
/// </summary>
public static class NetworkHelper
{
    /// <summary>
    /// 获取本机所有可用网卡信息
    /// </summary>
    /// <returns></returns>
    public static List<NetworkInfo> GetNetworkInfos()
    {
        var networkInfos = new List<NetworkInfo>();

        // 获取可用网卡
        var interfaces = NetworkInterface.GetAllNetworkInterfaces().Where(network => network.OperationalStatus == OperationalStatus.Up);

        foreach (NetworkInterface ni in interfaces)
        {
            IPInterfaceProperties properties = ni.GetIPProperties();
            var networkInfo = new NetworkInfo
            {
                Name = ni.Name,
                Description = ni.Description,
                Type = ni.NetworkInterfaceType.ToString(),
                Speed = ni.Speed.ToString("#,##0") + " bps",
                PhysicalAddress = ni.GetPhysicalAddress().ToString(),
                DnsAddresses = properties.DnsAddresses.Select(ip => ip.ToString()).ToList(),
                IpAddresses = properties.UnicastAddresses.Select(ip => ip.Address + " / " + ip.IPv4Mask).ToList(),
            };
            networkInfos.Add(networkInfo);
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
    /// 物理地址
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