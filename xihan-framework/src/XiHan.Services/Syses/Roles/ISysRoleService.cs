#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysRoleService
// Guid:88a1e475-2a27-4620-9692-16f266ebe3db
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 02:09:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Models.Syses;
using XiHan.Services.Bases;

namespace XiHan.Services.Syses.Roles;

/// <summary>
/// ISysRoleService
/// </summary>
public interface ISysRoleService : IBaseService<SysRole>
{
    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<SysRole>> GetUserRolesByUserId(long userId);

    /// <summary>
    /// 获取用户角色主键列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<long>> GetUserRoleIdsByUserId(long userId);
}