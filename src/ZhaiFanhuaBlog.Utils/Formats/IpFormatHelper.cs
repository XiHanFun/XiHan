#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IpFormatHelper
// Guid:db7cf586-8602-44c8-ad14-a1aa2ef6df3c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-07-26 下午 09:16:37
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.Net;

namespace ZhaiFanhuaBlog.Utils.Formats;

/// <summary>
/// Ip地址格式化帮助类
/// </summary>
public static class IpFormatHelper
{
    /// <summary>
    /// IPAddress转byte[]
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static byte[] FormatIPAddressToByte(this IPAddress address)
    {
        return address.GetAddressBytes();
    }

    /// <summary>
    /// IPAddress转String
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static string FormatIPAddressToString(this IPAddress address)
    {
        return address.ToString();
    }

    /// <summary>
    /// byte[]转String
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string FormatByteToString(this byte[] bytes)
    {
        return new IPAddress(bytes).ToString();
    }

    /// <summary>
    /// byte[]转IPAddress
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static IPAddress FormatByteToIPAddress(this byte[] bytes)
    {
        return new IPAddress(bytes);
    }

    /// <summary>
    /// String转IPAddress
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static IPAddress FormatStringToIPAddress(this string str)
    {
        return IPAddress.Parse(str);
    }

    /// <summary>
    /// ipV4转ipV6
    /// </summary>
    /// <param name="ipV4str"></param>
    /// <returns></returns>
    public static string FormatV4ToV6(this string ipV4str)
    {
        return IPAddress.Parse(ipV4str).MapToIPv6().ToString();
    }

    /// <summary>
    /// ipV4转ipV6
    /// </summary>
    /// <param name="ipV6str"></param>
    /// <returns></returns>
    public static string FormatV6ToV4(this string ipV6str)
    {
        return IPAddress.Parse(ipV6str).MapToIPv4().ToString();
    }
}