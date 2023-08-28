#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysOauthService
// Guid:74173f6f-6196-437e-8028-c4caa016002b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-08-16 上午 11:21:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Oauths.Logic;

/// <summary>
/// 系统三方开放授权协议服务
/// </summary>
[AppService(ServiceType = typeof(ISysOauthService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysOauthService : BaseService<SysOauth>, ISysOauthService
{
}