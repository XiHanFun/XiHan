#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysPermissionService
// Guid:3f992c48-15e5-4274-8753-dd0f178f8b87
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 03:58:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Menus;
using XiHan.Services.Syses.Roles;

namespace XiHan.Services.Syses.Permissions.Logic;

/// <summary>
/// 系统权限服务
/// </summary>
[AppService(ServiceType = typeof(ISysPermissionService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysPermissionService : BaseService<SysPermission>, ISysPermissionService
{
    private readonly ISysRoleService _sysRoleService;
    private readonly ISysMenuService _sysMenuService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysRoleService"></param>
    /// <param name="sysMenuService"></param>
    public SysPermissionService(ISysRoleService sysRoleService, ISysMenuService sysMenuService)
    {
        _sysRoleService = sysRoleService;
        _sysMenuService = sysMenuService;
    }

    /// <summary>
    /// 获取角色权限
    /// </summary>
    /// <param name="user">用户信息</param>
    /// <returns>角色权限信息</returns>
    public List<string> GetRolePermission(SysUser user)
    {
        var roles = new List<string>();
        // 管理员拥有所有权限
        if (user.IsAdmin())
        {
            roles.Add("admin");
        }
        else
        {
            // roles.AddRange(_sysRoleService.GetRoleIdsByUserId(user.BaseId));
        }
        return roles;
    }

    /// <summary>
    /// 获取菜单权限
    /// </summary>
    /// <param name="user">用户信息</param>
    /// <returns>菜单权限信息</returns>
    public List<string> GetMenuPermission(SysUser user)
    {
        var menus = new List<string>();
        return menus;
    }
}