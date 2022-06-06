// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IpInfoHelper
// Guid:dc0502e1-f675-41d3-8a67-dbd590e76260
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 上午 01:11:29
// ----------------------------------------------------------------

using System.Net.Sockets;
using System.Net;

namespace ZhaiFanhuaBlog.Utils.Infos;

public static class IpInfoHelper
{
    /// <summary>
    /// 获取本机IP地址
    /// </summary>
    /// <returns>本机IP地址</returns>
    public static string GetLocalIP()
    {
        try
        {
            // 得到主机名
            string HostName = Dns.GetHostName();
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                // 从IP地址列表中筛选出IPv4类型的IP地址
                // AddressFamily.InterNetwork表示此IP为IPv4,
                // AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return "IPv4:" + IpEntry.AddressList[i].ToString();
                }
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetworkV6)
                {
                    return "IPv6:" + IpEntry.AddressList[i].ToString();
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