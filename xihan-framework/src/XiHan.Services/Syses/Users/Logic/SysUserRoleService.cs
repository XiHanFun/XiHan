#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserRoleService
// Guid:75cbea45-f917-4632-9e78-2e8820ccd424
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-23 上午 02:26:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Users.Logic;

/// <summary>
/// 系统用户角色关联服务
/// </summary>
[AppService(ServiceType = typeof(ISysUserRoleService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysUserRoleService : BaseService<SysUserRole>, ISysUserRoleService
{
    /// <summary>
    /// 新增用户角色
    /// </summary>
    /// <param name="sysUserRole"></param>
    /// <returns></returns>
    public async Task<bool> CreateUserRole(SysUserRole sysUserRole)
    {
        return await AddAsync(sysUserRole);
    }

    /// <summary>
    /// 批量新增用户角色
    /// </summary>
    /// <param name="sysUserRoles"></param>
    /// <returns></returns>
    public async Task<bool> CreateUserRoles(List<SysUserRole> sysUserRoles)
    {
        return await AddAsync(sysUserRoles);
    }

    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUserRoleByUserId(long userId)
    {
        return await DeleteAsync(it => it.UserId == userId);
    }

    /// <summary>
    /// 批量删除某角色下对应的选定用户
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="userIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteRoleUserByUserIds(long roleId, List<long> userIds)
    {
        return await DeleteAsync(it => it.RoleId == roleId && userIds.Contains(it.UserId));
    }

    /// <summary>
    /// 获取所属角色的用户数据
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<List<SysUser>> GetSysUsersByRoleId(long roleId)
    {
        return await Context.Queryable<SysUserRole>()
            .LeftJoin<SysUser>((ur, u) => ur.UserId == u.BaseId)
            .Where((ur, u) => ur.RoleId == roleId && !u.IsDeleted)
            .Select((ur, u) => u)
            .ToListAsync();
    }

    /// <summary>
    /// 获取所属角色的用户总数量
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<int> GetSysUsersCountByRoleId(long roleId)
    {
        return await CountAsync(ur => ur.RoleId == roleId);
    }
}