#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ClientIpHelper
// Guid:41c12201-2bdf-4cdf-ba14-a49aeaf92f9a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-14 上午 02:36:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using System.Net;
using XiHan.Utils.Formats;

namespace XiHan.Commons.Infos.BaseInfos;

/// <summary>
/// 客户端 Ip 帮助类
/// </summary>
public static class ClientIpHelper
{
    /// <summary>
    /// 取得客户端 IP
    /// </summary>
    /// <returns></returns>
    public static string GetClientIpV4(HttpContext httpContext)
    {
        return ClientIpAddressInfo(httpContext).MapToIPv4().ToString();
    }

    /// <summary>
    /// 取得客户端 IP
    /// </summary>
    /// <returns></returns>
    public static string GetClientIpV6(HttpContext httpContext)
    {
        return ClientIpAddressInfo(httpContext).MapToIPv6().ToString();
    }

    /// <summary>
    /// 取得客户端 IP
    /// </summary>
    /// <returns></returns>
    public static IPAddress ClientIpAddressInfo(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        var result = "0.0.0.0";
        var request = httpContext.Request;
        var context = request.HttpContext;
        var header = request.Headers;

        if (context.Connection.RemoteIpAddress != null)
        {
            result = context.Connection.RemoteIpAddress.ToString();
        }
        else
        {
            // 取代理 IP
            if (header.ContainsKey("X-Real-IP") | header.ContainsKey("X-Forwarded-For"))
            {
                result = header["X-Real-IP"].FirstOrDefault() ?? header["X-Forwarded-For"].FirstOrDefault();
            }
        }
        if (string.IsNullOrEmpty(result))
        {
            result = "0.0.0.0";
        }
        return result.FormatStringToIpAddress();
    }
}