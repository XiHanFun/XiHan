#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ClientInfoHelper
// Guid:fd74bba3-6e40-4d3f-a365-0a32fb9fb796
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-14 上午 04:56:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using UAParser;
using XiHan.Infrastructures.Infos.BaseInfos;

namespace XiHan.Infrastructures.Infos;

/// <summary>
/// 客户端信息帮助类
/// </summary>
public static class ClientInfoHelper
{
    /// <summary>
    /// 获取客户端信息
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ClientInfo GetClient(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        var clientModel = new ClientInfo
        {
            RemoteIPv4 = ClientIpHelper.GetClientIpV4(httpContext),
            RemoteIPv6 = ClientIpHelper.GetClientIpV6(httpContext),
        };

        var header = httpContext.Request.HttpContext.Request.Headers;

        if (header.TryGetValue("Accept-Language", out var value))
        {
            clientModel.Language = value.ToString().Split(';')[0];
        }
        if (header.TryGetValue("Referer", out var value1))
        {
            clientModel.Referer = value1.ToString();
        }
        if (header.TryGetValue("User-Agent", out var value2))
        {
            var agent = value2.ToString();
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
}

/// <summary>
/// 客户端信息
/// </summary>
public class ClientInfo
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