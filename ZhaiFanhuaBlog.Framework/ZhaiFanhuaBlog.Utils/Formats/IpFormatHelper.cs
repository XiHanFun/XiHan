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
/// Ip地址格式化帮助类
/// </summary>
public class IpFormatHelper
{
    /// <summary>
    /// IPAddress转byte[]
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static byte[] FormatIPAddressToByte(IPAddress address)
    {
        return address.GetAddressBytes();
    }

    /// <summary>
    /// IPAddress转String
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static string FormatIPAddressToString(IPAddress address)
    {
        return address.ToString();
    }

    /// <summary>
    /// byte[]转String
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string FormatByteToString(byte[] bytes)
    {
        return new IPAddress(bytes).ToString();
    }

    /// <summary>
    /// byte[]转IPAddress
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static IPAddress FormatByteToIPAddress(byte[] bytes)
    {
        return new IPAddress(bytes);
    }

    /// <summary>
    /// String转IPAddress
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static IPAddress FormatStringToIPAddress(string str)
    {
        return IPAddress.Parse(str);
    }
}