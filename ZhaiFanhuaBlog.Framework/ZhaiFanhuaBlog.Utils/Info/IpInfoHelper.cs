// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IpInfoHelper
// Guid:dc0502e1-f675-41d3-8a67-dbd590e76260
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:11:29
// ----------------------------------------------------------------

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ZhaiFanhuaBlog.Utils.Info;

public static class IpInfoHelper
{
    /// <summary>
    /// 获取本机IP地址
    /// </summary>
    /// <returns>本机IP地址</returns>
    public static string GetLocalIp()
    {
        try
        {
            // 获取可用网卡
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()?.Where(network => network.OperationalStatus == OperationalStatus.Up);
            // 获取所有可用网卡IP信息
            var unicastIPAddressInformations = networkInterfaces?.Select(x => x.GetIPProperties())?.SelectMany(x => x.UnicastAddresses);
            if (unicastIPAddressInformations != null)
            {
                foreach (var ipInfo in unicastIPAddressInformations)
                {
                    if (!IPAddress.IsLoopback(ipInfo.Address) && ipInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipInfo.Address.ToString();
                    }
                }
            }
            return "No IP address was obtained";
        }
        catch (Exception)
        {
            throw;
        }
    }
}