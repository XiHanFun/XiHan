using System.Net;

namespace ZhaiFanhuaBlog.Utils.IpLocation.Ip2region;

/// <summary>
/// ������
/// powerd by https://github.com/lionsoul2014/ip2region
/// </summary>
public static class Util
{
    /// <summary>
    /// ��ַת��
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public static uint IpAddressToUInt32(string ipAddress)
    {
        var address = IPAddress.Parse(ipAddress);
        return IpAddressToUInt32(address);
    }

    /// <summary>
    /// ��ַת��
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public static uint IpAddressToUInt32(IPAddress ipAddress)
    {
        byte[] bytes = ipAddress.GetAddressBytes();
        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    /// �м�IP
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static uint GetMidIp(uint x, uint y)
        => (x & y) + ((x ^ y) >> 1);

    /// <summary>
    /// �м�IP
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int GetMidIp(int x, int y)
        => (x & y) + ((x ^ y) >> 1);
}