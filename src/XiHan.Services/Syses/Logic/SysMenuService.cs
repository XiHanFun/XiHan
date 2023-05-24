#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysMenuService
// Guid:69d412ed-50f6-4707-b560-8288ac375e49
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 06:04:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Logic;

/// <summary>
/// 系统菜单服务
/// </summary>
[AppService(ServiceType = typeof(ISysMenuService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysMenuService : BaseService<SysMenu>, ISysMenuService
{
    private readonly ISysRoleService _sysRoleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysRoleService"></param>
    public SysMenuService(ISysRoleService sysRoleService)
    {
        _sysRoleService = sysRoleService;
    }
}