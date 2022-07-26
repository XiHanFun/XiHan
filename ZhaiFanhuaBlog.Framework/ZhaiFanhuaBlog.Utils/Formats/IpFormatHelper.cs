// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IpFormatHelper
// Guid:db7cf586-8602-44c8-ad14-a1aa2ef6df3c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-26 下午 09:16:37
// ----------------------------------------------------------------

using System.Net;

namespace ZhaiFanhuaBlog.Utils.Formats;

/// <summary>
/// IPFormatHelper
/// </summary>
public class IpFormatHelper
{
    /// <summary>
    /// IPAddress转byte[]
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static byte[]? FormatIPAddressToByte(IPAddress? address)
    {
        if (address != null)
            return address.GetAddressBytes();
        return null;
    }

    /// <summary>
    /// IPAddress转String
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static string? FormatIPAddressToString(IPAddress? address)
    {
        if (address != null)
            return address.ToString();
        return null;
    }

    public static string? FormatByteToString(byte[]? bytes)
    {
        if (bytes != null)
            return new IPAddress(bytes).ToString();
        return null;
    }

    /// <summary>
    /// byte[]转IPAddress
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static IPAddress? FormatByteToIPAddress(byte[]? bytes)
    {
        if (bytes != null)
            return new IPAddress(bytes);
        return null;
    }

    /// <summary>
    /// String转IPAddress
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static IPAddress? FormatStringToIPAddress(string? ip)
    {
        if (ip != null)
            return IPAddress.Parse(ip);
        return null;
    }
}