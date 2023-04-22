#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysUserRoleService
// Guid:75cbea45-f917-4632-9e78-2e8820ccd424
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-23 上午 02:26:40
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructure.Apps.Services;
using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Logic;

/// <summary>
/// 系统用户角色关联服务
/// </summary>
[AppService(ServiceType = typeof(ISysUserRoleService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysUserRoleService : BaseService<SysUserRole>, ISysUserRoleService
{
    /// <summary>
    /// 获取所属角色的用户总数量
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<int> GetSysUsersCountByRoleId(long roleId)
    {
        return await CountAsync(ur => ur.RoleId == roleId);
    }

    /// <summary>
    /// 获取所属角色的用户数据
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public List<SysUser> GetSysUsersByRoleId(long roleId)
    {
        return Context.Queryable<SysUserRole, SysUser>((ur, u) => new JoinQueryInfos(JoinType.Left, ur.UserId == u.BaseId))
            .Where((ur, u) => ur.RoleId == roleId && !u.IsSoftDeleted)
            .Select((ur, u) => u)
            .ToList();
    }

    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUserRoleByUserId(int userId)
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
    /// 添加用户角色
    /// </summary>
    /// <param name="sysUserRoles"></param>
    /// <returns></returns>
    public async Task<bool> CreateUserRoles(List<SysUserRole> sysUserRoles)
    {
        return await AddBatchAsync(sysUserRoles);
    }
}