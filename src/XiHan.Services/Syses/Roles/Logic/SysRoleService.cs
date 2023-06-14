#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysRoleService
// Guid:dc2714b5-bc05-44a5-a93a-99b792065372
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 02:09:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Users;

namespace XiHan.Services.Syses.Roles.Logic;

/// <summary>
/// 系统角色服务
/// </summary>
[AppService(ServiceType = typeof(ISysRoleService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysRoleService : BaseService<SysRole>, ISysRoleService
{
    private ISysUserRoleService _sysUserRoleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysUserRoleService"></param>
    public SysRoleService(ISysUserRoleService sysUserRoleService)
    {
        _sysUserRoleService = sysUserRoleService;
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<SysRole>> GetUserRolesByUserId(long userId)
    {
        return await Context.Queryable<SysUserRole>()
            .LeftJoin<SysRole>((ur, r) => ur.RoleId == r.BaseId)
            .Where((ur, r) => ur.UserId == userId && !r.IsDeleted)
            .Select((ur, r) => r)
            .ToListAsync();
    }

    /// <summary>
    /// 获取用户角色主键列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<long>> GetUserRoleIdsByUserId(long userId)
    {
        var list = await GetUserRolesByUserId(userId);
        return list.Select(r => r.BaseId).ToList();
    }
}