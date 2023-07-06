#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:HttpContextHelper
// Guid:07ebcfda-13ac-4019-a8ba-b03f21d6a8c2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-16 上午 01:51:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;

namespace XiHan.Infrastructures.Apps.HttpContexts;

/// <summary>
/// 请求上下文信息帮助类
/// </summary>
public class HttpContextInfoHelper
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpContext"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public HttpContextInfoHelper(HttpContext? httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        ClientInfo = httpContext.GetClientInfo();
        AddressInfo = httpContext.GetAddressInfo();
        AuthInfo = httpContext.GetUserAuthInfo();
    }

    /// <summary>
    /// 客户端信息
    /// </summary>
    public UserClientInfo ClientInfo { get; set; }

    /// <summary>
    /// 地址信息
    /// </summary>
    public UserAddressInfo AddressInfo { get; set; }

    /// <summary>
    /// 权限信息
    /// </summary>
    public UserAuthInfo AuthInfo { get; set; }
}