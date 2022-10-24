// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HttpContextHelper
// Guid:07ebcfda-13ac-4019-a8ba-b03f21d6a8c2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 上午 01:51:36
// ----------------------------------------------------------------

using Microsoft.AspNetCore.Http;

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
        RemoteIPv4 = httpContext.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4()?.ToString();
        RemoteIPv6 = httpContext.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv6()?.ToString();

        var ua = header["User-Agent"].ToString().ToLower();
        Agent = ua;
        SystemName = GetSystemName(ua);
        SystemType = GetSystemType(ua);
        BrowserName = GetBrowserName(ua);

        //取代理IP
        if (header.ContainsKey("X-Real-IP") | header.ContainsKey("X-Forwarded-For"))
        {
            RemoteIPv4 = header["X-Real-IP"].FirstOrDefault() ??
                         header["X-Forwarded-For"].FirstOrDefault();
        }
        if (header.ContainsKey("Accept-Language"))
        {
            Language = header["Accept-Language"].ToString().Split(';')[0];
        }
        if (header.ContainsKey("Referer"))
        {
            Referer = header["Referer"].ToString();
        }
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
    /// 获取系统名称
    /// </summary>
    /// <param name="ua"></param>
    private static string GetSystemName(string ua)
    {
        string sn = "Unknown";

        if (ua.Contains("nt 5.0"))
            sn = "Windows 2000";
        else if (ua.Contains("nt 5.1"))
            sn = "Windows XP";
        else if (ua.Contains("nt 5.2"))
            sn = "Windows 2003";
        else if (ua.Contains("nt 6.0"))
            sn = "Windows Vista";
        else if (ua.Contains("nt 6.1"))
            sn = "Windows 7";
        else if (ua.Contains("nt 6.2"))
            sn = "Windows 8";
        else if (ua.Contains("nt 6.3"))
            sn = "Windows 8.1";
        else if (ua.Contains("nt 6.4") || ua.Contains("nt 10.0"))
            sn = "Windows 10";
        else if (ua.Contains("android"))
            sn = "Android";
        else if (ua.Contains("mac os x"))
            sn = "IOS";
        else if (ua.Contains("windows phone"))
            sn = "Windows Phone";
        else if (ua.Contains("unix"))
            sn = "Unix";
        else if (ua.Contains("linux"))
            sn = "Linux";
        else if (ua.Contains("mac"))
            sn = "Mac";
        else if (ua.Contains("sunos"))
            sn = "SunOS";
        return sn;
    }

    /// <summary>
    /// 获取系统类型
    /// </summary>
    /// <param name="ua"></param>
    /// <returns></returns>
    private static string GetSystemType(string ua)
    {
        string st = "Unknown";
        if (ua.Contains("x64"))
            st = "64位";
        else if (ua.Contains("x86"))
            st = "32位";
        return st;
    }

    /// <summary>
    /// 获取浏览器名称
    /// </summary>
    /// <param name="ua"></param>
    private static string GetBrowserName(string ua)
    {
        string bn = "Unknown";
        if (ua.Contains("opera/ucweb"))
            bn = "UC Opera";
        else if (ua.Contains("openwave/ucweb"))
            bn = "UCOpenwave";
        else if (ua.Contains("ucweb"))
            bn = "UC";
        else if (ua.Contains("360se"))
            bn = "360";
        else if (ua.Contains("metasr"))
            bn = "搜狗";
        else if (ua.Contains("maxthon"))
            bn = "遨游";
        else if (ua.Contains("the world"))
            bn = "世界之窗";
        else if (ua.Contains("tencenttraveler") || ua.Contains("qqbn"))
            bn = "腾讯";
        else if (ua.Contains("msie"))
            bn = "IE";
        else if (ua.Contains("edg"))
            bn = "Edge";
        else if (ua.Contains("chrome"))
            bn = "Chrome";
        else if (ua.Contains("safari"))
            bn = "Safari";
        else if (ua.Contains("firefox"))
            bn = "Firefox";
        else if (ua.Contains("opera"))
            bn = "Opera";

        return bn;
    }
}