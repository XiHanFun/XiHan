#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:HttpContextExtension
// Guid:61d55324-ab83-4df1-a500-e076d5b6cd89
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-13 下午 09:01:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Security.Claims;
using System.Text;
using UAParser;
using XiHan.Infrastructures.Infos.IpLocation;
using XiHan.Infrastructures.Requests.Https;
using XiHan.Utils.Extensions;
using XiHan.Utils.Verifications;

namespace XiHan.Infrastructures.Apps.HttpContexts;

/// <summary>
/// 请求上下文拓展
/// </summary>
public static class HttpContextExtend
{
    #region 客户端信息

    /// <summary>
    /// 获取客户端信息
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static UserClientInfo GetClientInfo(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var requestMethod = httpContext.Request.Method;
        var header = httpContext.Request.HttpContext.Request.Headers;
        header.TryGetValue("Accept-Language", out var language);
        header.TryGetValue("Referer", out var referer);
        header.TryGetValue("User-Agent", out var agent);
        var clientInfo = Parser.GetDefault().Parse(agent);

        var clientModel = new UserClientInfo
        {
            IsAjaxRequest = httpContext.IsAjaxRequest(),
            RequestMethod = typeof(RequestMethodEnum).GetEnumInfos().First(m => m.Key.ToLowerInvariant() == requestMethod.ToLowerInvariant()).Value,
            RequestUrl = httpContext.GetRequestUrl(),
            QueryString = httpContext.GetQueryString(requestMethod),
            Language = language.ToString().Split(';')[0],
            Referer = referer.ToString(),
            Agent = agent.ToString(),
            DeviceType = clientInfo.Device.Family,
            OsName = clientInfo.OS.Family,
            OsVersion = (clientInfo.OS.Major ?? "0") + "." + (clientInfo.OS.Minor ?? "0") + "." + (clientInfo.OS.Patch ?? "0") + "." + (clientInfo.OS.PatchMinor ?? "0"),
            UaName = clientInfo.UA.Family,
            UaVersion = (clientInfo.UA.Major ?? "0") + "." + (clientInfo.UA.Minor ?? "0") + "." + (clientInfo.UA.Patch ?? "0"),
            RemoteIPv4 = httpContext.GetClientIpV4(),
            RemoteIPv6 = httpContext.GetClientIpV6(),
        };
        return clientModel;
    }

    /// <summary>
    /// 是否是 ajax 请求
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsAjaxRequest(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var request = httpContext.Request;
        return request.Headers["X-Requested-With"] == "XMLHttpRequest" || request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }

    /// <summary>
    /// 判断是否IP
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIp(string ip)
    {
        return RegexHelper.IsIpRegex(ip);
    }

    /// <summary>
    /// 取得客户端 IP4
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetClientIpV4(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        return httpContext.GetClientIpAddressInfo().MapToIPv4().ToString();
    }

    /// <summary>
    /// 取得客户端 IP6
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetClientIpV6(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        return httpContext.GetClientIpAddressInfo().MapToIPv6().ToString();
    }

    /// <summary>
    /// 取得客户端 IP
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IPAddress GetClientIpAddressInfo(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

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
                result = header["X-Real-IP"].FirstOrDefault() ?? header["X-Forwarded-For"].FirstOrDefault();
        }

