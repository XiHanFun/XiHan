#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserController
// Guid:68036199-f77d-41b5-a629-bc972917fa69
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-19 下午 05:50:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Authorization;
using XiHanCore.Services.Syses.Roles;
using XiHanCore.Services.Syses.Users;
using XiHanCore.WebCore.Common.Swagger;

namespace XiHanCore.WebHost.Controllers.Syses;

/// <summary>
/// 系统用户管理
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="sysUserService"></param>
/// <param name="sysRoleService"></param>
[Authorize]
[ApiGroup(ApiGroupNameEnum.Manage)]
public class SysUserController(ISysUserService sysUserService, ISysRoleService sysRoleService) : BaseApiController
{
    private readonly ISysUserService _sysUserService = sysUserService;
    private readonly ISysRoleService _sysRoleService = sysRoleService;
}