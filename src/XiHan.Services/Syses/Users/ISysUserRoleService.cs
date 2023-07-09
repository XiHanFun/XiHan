#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysUserRoleService
// Guid:a0bd94b7-983d-4169-b378-566d88da86bd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-23 上午 02:25:53
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Users;

/// <summary>
/// ISysUserRoleService
/// </summary>
public interface ISysUserRoleService : IBaseService<SysUserRole>
{
    /// <summary>
    /// 获取所属角色的用户总数量
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<int> GetSysUsersCountByRoleId(long roleId);

    /// <summary>
    /// 获取所属角色的用户数据
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<List<SysUser>> GetSysUsersByRoleId(long roleId);

    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> DeleteUserRoleByUserId(long userId);

    /// <summary>
    /// 批量删除某角色下对应的选定用户
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="userIds"></param>
    /// <returns></returns>
    Task<bool> DeleteRoleUserByUserIds(long roleId, List<long> userIds);

    ///// <summary>
    ///// 新增用户角色信息
    ///// </summary>
    ///// <param name="sysUser"></param>
    ///// <returns></returns>
    //Task<bool> CreateUserRole(SysUser sysUser);

    /// <summary>
    /// 批量新增用户角色
    /// </summary>
    /// <param name="sysUserRoles"></param>
    /// <returns></returns>
    Task<bool> CreateUserRoles(List<SysUserRole> sysUserRoles);

    /// <summary>
    /// 新增用户角色
    /// </summary>
    /// <param name="sysUserRole"></param>
    /// <returns></returns>
    Task<bool> CreateUserRole(SysUserRole sysUserRole);
}