        if (string.IsNullOrEmpty(result)) result = "0.0.0.0";
        return result.FormatStringToIpAddress();
    }

    /// <summary>
    /// 获取请求Url
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetRequestUrl(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        return httpContext.Request.Path.Value;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetQueryString(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        return httpContext.Request.QueryString.Value;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetQueryString(this HttpContext? httpContext, string method)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        string? param = string.Empty;
        if (HttpMethods.IsPost(method) || HttpMethods.IsPut(method) || HttpMethods.IsDelete(method))
        {
            using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);
            // 先获取请求实体，需要使用异步方式才能获取
            param = reader.ReadToEndAsync().Result;
            // 为空则取请求字符串里的参数
            if (param.IsEmptyOrNull())
            {
                param = GetQueryString(httpContext);
            }
        }
        else
        {
            param = GetQueryString(httpContext);
        }
        return param;
    }

    #endregion

    #region 地址信息

    /// <summary>
    /// 获取地址信息
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static UserAddressInfo GetAddressInfo(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var addressInfo = new UserAddressInfo();
        var addressInfoResult = IpSearchHelper.Search(httpContext.GetClientIpV4());
        if (addressInfoResult != null) addressInfo = addressInfoResult;
        return addressInfo;
    }

    #endregion

    #region 权限信息

    /// <summary>
    /// 获取登录用户权限信息
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static UserAuthInfo GetUserAuthInfo(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var userAuthInfo = new UserAuthInfo
        {
            UserId = httpContext.GetUserId(),
            UserName = httpContext.GetUserName(),
            UserRole = httpContext.GetUserRole(),
            UserToken = httpContext.GetUserToken(),
            IsAdmin = httpContext.IsAdmin(),
            Claims = httpContext.GetClaims()
        };
        return userAuthInfo;
    }

    /// <summary>
    /// 获取登录用户 Id
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static long? GetUserId(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var uid = httpContext.User.FindFirstValue(ClaimTypes.PrimarySid);
        return uid.ParseToLong();
    }

    /// <summary>
    /// 获取登录用户名
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetUserName(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var uname = httpContext.User.Identity?.Name;
        return uname;
    }

    /// <summary>
    /// 获取登录用户权限
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetUserRole(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var roleId = httpContext.User.FindFirstValue(ClaimTypes.Role);
        return roleId.ParseToString();
    }

    /// <summary>
    /// 获取请求令牌
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetUserToken(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        return httpContext.Request.Headers["Authorization"].ToString();
    }

    /// <summary>
    /// 判断是否是管理员
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsAdmin(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var userName = httpContext.GetUserName();
        return userName == AppGlobalConstant.AdminRole;
    }

    /// <summary>
    /// ClaimsIdentity
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<ClaimsIdentity> GetClaims(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        return httpContext.User.Identities;
    }

    #endregion

    #region 特性信息

    /// <summary>
    /// 获取终结点
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Endpoint? GetEndpoint(this HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        return httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
    }

    #endregion
}

/// <summary>
/// 客户端信息
/// </summary>
public class UserClientInfo
{
    /// <summary>
    /// 是否是 ajax 请求
    /// </summary>
    public bool? IsAjaxRequest { get; set; }

    /// <summary>
    /// 请求方式
    /// </summary>
    public int? RequestMethod { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string? RequestUrl { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public string? QueryString { get; set; }

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
    /// 远程IPv4
    /// </summary>
    public string? RemoteIPv4 { get; set; }

    /// <summary>
    /// 远程IPv6
    /// </summary>
    public string? RemoteIPv6 { get; set; }
}

/// <summary>
/// 地址信息
/// </summary>
public class UserAddressInfo
{
    /// <summary>
    /// 国家
    /// 中国
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// 省份/自治区/直辖市
    /// 贵州
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// 地级市
    /// 安顺
    /// </summary>
    public string? PrefectureLevelCity { get; set; }

    /// <summary>
    /// 区/县
    /// 西秀区
    /// </summary>
    public string? DistrictOrCounty { get; set; }

    /// <summary>
    /// 运营商
    /// 联通
    /// </summary>
    public string? Operator { get; set; }

    /// <summary>
    /// 邮政编码
    /// 561000
    /// </summary>
    public long? PostalCode { get; set; }

    /// <summary>
    /// 地区区号
    /// 0851
    /// </summary>
    public int? AreaCode { get; set; }
}

/// <summary>
/// 权限信息
/// </summary>
public class UserAuthInfo
{
    /// <summary>
    /// 用户 Id
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 用户权限
    /// </summary>
    public string? UserRole { get; set; }

    /// <summary>
    /// 请求令牌
    /// </summary>
    public string? UserToken { get; set; }

    /// <summary>
    /// 是否管理员
    /// </summary>
    public bool? IsAdmin { get; set; }

    /// <summary>
    /// ClaimsIdentity
    /// </summary>
    public IEnumerable<ClaimsIdentity>? Claims { get; set; }
}