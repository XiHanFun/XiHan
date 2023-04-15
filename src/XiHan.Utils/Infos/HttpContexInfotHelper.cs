#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:HttpContextHelper
// Guid:07ebcfda-13ac-4019-a8ba-b03f21d6a8c2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-16 上午 01:51:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Http;
using XiHan.Utils.Infos.IpLocation;

namespace XiHan.Utils.Infos;

/// <summary>
/// 请求上下文信息帮助类
/// </summary>
public class HttpContexInfotHelper
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpContext"></param>
    public HttpContexInfotHelper(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        ClientInfo = ClientInfoHelper.GetClient(httpContext);
        AddressInfo = IpSearchHelper.Search(ClientInfo.RemoteIPv4 ?? string.Empty);
    }

    /// <summary>
    /// 客户端信息
    /// </summary>
    public ClientInfo? ClientInfo { get; set; }

    /// <summary>
    /// 地址信息
    /// </summary>
    public AddressModel? AddressInfo { get; set; }
}