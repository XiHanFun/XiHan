#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HttpContextHelper
// Guid:07ebcfda-13ac-4019-a8ba-b03f21d6a8c2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 上午 01:51:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using System.Net;
using ZhaiFanhuaBlog.Utils.Formats;
using ZhaiFanhuaBlog.Utils.Ip;

namespace ZhaiFanhuaBlog.Utils.Info;

/// <summary>
/// HttpContextHelper
/// </summary>
public class HttpContextHelper
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpContext"></param>
    public HttpContextHelper(HttpContext httpContext)
    {
        var header = httpContext.Request.HttpContext.Request.Headers;
        RemoteIPv4 = httpContext.Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();
        RemoteIPv6 = httpContext.Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv6()?.ToString();

        if (httpContext.Request.HttpContext.Connection.RemoteIpAddress != null)
        {
            RemoteIPv4 = httpContext.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            RemoteIPv6 = httpContext.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
        }
        else
        {
            // 取代理IP
            if (header.ContainsKey("X-Real-IP") | header.ContainsKey("X-Forwarded-For"))
            {
                string? ip = header["X-Real-IP"].FirstOrDefault() ?? header["X-Forwarded-For"].FirstOrDefault();
                if (ip != null)
                {
                    IPAddress address = IpFormatHelper.FormatStringToIPAddress(ip);
                    RemoteIPv4 = address.MapToIPv4().ToString();
                    RemoteIPv6 = address.MapToIPv6().ToString();
                }
            }
        }
        if (header.ContainsKey("Accept-Language"))
        {
            Language = header["Accept-Language"].ToString().Split(';')[0];
        }
        if (header.ContainsKey("Referer"))
        {
            Referer = header["Referer"].ToString();
        }

        var agent = header["User-Agent"].ToString().ToLower();
        Agent = agent;
        SystemName = GetSystemName(agent);
        SystemType = GetSystemType(agent);
        BrowserName = GetBrowserName(agent);
        BrowserVersion = GetBrowserVersion(agent);
        AddressInfo = IpSearchHelper.Search(RemoteIPv4 ?? string.Empty);
    }

    /// <summary>
    /// 代理信息
    /// </summary>
    public string? Agent { get; set; }

    /// <summary>
    /// 远程IPv4
    /// </summary>
    public string? RemoteIPv4 { get; set; }

    /// <summary>
    /// 远程IPv6
    /// </summary>
    public string? RemoteIPv6 { get; set; }

    /// <summary>
    /// 浏览器名称
    /// </summary>
    public string? BrowserName { get; set; }

    /// <summary>
    /// 浏览器版本
    /// </summary>
    public string? BrowserVersion { get; set; }

    /// <summary>
    /// 系统名称
    /// </summary>
    public string? SystemName { get; set; }

    /// <summary>
    /// 系统类型
    /// </summary>
    public string? SystemType { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// 引荐
    /// </summary>
    public string? Referer { get; set; }

    /// <summary>
    /// 地址信息
    /// </summary>
    public IpAddressModel? AddressInfo { get; set; }

    /// <summary>
    /// 获取系统名称
    /// </summary>
    /// <param name="agent"></param>
    private string GetSystemName(string agent)
    {
        string sn = "Unknown";

        if (agent.Contains("nt 5.0"))
        {
            sn = "Windows 2000";
        }
        else if (agent.Contains("nt 5.1"))
        {
            sn = "Windows XP";
        }
        else if (agent.Contains("nt 5.2"))
        {
            sn = "Windows 2003";
        }
        else if (agent.Contains("nt 6.0"))
        {
            sn = "Windows Vista";
        }
        else if (agent.Contains("nt 6.1"))
        {
            sn = "Windows 7";
        }
        else if (agent.Contains("nt 6.2"))
        {
            sn = "Windows 8";
        }
        else if (agent.Contains("nt 6.3"))
        {
            sn = "Windows 8.1";
        }
        else if (agent.Contains("nt 6.4") || agent.Contains("nt 10.0"))
        {
            sn = "Windows 10";
        }
        else if (agent.Contains("android"))
        {
            sn = "Android";
        }
        else if (agent.Contains("mac os x"))
        {
            sn = "IOS";
        }
        else if (agent.Contains("windows phone"))
        {
            sn = "Windows Phone";
        }
        else if (agent.Contains("unix"))
        {
            sn = "Unix";
        }
        else if (agent.Contains("linux"))
        {
            sn = "Linux";
        }
        else if (agent.Contains("mac"))
        {
            sn = "Mac";
        }
        else if (agent.Contains("sunos"))
        {
            sn = "SunOS";
        }
        return sn;
    }

    /// <summary>
    /// 获取系统类型
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    private string GetSystemType(string agent)
    {
        string st = "Unknown";
        if (agent.Contains("x64"))
            st = "64位";
        else if (agent.Contains("x86"))
            st = "32位";
        return st;
    }

    /// <summary>
    /// 获取浏览器名称
    /// </summary>
    /// <param name="agent"></param>
    private static string GetBrowserName(string agent)
    {
        string bn = "Unknown";
        if (agent.Contains("opera/ucweb/"))
        {
            bn = "UC Opera";
        }
        else if (agent.Contains("openwave/ucweb/"))
        {
            bn = "UCOpenwave";
        }
        else if (agent.Contains("ucweb/"))
        {
            bn = "UC";
        }
        else if (agent.Contains("360se/"))
        {
            bn = "360";
        }
        else if (agent.Contains("metasr/"))
        {
            bn = "搜狗";
        }
        else if (agent.Contains("maxthon/"))
        {
            bn = "遨游";
        }
        else if (agent.Contains("the world/"))
        {
            bn = "世界之窗";
        }
        else if (agent.Contains("tencenttraveler/") || agent.Contains("qqbn/"))
        {
            bn = "腾讯";
        }
        else if (agent.Contains("msie/"))
        {
            bn = "IE";
        }
        else if (agent.Contains("edg/"))
        {
            bn = "Edge";
        }
        else if (agent.Contains("chrome/"))
        {
            bn = "Chrome";
        }
        else if (agent.Contains("safari/"))
        {
            bn = "Safari";
        }
        else if (agent.Contains("firefox/"))
        {
            bn = "Firefox";
        }
        else if (agent.Contains("opera/"))
        {
            bn = "Opera";
        }

        return bn;
    }

    /// <summary>
    /// 获取浏览器版本
    /// </summary>
    /// <param name="agent"></param>
    private static string GetBrowserVersion(string agent)
    {
        string bv = "Unknown";
        return bv;
    }
}