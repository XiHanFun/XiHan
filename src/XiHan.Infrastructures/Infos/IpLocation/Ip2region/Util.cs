using System.Net;

namespace XiHan.Infrastructures.Infos.IpLocation.Ip2region;

/// <summary>
/// 工具类
/// </summary>
/// <remarks>powered by https://github.com/lionsoul2014/ip2region</remarks>
public static class Util
{
    /// <summary>
    /// 地址转换
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public static uint IpAddressToUInt32(string ipAddress)
    {
        var address = IPAddress.Parse(ipAddress);
        return IpAddressToUInt32(address);
    }

    /// <summary>
    /// 地址转换
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public static uint IpAddressToUInt32(IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    /// 中间IP
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static uint GetMidIp(uint x, uint y)
    {
        return (x & y) + ((x ^ y) >> 1);
    }

    /// <summary>
    /// 中间IP
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int GetMidIp(int x, int y)
    {
        return (x & y) + ((x ^ y) >> 1);
    }
}