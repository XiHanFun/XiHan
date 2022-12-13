#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ClientIpInfoHelper
// Guid:41c12201-2bdf-4cdf-ba14-a49aeaf92f9a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-12-14 上午 02:36:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using System.Net;
using ZhaiFanhuaBlog.Utils.Formats;

namespace ZhaiFanhuaBlog.Utils.Info;

/// <summary>
/// 客户端Ip地址
/// </summary>
public static class ClientIpInfoHelper
{
    /// <summary>
    /// 取得客户端IP
    /// </summary>
    /// <returns></returns>
    public static string GetClientIpV4(HttpContext httpContext)
    {
        return ClientIPAddressInfo(httpContext).MapToIPv4().ToString();
    }

    /// <summary>
    /// 取得客户端IP
    /// </summary>
    /// <returns></returns>
    public static string GetClientIpV6(HttpContext httpContext)
    {
        return ClientIPAddressInfo(httpContext).MapToIPv6().ToString();
    }

    /// <summary>
    /// 取得客户端IP
    /// </summary>
    /// <returns></returns>
    public static IPAddress ClientIPAddressInfo(HttpContext httpContext)
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
            // 取代理IP
            if (header.ContainsKey("X-Real-IP") | header.ContainsKey("X-Forwarded-For"))
            {
                result = header["X-Real-IP"].FirstOrDefault() ?? header["X-Forwarded-For"].FirstOrDefault();
            }
        }
        if (result == null || result == string.Empty)
        {
            result = "0.0.0.0";
        }
        return IpFormatHelper.FormatStringToIPAddress(result);
    }
}