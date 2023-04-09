#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ClientHelper
// Guid:fd74bba3-6e40-4d3f-a365-0a32fb9fb796
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-14 上午 04:56:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using UAParser;

namespace XiHan.Utils.Info.BaseInfos;

/// <summary>
/// 客户端帮助类
/// </summary>
public static class ClientHelper
{
    /// <summary>
    /// 获取客户端信息
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ClientModel GetClient(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        ClientModel clientModel = new()
        {
            RemoteIPv4 = ClientIpHelper.GetClientIpV4(httpContext),
            RemoteIPv6 = ClientIpHelper.GetClientIpV6(httpContext)
        };

        var header = httpContext.Request.HttpContext.Request.Headers;

        if (header.ContainsKey("Accept-Language"))
        {
            clientModel.Language = header["Accept-Language"].ToString().Split(';')[0];
        }
        if (header.ContainsKey("Referer"))
        {
            clientModel.Referer = header["Referer"].ToString();
        }
        if (header.ContainsKey("User-Agent"))
        {
            var agent = header["User-Agent"].ToString();
            var clientInfo = Parser.GetDefault().Parse(agent);
            clientModel.Agent = agent;
            clientModel.OsName = clientInfo.OS.Family;
            if (!string.IsNullOrWhiteSpace(clientInfo.OS.Major))
            {
                clientModel.OsVersion = clientInfo.OS.Major;
                if (!string.IsNullOrWhiteSpace(clientInfo.OS.Minor))
                {
                    clientModel.OsVersion += "." + clientInfo.OS.Minor;
                }
            }
            clientModel.UaName = clientInfo.UA.Family;
            if (!string.IsNullOrWhiteSpace(clientInfo.UA.Major))
            {
                clientModel.UaVersion = clientInfo.UA.Major;
                if (!string.IsNullOrWhiteSpace(clientInfo.UA.Minor))
                {
                    clientModel.UaVersion += "." + clientInfo.UA.Minor;
                }
            }
        }

        return clientModel;
    }

    #region 方法

    /// <summary>
    /// 获取系统名称
    /// </summary>
    /// <param name="agent"></param>
    private static string GetSystemName(string agent)
    {
        var sn = "Unknown";

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
    private static string GetSystemType(string agent)
    {
        var st = "Unknown";
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
        var bn = "Unknown";
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
        var bv = "Unknown";
        return bv;
    }

    #endregion
}

/// <summary>
/// ClientModel
/// </summary>
public class ClientModel
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// 系统名称
    /// </summary>
    public string? OsName { get; set; }

    /// <summary>
    /// 系统版本
    /// </summary>
    public string? OsVersion { get; set; }

    /// <summary>
    /// 浏览器名称
    /// </summary>
    public string? UaName { get; set; }

    /// <summary>
    /// 浏览器版本
    /// </summary>
    public string? UaVersion { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// 引荐
    /// </summary>
    public string? Referer { get; set; }

    /// <summary>
    /// 代理信息
    /// </summary>
    public string? Agent { get; set; }

    /// <summary>
    /// 远程IPv4
    /// </summary>
    public string? RemoteIPv4 { get; init; }

    /// <summary>
    /// 远程IPv6
    /// </summary>
    public string? RemoteIPv6 { get; init; }
}