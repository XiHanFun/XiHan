#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysRolePermissionService
// Guid:b5cccbed-5554-4d8e-bbd0-0ce7a1f532a1
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-11 上午 11:37:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Roles.Logic;

/// <summary>
/// 系统角色权限服务(为某角色分派权限)
/// </summary>
[AppService(ServiceType = typeof(ISysRolePermissionService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysRolePermissionService : BaseService<SysRolePermission>, ISysRolePermissionService
{
}