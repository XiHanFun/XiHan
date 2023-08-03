#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysPermissionService
// Guid:749972c3-c4cf-4fba-84fd-19090110211f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 03:58:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Permissions.Dtos;
using XiHan.Utils.Exceptions;

namespace XiHan.Services.Syses.Permissions;

/// <summary>
/// ISysPermissionService
/// </summary>
public interface ISysPermissionService : IBaseService<SysPermission>
{
    /// <summary>
    /// 新增权限
    /// </summary>
    /// <param name="permissionCDto"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    Task<long> CreatePermission(SysPermissionCDto permissionCDto);

    /// <summary>
    /// 批量删除权限
    /// </summary>
    /// <param name="permissionIds"></param>
    /// <returns></returns>
    Task<bool> DeletePermissionByIds(long[] permissionIds);

    /// <summary>
    /// 修改字典数项
    /// </summary>
    /// <param name="permissionCDto"></param>
    /// <returns></returns>
    Task<bool> ModifyPermission(SysPermissionCDto permissionCDto);

    /// <summary>
    /// 查询权限(根据Id)
    /// </summary>
    /// <param name="permissionId"></param>
    /// <returns></returns>
    Task<SysPermission> GetPermissionById(long permissionId);

    /// <summary>
    /// 查询权限列表
    /// </summary>
    /// <param name="whereDto"></param>
    /// <returns></returns>
    Task<List<SysPermission>> GetPermissionList(SysPermissionWDto whereDto);

    /// <summary>
    /// 查询权限列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysPermission>> GetPermissionPageList(PageWhereDto<SysPermissionWDto> pageWhere);
}