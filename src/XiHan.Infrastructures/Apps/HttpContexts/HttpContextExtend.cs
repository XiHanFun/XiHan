#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpContextExtension
// Guid:61d55324-ab83-4df1-a500-e076d5b6cd89
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-13 下午 09:01:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Security.Claims;
using System.Text;
using UAParser;
using XiHan.Infrastructures.Apps.HttpContexts.IpLocation;
using XiHan.Utils.Exceptions;
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
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static UserClientInfo GetClientInfo(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        try
        {
            var header = context.Request.Headers;
            header.TryGetValue("Accept-Language", out var language);
            header.TryGetValue("Referer", out var referer);
            header.TryGetValue("User-Agent", out var agent);
            var clientInfo = Parser.GetDefault().Parse(agent);

            var clientModel = new UserClientInfo
            {
                IsAjaxRequest = context.IsAjaxRequest(),
                RequestMethod = context.Request.Method.ToUpperInvariant(),
                RequestUrl = context.GetRequestUrl(),
                Language = language.ToString().Split(';')[0],
                Referer = referer.ToString(),
                Agent = agent.ToString(),
                DeviceType = clientInfo.Device.Family,
                OsName = clientInfo.OS.Family,
                OsVersion = (clientInfo.OS.Major ?? "0") + "." + (clientInfo.OS.Minor ?? "0") + "." + (clientInfo.OS.Patch ?? "0") + "." + (clientInfo.OS.PatchMinor ?? "0"),
                BrowserName = clientInfo.UA.Family,
                BrowserVersion = (clientInfo.UA.Major ?? "0") + "." + (clientInfo.UA.Minor ?? "0") + "." + (clientInfo.UA.Patch ?? "0"),
                RemoteIPv4 = context.GetClientIpV4(),
                RemoteIPv6 = context.GetClientIpV6()
            };
            return clientModel;
        }
        catch (Exception ex)
        {
            throw new CustomException("获取客户端信息出错！", ex);
        }
    }

    /// <summary>
    /// 是否是 ajax 请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsAjaxRequest(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var stringValues = (from headers in context.Request.Headers
            where headers.Key.ToLower() == "X-Requested-With".ToLower()
            select headers.Value).FirstOrDefault();
        return !string.IsNullOrWhiteSpace(stringValues) && stringValues.ToString() == "XMLHttpRequest";
    }

    /// <summary>
    ///  是否是 json 请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsJsonRequest(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var stringValues = (from headers in context.Request.Headers
            where headers.Key.ToLower() == "content-type".ToLower()
            select headers.Value).FirstOrDefault();
        return !string.IsNullOrWhiteSpace(stringValues) && stringValues.ToString() == "application/json";
    }

    /// <summary>
    /// 是否为 html 网页请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsHtmlRequest(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var stringValues = (from headers in context.Request.Headers
            where headers.Key.ToLower() == "accept".ToLower()
            select headers.Value).FirstOrDefault();
        return !string.IsNullOrWhiteSpace(stringValues) && stringValues.ToString().Contains("text/html");
    }

    /// <summary>
    /// 通过 httpcontext 下载文件
    /// </summary>
    /// <param name="context"></param>
    /// <param name="fileContents"></param>
    /// <param name="contentType"></param>
    /// <param name="fileDownloadName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void DownLoadFile(this HttpContext? context, byte[] fileContents, string contentType, string fileDownloadName)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        context.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
        context.Response.ContentType = contentType;
        context.Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileDownloadName.UrlEncode());
        context.Response.BodyWriter.WriteAsync(fileContents);
        context.Response.BodyWriter.FlushAsync();
    }

    /// <summary>
    /// 验证当前上下文响应内容是否是下载文件
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool IsDownLoadFile(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        return context.Response.Headers["Content-Disposition"].ToString().StartsWith("attachment; filename=");
    }

    /// <summary>
    /// 设置Cookie值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="key">名称</param>
    /// <param name="value">值</param>
    /// <param name="expires">过期时长</param>
    public static void SetCookie(this HttpContext context, string key, string value, int? expires = null)
    {
        if (!expires.HasValue)
        {
            context.Response.Cookies.Append(key, value);
            return;
        }

        context.Response.Cookies.Append(key, value, new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(Convert.ToDouble(expires))
        });
    }

    /// <summary>
    /// 读取Cookie值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="key">名称</param>
    /// <returns>返回的cookies</returns>
    public static string? GetCookie(this HttpContext context, string key)
    {
        return context.Request.Cookies[key];
    }

    /// <summary>
    /// 删除Cookie对象
    /// </summary>
    /// <param name="context"></param>
    /// <param name="key">名称</param>
    public static void RemoveCookie(this HttpContext context, string key)
    {
        context.Response.Cookies.Delete(key);
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
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetClientIpV4(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        return context.GetClientIpAddressInfo().MapToIPv4().ToString();
    }

    /// <summary>
    /// 取得客户端 IP6
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetClientIpV6(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        return context.GetClientIpAddressInfo().MapToIPv6().ToString();
    }

    /// <summary>
    /// 取得客户端 IP
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IPAddress GetClientIpAddressInfo(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var result = "0.0.0.0";
        var header = context.Request.Headers;

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
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetRequestUrl(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        return context.Request.Path.Value;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task<string> GetRequestParameters(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var requestParameters = string.Empty;
        var request = context.Request;
        var method = request.Method;
        if (HttpMethods.IsPost(method) || HttpMethods.IsPut(method) || HttpMethods.IsPatch(method))
        {
            // 使用异步获取请求实体
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8);
            var requestBody = await reader.ReadToEndAsync();
            // 为空则取请求字符串里的参数
            requestParameters = requestBody.IsEmptyOrNull() ? request.QueryString.Value ?? string.Empty : requestBody;
        }
        else if (HttpMethods.IsGet(method) || HttpMethods.IsDelete(method))
        {
            requestParameters = request.QueryString.Value ?? string.Empty;
        }

        return requestParameters;
    }

    /// <summary>
    /// 获取响应结果
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task<string> GetResponseResult(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var responseResult = string.Empty;
        var response = context.Response;
        // 使用异步获取请求实体
        using var reader = new StreamReader(response.Body, Encoding.UTF8);
        var requestBody = await reader.ReadToEndAsync();
        // 为空则取请求字符串里的参数
        responseResult = requestBody.IsEmptyOrNull() ? string.Empty : requestBody;
        return responseResult;
    }

    #endregion

    #region 地址信息

    /// <summary>
    /// 获取地址信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static UserAddressInfo GetAddressInfo(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        try
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var addressInfo = new UserAddressInfo();
            var addressInfoResult = IpSearchHelper.Search(context.GetClientIpV4());
            if (addressInfoResult != null) addressInfo = addressInfoResult;
            return addressInfo;
        }
        catch (Exception ex)
        {
            throw new CustomException("获取地址信息出错！", ex);
        }
    }

    #endregion

    #region 权限信息

    /// <summary>
    /// 获取登录用户权限信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static UserAuthInfo GetUserAuthInfo(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        try
        {
            var userAuthInfo = new UserAuthInfo
            {
                UserId = context.GetUserId(),
                UserName = context.GetUserName(),
                UserRole = context.GetUserRole(),
                UserToken = context.GetUserToken(),
                //IsAdmin = context.IsAdmin(),
                Claims = context.GetClaims()
            };
            return userAuthInfo;
        }
        catch (Exception ex)
        {
            throw new CustomException("获取地址信息出错！", ex);
        }
    }

    /// <summary>
    /// 获取登录用户 Id
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static long? GetUserId(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var uid = context.User.FindFirstValue(ClaimTypes.PrimarySid);
        return uid.ParseToLong();
    }

    /// <summary>
    /// 获取登录用户名
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string? GetUserName(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var uname = context.User.Identity?.Name;
        return uname;
    }

    /// <summary>
    /// 获取登录用户权限
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetUserRole(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        var roleId = context.User.FindFirstValue(ClaimTypes.Role);
        return roleId.ParseToString();
    }

    /// <summary>
    /// 获取请求令牌
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string GetUserToken(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        return context.Request.Headers["Authorization"].ToString();
    }

    ///// <summary>
    ///// 判断是否是管理员
    ///// </summary>
    ///// <param name="context"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    //public static bool IsAdmin(this HttpContext? context)
    //{
    //    if (context == null) throw new ArgumentNullException(nameof(context));

    //    var userName = context.GetUserName();
    //    return userName == AppGlobalConstant.AdminRole;
    //}

    /// <summary>
    /// ClaimsIdentity
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<ClaimsIdentity> GetClaims(this HttpContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));

        return context.User.Identities;
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
    public string? RequestMethod { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string? RequestUrl { get; set; }

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
    public string? BrowserName { get; set; }

    /// <summary>
    /// 浏览器版本
    /// </summary>
    public string? BrowserVersion { get; set; }

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
    /// 长地址
    /// </summary>
    public string? AddressInfo { get; set; }

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
    /// 用户ID
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