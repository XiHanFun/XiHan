#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysRoleService
// Guid:dc2714b5-bc05-44a5-a93a-99b792065372
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 02:09:39
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Exceptions;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Roles.Dtos;
using XiHan.Services.Syses.Users;

namespace XiHan.Services.Syses.Roles.Logic;

/// <summary>
/// 系统角色服务
/// </summary>
[AppService(ServiceType = typeof(ISysRoleService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysRoleService : BaseService<SysRole>, ISysRoleService
{
    private ISysUserRoleService _sysUserRoleService;
    private ISysRolePermissionService _sysRolePermissionService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sysUserRoleService"></param>
    /// <param name="sysRolePermissionService"></param>
    public SysRoleService(ISysUserRoleService sysUserRoleService, ISysRolePermissionService sysRolePermissionService)
    {
        _sysUserRoleService = sysUserRoleService;
        _sysRolePermissionService = sysRolePermissionService;
    }

    /// <summary>
    /// 校验角色是否唯一
    /// </summary>
    /// <param name="sysRole"></param>
    /// <returns></returns>
    private async Task<bool> CheckRoleUnique(SysRole sysRole)
    {
        var isUnique = await IsAnyAsync(f => f.RoleCode == sysRole.RoleCode && f.RoleName == sysRole.RoleName);
        if (isUnique) throw new CustomException($"角色【{sysRole.RoleName}】已存在!");
        return isUnique;
    }

    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="roleCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateRole(SysRoleCDto roleCDto)
    {
        var sysRole = roleCDto.Adapt<SysRole>();

        _ = await CheckRoleUnique(sysRole);

        return await AddReturnIdAsync(sysRole);
    }

    /// <summary>
    /// 批量删除角色
    /// </summary>
    /// <param name="dictIds"></param>
    /// <returns></returns>
    public async Task<bool> DeleteRoleByIds(long[] dictIds)
    {
        var roles = await QueryAsync(d => dictIds.Contains(d.BaseId));
        if (roles.Any(r => r.RoleCode == AppGlobalConstant.SysRoleAdmin))
        {
            throw new CustomException($"禁止删除系统管理员角色!");
        }

        return await RemoveAsync(roles);
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
            .Where((ur, r) => ur.BaseId == userId && !r.IsDeleted)
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