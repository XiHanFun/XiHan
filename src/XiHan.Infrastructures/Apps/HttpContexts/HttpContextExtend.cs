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

using IP2Region.Net.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;
using System.Text;
using UAParser;
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
    public static UserClientInfo GetClientInfo(this HttpContext context)
    {
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
    public static bool IsAjaxRequest(this HttpContext context)
    {
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
    public static bool IsJsonRequest(this HttpContext context)
    {
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
    public static bool IsHtmlRequest(this HttpContext context)
    {
        var stringValues = (from headers in context.Request.Headers
                            where headers.Key.ToLower() == "accept".ToLower()
                            select headers.Value).FirstOrDefault();
        return !string.IsNullOrWhiteSpace(stringValues) && stringValues.ToString().Contains("text/html");
    }

    /// <summary>
    /// 验证当前上下文响应内容是否是导出文件
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool IsExportFile(this HttpContext context)
    {
        return context.Response.Headers["Content-Disposition"].ToString().StartsWith("attachment; filename=");
    }

    /// <summary>
    /// 通过 httpcontext 导出文件
    /// </summary>
    /// <param name="context"></param>
    /// <param name="fileExportName"></param>
    /// <param name="fileContents"></param>
    /// <param name="contentType"></param>
    public static async Task ExportFile(this HttpContext context, string fileExportName, byte[] fileContents, ContentTypeEnum contentType)
    {
        context.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
        context.Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileExportName.UrlEncode());
        context.Response.ContentType = contentType.GetValue();
        await context.Response.BodyWriter.WriteAsync(fileContents);
        await context.Response.BodyWriter.FlushAsync();
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
    /// 获取请求Url
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string? GetRequestUrl(this HttpContext context)
    {
        return context.Request.Path.Value;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task<string> GetRequestParameters(this HttpContext context)
    {
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
    public static async Task<string> GetResponseResult(this HttpContext context)
    {
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
    public static UserAddressInfo GetAddressInfo(this HttpContext context)
    {
        try
        {
            var ip = context.GetClientIpV4();
            if (ip.IsNullOrEmpty()) throw new ArgumentException(nameof(ip));

            // 中国|0|浙江省|杭州市|电信
            var searcher = App.GetRequiredService<ISearcher>();
            var searchResult = searcher.Search(ip);
            if (searchResult == null) throw new ArgumentException("Ip地址信息查询出错", nameof(searchResult));

            string[] addressArray = searchResult.Replace('0', '-').Split('|');
            var addressInfo = new UserAddressInfo()
            {
                RemoteIPv4 = context.GetClientIpV4(),
                RemoteIPv6 = context.GetClientIpV6(),
                // 长地址信息
                AddressInfo = searchResult,
                // 国家 中国
                Country = addressArray[0],
                // 省份/自治区/直辖市 浙江省
                State = addressArray[2],
                // 地级市 安顺市
                PrefectureLevelCity = addressArray[3],
                // 区/县 西秀区
                DistrictOrCounty = null,
                // 运营商 联通
                Operator = addressArray[4],
                // 邮政编码 561000
                PostalCode = null,
                // 地区区号 0851
                AreaCode = null
            };
            return addressInfo;
        }
        catch (Exception ex)
        {
            throw new CustomException("获取地址信息出错！", ex);
        }
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
    public static string GetClientIpV4(this HttpContext context)
    {
        return context.GetClientIpAddressInfo().FormatIpToV4String();
    }

    /// <summary>
    /// 取得客户端 IP6
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetClientIpV6(this HttpContext context)
    {
        return context.GetClientIpAddressInfo().FormatIpToV6String();
    }

    /// <summary>
    /// 取得客户端 IP
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IPAddress GetClientIpAddressInfo(this HttpContext context)
    {
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
        return result.FormatIpToAddress();
    }

    #endregion

    #region 权限信息

    /// <summary>
    /// 获取登录用户权限信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static UserAuthInfo GetUserAuthInfo(this HttpContext context)
    {
        try
        {
            var userAuthInfo = new UserAuthInfo
            {
                IsAuthenticated = context.IsAuthenticated(),
                UserId = context.GetUserId(),
                UserAccount = context.GetUserAccount(),
                UserNickName = context.GetUserNickName(),
                UserRealName = context.GetUserRealName(),
                UserRole = context.GetUserRole(),
                UserToken = context.GetUserToken(),
                UserClaims = context.GetUserClaims()
            };
            return userAuthInfo;
        }
        catch (Exception ex)
        {
            throw new CustomException("获取地址信息出错！", ex);
        }
    }

    /// <summary>
    /// 是否已鉴权
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool IsAuthenticated(this HttpContext context)
    {
        return context.User.Identity?.IsAuthenticated ?? false;
    }

    /// <summary>
    /// 获取登录用户 Id
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static long GetUserId(this HttpContext context)
    {
        return context.User.FindFirstValue("UserId").ParseToLong();
    }

    /// <summary>
    /// 获取登录账户名称
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserAccount(this HttpContext context)
    {
        return context.User.FindFirstValue("UserAccount") ?? string.Empty;
    }

    /// <summary>
    /// 获取登录账户姓名
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserRealName(this HttpContext context)
    {
        return context.User.FindFirstValue("UserRealName") ?? string.Empty;
    }

    /// <summary>
    /// 获取登录账户昵称
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserNickName(this HttpContext context)
    {
        return context.User.FindFirstValue("UserNickName") ?? string.Empty;
    }

    /// <summary>
    /// 获取登录账户权限
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserRole(this HttpContext context)
    {
        var roleIds = context.User.FindAll("UserRole").Select(r => r.Value).ToList();
        return roleIds.GetListStr(',');
    }

    /// <summary>
    /// 获取登录账户令牌
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserToken(this HttpContext context)
    {
        return context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
    }

    /// <summary>
    /// ClaimsIdentity
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IEnumerable<ClaimsIdentity> GetUserClaims(this HttpContext context)
    {
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
    public bool IsAjaxRequest { get; set; }

    /// <summary>
    /// 请求方式
    /// </summary>
    public string RequestMethod { get; set; } = string.Empty;

    /// <summary>
    /// 请求地址
    /// </summary>
    public string? RequestUrl { get; set; } = string.Empty;

    /// <summary>
    /// 语言
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// 引荐
    /// </summary>
    public string Referer { get; set; } = string.Empty;

    /// <summary>
    /// 代理信息
    /// </summary>
    public string Agent { get; set; } = string.Empty;

    /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称
    /// </summary>
    public string OsName { get; set; } = string.Empty;

    /// <summary>
    /// 系统版本
    /// </summary>
    public string OsVersion { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器名称
    /// </summary>
    public string BrowserName { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器版本
    /// </summary>
    public string BrowserVersion { get; set; } = string.Empty;
}

/// <summary>
/// 地址信息
/// </summary>
public class UserAddressInfo
{
    /// <summary>
    /// 远程IPv4
    /// </summary>
    public string RemoteIPv4 { get; set; } = string.Empty;

    /// <summary>
    /// 远程IPv6
    /// </summary>
    public string RemoteIPv6 { get; set; } = string.Empty;

    /// <summary>
    /// 长地址
    /// </summary>
    public string AddressInfo { get; set; } = string.Empty;

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
    /// 是否已鉴权
    /// </summary>
    public bool IsAuthenticated { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string UserAccount { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string UserNickName { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    public string? UserRealName { get; set; }

    /// <summary>
    /// 用户权限
    /// </summary>
    public string UserRole { get; set; } = string.Empty;

    /// <summary>
    /// 请求令牌
    /// </summary>
    public string UserToken { get; set; } = string.Empty;

    /// <summary>
    /// ClaimsIdentity
    /// </summary>
    public IEnumerable<ClaimsIdentity> UserClaims { get; set; } = Enumerable.Empty<ClaimsIdentity>();
}