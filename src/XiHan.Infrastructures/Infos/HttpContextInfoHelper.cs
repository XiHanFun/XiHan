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

using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.HttpContexts;

namespace XiHan.Infrastructures.Infos;

/// <summary>
/// 请求上下文信息帮助类
/// </summary>
public static class HttpContextInfoHelper
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public static UserClientInfo ClientInfo => App.HttpContextCurrent.GetClientInfo();

    /// <summary>
    /// 地址信息
    /// </summary>
    public static UserAddressInfo AddressInfo => App.HttpContextCurrent.GetAddressInfo();

    /// <summary>
    /// 权限信息
    /// </summary>
    public static UserAuthInfo AuthInfo => App.HttpContextCurrent.GetUserAuthInfo();
}