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
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using UAParser;
using XiHan.Infrastructures.Apps.Logging;
using XiHan.Infrastructures.Consts;
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
    public static bool IsDownloadFile(this HttpContext context)
    {
        return context.Response.Headers["Content-Disposition"].ToString().StartsWith("attachment; filename=");
    }

    /// <summary>
    /// 通过 httpcontext 下载文件
    /// </summary>
    /// <param name="context"></param>
    /// <param name="fileExportName"></param>
    /// <param name="fileContents"></param>
    /// <param name="contentType"></param>
    public static async Task DownloadFile(this HttpContext context, string fileExportName, byte[] fileContents, ContentTypeEnum contentType)
    {
        context.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
        context.Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileExportName.UrlEncode());
        context.Response.ContentType = contentType.GetValue();
        await context.Response.BodyWriter.WriteAsync(fileContents);
        await context.Response.BodyWriter.FlushAsync();
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
    public static UserAuthInfo GetAuthInfo(this HttpContext context)
    {
        try
        {
            var userAuthInfo = new UserAuthInfo
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
        return context.User.FindFirstValue(ClaimConst.IsSuperAdmin) == null ? false : context.User.FindFirstValue(ClaimConst.IsSuperAdmin).ParseToBool();
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
        return roleIds.GetListStr(',');
    }

    /// <summary>
    /// 获取登录账户令牌
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetUserToken(this HttpContext context)
    {
        return context.Request.Headers["Authorization"].ToString().Replace(ClaimConst.TokenReplace, string.Empty);
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

    #region 控制器信息

    /// <summary>
    /// 获取控制器信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static async Task<UserActionInfo> GetActionInfo(this HttpContext context)
    {
        var actionInfo = new UserActionInfo
        {
            RequestMethod = context.GetRequestMethod(),
            RequestUrl = context.GetRequestUrl(),
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
                actionInfo.BusinessType = logAttribute.BusinessType.GetEnumValueByKey();
            }

            actionInfo.RequestParameters = await context.GetRequestParameters();
            actionInfo.ResponseResult = await context.GetResponseResult();
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
        return url ?? string.Empty;
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
            using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);
            var requestBody = await reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
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
        //var response = context.Response;
        //// 使用异步获取请求实体
        //using var reader = new StreamReader(response.Body, Encoding.UTF8, true, 1024, true);
        //var requestBody = await reader.ReadToEndAsync();
        //response.Body.Seek(0, SeekOrigin.Begin);
        //// 为空则取请求字符串里的参数
        //responseResult = requestBody.IsEmptyOrNull() ? string.Empty : requestBody;
        return responseResult;
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
    /// 语言
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// 来源页面
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
    /// 国家 中国
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// 省份/自治区/直辖市 贵州
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// 地级市 安顺
    /// </summary>
    public string? PrefectureLevelCity { get; set; }

    /// <summary>
    /// 区/县 西秀区
    /// </summary>
    public string? DistrictOrCounty { get; set; }

    /// <summary>
    /// 运营商 联通
    /// </summary>
    public string? Operator { get; set; }

    /// <summary>
    /// 邮政编码 561000
    /// </summary>
    public long? PostalCode { get; set; }

    /// <summary>
    /// 地区区号 0851
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
    /// 是否为超级管理员
    /// </summary>
    public bool IsSuperAdmin { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 租户标识
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 组织机构标识
    /// </summary>
    public long OrgId { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    public string RealName { get; set; } = string.Empty;

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

/// <summary>
/// 控制器信息
/// </summary>
public class UserActionInfo
{
    /// <summary>
    /// 请求方式
    /// </summary>
    public string RequestMethod { get; set; } = string.Empty;

    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; set; } = string.Empty;

    /// <summary>
    /// 请求参数
    /// </summary>
    public string RequestParameters { get; set; } = string.Empty;

    /// <summary>
    /// 响应结果
    /// </summary>
    public string ResponseResult { get; set; } = string.Empty;

    /// <summary>
    /// 控制器名称
    /// </summary>
    public string ControllerName { get; set; } = string.Empty;

    /// <summary>
    /// 操作名称
    /// </summary>
    public string ActionName { get; set; } = string.Empty;

    /// <summary>
    /// 方法名称
    /// </summary>
    public string MethodName { get; set; } = string.Empty;

    /// <summary>
    /// 操作模块
    ///</summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型
    /// BusinessTypeEnum
    /// 0其它 1新增 2修改 3删除 4授权 5导出 6导入 7强退 8生成代码 9清空数据
    /// </summary>
    public int BusinessType { get; set; }
}