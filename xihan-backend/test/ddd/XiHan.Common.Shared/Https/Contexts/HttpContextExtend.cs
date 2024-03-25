#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpContextExtension
// Guid:25cea3c6-9d44-4179-a38d-c31bd79f569d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/2/23 12:00:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using IP2Region.Net.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net;
using System.Security.Claims;
using System.Text;
using UAParser;
using XiHan.Common.Shared.Attributes;
using XiHan.Common.Shared.Consts;
using XiHan.Common.Shared.Https.Dtos;
using XiHan.Common.Shared.Https.Enums;
using XiHan.Common.Utilities.Exceptions;
using XiHan.Common.Utilities.Extensions;

using XiHan.Common.Utilities.Verifications;

namespace XiHan.Common.Shared.Https.Contexts;

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
    public static UserClientInfoDto GetClientInfo(this HttpContext context)
    {
        try
        {
            var header = context.Request.Headers;
            _ = header.TryGetValue("Accept-Language", out var language);
            _ = header.TryGetValue("Referer", out var referer);
            _ = header.TryGetValue("User-Agent", out var agent);
            var clientInfo = Parser.GetDefault().Parse(agent);

            UserClientInfoDto clientModel = new()
            {
                IsAjaxRequest = context.IsAjaxRequest(),
                Language = language.ToString().Split(';')[0],
                Referer = referer.ToString(),
                Agent = agent.ToString(),
                DeviceType = clientInfo.Device.Family,
                OsName = clientInfo.OS.Family,
                OsVersion = (clientInfo.OS.Major ?? "0") + "." + (clientInfo.OS.Minor ?? "0") + "." + (clientInfo.OS.Patch ?? "0") + "." + (clientInfo.OS.PatchMinor ?? "0"),
                BrowserName = clientInfo.UA.Family,
                BrowserVersion = (clientInfo.UA.Major ?? "0") + "." + (clientInfo.UA.Minor ?? "0") + "." + (clientInfo.UA.Patch ?? "0")
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
                            where headers.Key.Equals("X-Requested-With", StringComparison.CurrentCultureIgnoreCase)
                            select headers.Value).FirstOrDefault();
        return !string.IsNullOrWhiteSpace(stringValues) && stringValues.ToString() == "XMLHttpRequest";
    }

    /// <summary>
    /// 是否是 json 请求
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool IsJsonRequest(this HttpContext context)
    {
        var stringValues = (from headers in context.Request.Headers
                            where headers.Key.Equals("content-type", StringComparison.CurrentCultureIgnoreCase)
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
                            where headers.Key.Equals("accept", StringComparison.CurrentCultureIgnoreCase)
                            select headers.Value).FirstOrDefault();
        return !string.IsNullOrWhiteSpace(stringValues) && stringValues.ToString().Contains("text/html");
    }

    /// <summary>
    /// 验证当前上下文响应内容是否是导出文件
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool IsDownloadFile(this HttpContext context)
    {
        return context.Response.Headers.ContentDisposition.ToString().StartsWith("attachment; filename=");
    }

    /// <summary>
    /// 通过 httpcontext 下载文件
    /// </summary>
    /// <param name="context"></param>
    /// <param name="fileExportName">导出名称</param>
    /// <param name="path">完整文件路径</param>
    /// <param name="contentType">文件的 MIME 类型</param>
    public static async Task DownloadFile(this HttpContext context, string fileExportName, string path, HttpResponseContentTypeEnum contentType)
    {
        context.Response.Headers.Append("Access-Control-Expose-Headers", "Content-Disposition");
        context.Response.Headers.Append("Content-Disposition", "attachment; filename=" + fileExportName.UrlEncode());
        context.Response.ContentType = contentType.GetValue();
        // 创建文件流
        using FileStream fileStream = new(path, FileMode.Open);
        using MemoryStream memoryStream = new();
        fileStream.CopyTo(memoryStream);
        _ = await context.Response.BodyWriter.WriteAsync(memoryStream.ToArray());
        _ = await context.Response.BodyWriter.FlushAsync();
    }

    /// <summary>
    /// 通过 httpcontext 下载文件
    /// </summary>
    /// <param name="context"></param>
    /// <param name="fileExportName">导出名称</param>
    /// <param name="fileContents">文件内容</param>
    /// <param name="contentType">文件的 MIME 类型</param>
    public static async Task DownloadFile(this HttpContext context, string fileExportName, byte[] fileContents, HttpResponseContentTypeEnum contentType)
    {
        context.Response.Headers.Append("Access-Control-Expose-Headers", "Content-Disposition");
        context.Response.Headers.Append("Content-Disposition", "attachment; filename=" + fileExportName.UrlEncode());
        context.Response.ContentType = contentType.GetValue();
        _ = await context.Response.BodyWriter.WriteAsync(fileContents);
        _ = await context.Response.BodyWriter.FlushAsync();
    }

    /// <summary>
    /// 设置 Cookie 值
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
    /// 读取 Cookie 值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="key">名称</param>
    /// <returns>返回的cookies</returns>
    public static string? GetCookie(this HttpContext context, string key)
    {
        return context.Request.Cookies[key];
    }

    /// <summary>
    /// 删除 Cookie 对象
    /// </summary>
    /// <param name="context"></param>
    /// <param name="key">名称</param>
    public static void RemoveCookie(this HttpContext context, string key)
    {
        context.Response.Cookies.Delete(key);
    }

    #endregion

    #region 地址信息

    /// <summary>
    /// 获取地址信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    ///

    public static UserAddressInfoDto GetAddressInfo(this HttpContext context)
    {
        try
        {
            var ip = context.GetClientIpV4();
            if (ip.IsNullOrEmpty()) throw new ArgumentException(nameof(ip));

            // 中国|0|浙江省|杭州市|电信
            var searcher = App.GetRequiredService<ISearcher>();
            var searchResult = searcher.Search(ip);
            ArgumentException.ThrowIfNullOrWhiteSpace("Ip地址信息查询未得到结果", searchResult);

            string[] addressArray = searchResult!.Replace('0', '-').Split('|');
            UserAddressInfoDto addressInfo = new()
            {
                RemoteIPv4 = context.GetClientIpV4(),
                RemoteIPv6 = context.GetClientIpV6(),
                AddressInfo = searchResult,
                Country = addressArray[0],
                State = addressArray[2],
                PrefectureLevelCity = addressArray[3],
                DistrictOrCounty = null,
                Operator = addressArray[4],
                PostalCode = null,
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
    public static UserAuthInfoDto GetAuthInfo(this HttpContext context)
    {
        try
        {
            UserAuthInfoDto userAuthInfo = new()
            {
                IsAuthenticated = context.IsAuthenticated(),
                IsSuperAdmin = context.IsSuperAdmin(),
                UserId = context.GetUserId(),
                TenantId = context.GetTenantId(),
                OrgId = context.GetOrgId(),
                Account = context.GetAccount(),
                NickName = context.GetNickName(),
                RealName = context.GetRealName(),
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
    /// 获取登录账户是否为超级管理员
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool IsSuperAdmin(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimConst.IsSuperAdmin) != null &&
               context.User.FindFirstValue(ClaimConst.IsSuperAdmin).ParseToBool();
    }

    /// <summary>
    /// 获取登录用户标识
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static long GetUserId(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimConst.UserId).ParseToLong();
    }

    /// <summary>
    /// 获取登录用户租户标识
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static long GetTenantId(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimConst.TenantId).ParseToLong();
    }

    /// <summary>
    /// 获取登录用户组织机构标识
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static long GetOrgId(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimConst.OrgId).ParseToLong();
    }

    /// <summary>
    /// 获取登录账户名称
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetAccount(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimConst.Account) ?? string.Empty;
    }

    /// <summary>
    /// 获取登录账户姓名
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetRealName(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimConst.RealName) ?? string.Empty;
    }

    /// <summary>
    /// 获取登录账户昵称
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetNickName(this HttpContext context)
    {
        return context.User.FindFirstValue(ClaimConst.NickName) ?? string.Empty;
    }

    /// <summary>
    /// 获取登录账户权限
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserRole(this HttpContext context)
    {
        var roleIds = context.User.FindAll(ClaimConst.UserRole).Select(r => r.Value).ToList();
        return roleIds.GetListStr();
    }

    /// <summary>
    /// 获取登录账户令牌
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserToken(this HttpContext context)
    {
        return context.Request.Headers.Authorization.ToString().Replace(ClaimConst.TokenReplace, string.Empty);
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

    /// <summary>
    /// 设置规范化文档自动登录
    /// </summary>
    /// <param name="context"></param>
    /// <param name="accessToken"></param>
    public static void SigninToSwagger(this HttpContext context, string accessToken)
    {
        // 设置 Swagger 刷新自动授权
        context.Response.Headers["access-token"] = accessToken;
    }

    /// <summary>
    /// 设置规范化文档退出登录
    /// </summary>
    /// <param name="context"></param>
    public static void SignoutToSwagger(this HttpContext context)
    {
        context.Response.Headers["access-token"] = "invalid_token";
    }

    /// <summary>
    /// 设置响应头 Tokens
    /// </summary>
    /// <param name="context"></param>
    /// <param name="accessToken"></param>
    /// <param name="refreshToken"></param>
    public static void SetTokensOfResponseHeaders(this HttpContext context, string accessToken, string? refreshToken)
    {
        context.Response.Headers["access-token"] = accessToken;
        if (refreshToken.IsNotEmptyOrNull()) context.Response.Headers["x-access-token"] = refreshToken;
    }

    #endregion

    #region 控制器信息

    /// <summary>
    /// 获取控制器信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task<UserActionInfoDto> GetActionInfo(this HttpContext context)
    {
        UserActionInfoDto actionInfo = new()
        {
            RequestMethod = context.GetRequestMethod(),
            RequestUrl = context.GetRequestUrl()
        };

        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            // 获取控制器、路由信息
            var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (actionDescriptor != null)
            {
                actionInfo.ControllerName = actionDescriptor.ControllerName;
                actionInfo.ActionName = actionDescriptor.ActionName;
                actionInfo.MethodName = actionDescriptor.MethodInfo.Name;
            }

            // 获取模块信息
            var logAttribute = endpoint.Metadata.GetMetadata<AppLogAttribute>();
            if (logAttribute != null)
            {
                actionInfo.Module = logAttribute.Module;
                actionInfo.BusinessType = logAttribute.BusinessType;
            }

            actionInfo.RequestParameters = await context.GetRequestParameters();
            //actionInfo.ResponseResult = await context.GetResponseResult();
        }

        return actionInfo;
    }

    /// <summary>
    /// 请求方式
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetRequestMethod(this HttpContext context)
    {
        // 获取 HttpRequest 对象
        var httpRequest = context.Request;
        return httpRequest.Method;
    }

    /// <summary>
    /// 获取请求 Url 地址(域名、路径、参数)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetRequestUrl(this HttpContext context)
    {
        // 获取 HttpRequest 对象
        var httpRequest = context.Request;
        var url = httpRequest.Host.Value + httpRequest.Path.Value + httpRequest.QueryString.Value;
        return url;
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
            // 启用请求缓冲
            request.EnableBuffering();
            // 使用异步获取请求实体
            using StreamReader reader = new(request.Body, Encoding.UTF8, true, 1024, true);
            var requestBody = await reader.ReadToEndAsync();
            _ = request.Body.Seek(0, SeekOrigin.Begin);
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
        var response = context.Response;
        // 使用异步获取请求实体
        using StreamReader reader = new(response.Body, Encoding.UTF8, true, 1024, true);
        var requestBody = await reader.ReadToEndAsync();
        _ = response.Body.Seek(0, SeekOrigin.Begin);
        // 为空则取请求字符串里的参数
        var responseResult = requestBody.IsEmptyOrNull() ? string.Empty : requestBody;
        return responseResult;
    }

    #endregion
}