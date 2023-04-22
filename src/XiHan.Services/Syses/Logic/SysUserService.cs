#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysUserService
// Guid:2f0d94cc-ae27-4504-94bf-cc835ad307f9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 02:04:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructure.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Logic;

/// <summary>
/// 系统用户服务
/// </summary>
[AppService(ServiceType = typeof(ISysUserService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysUserService : BaseService<SysUser>, ISysUserService
{
    private readonly ISysRoleService RoleService;
    private readonly ISysUserRoleService UserRoleService;
}