﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysServerService
// Guid:80d0ea29-2134-446f-9b55-725f9c14a168
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-20 上午 10:58:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Infos;
using XiHan.Services.Syses.Servers.Dtos;

namespace XiHan.Services.Syses.Servers;

/// <summary>
/// 系统服务器监控服务接口
/// </summary>
public interface ISysServerService
{
    /// <summary>
    /// 获取服务器信息
    /// </summary>
    /// <returns></returns>
    ServerInfoRDto GetServerInfo();

    /// <summary>
    /// 获取系统信息
    /// </summary>
    /// <returns></returns>
    SystemInfoHelper GetSystemInfo();

    /// <summary>
    /// 获取环境信息
    /// </summary>
    /// <returns></returns>
    EnvironmentInfoHelper GetEnvironmentInfo();

    /// <summary>
    /// 获取应用信息
    /// </summary>
    /// <returns></returns>
    ApplicationInfoHelper GetApplicationInfo();
}