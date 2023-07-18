#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysPermissionService
// Guid:3f992c48-15e5-4274-8753-dd0f178f8b87
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 03:58:35
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps.Caches;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Permissions.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Permissions.Logic;

/// <summary>
/// 系统权限服务
/// </summary>
[AppService(ServiceType = typeof(ISysPermissionService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysPermissionService : BaseService<SysPermission>, ISysPermissionService
{
    private readonly IAppCacheService _appCacheService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="appCacheService"></param>
    public SysPermissionService(IAppCacheService appCacheService)
    {
        _appCacheService = appCacheService;
    }

    /// <summary>
    /// 校验权限是否唯一
    /// </summary>
    /// <param name="sysPermission"></param>
    /// <returns></returns>
    private async Task<bool> CheckPermissionUnique(SysPermission sysPermission)
    {
        var isUnique = await IsAnyAsync(p => p.PermissionCode == sysPermission.PermissionCode && p.PermissionName == sysPermission.PermissionName);
        if (isUnique) throw new CustomException($"权限【{sysPermission.PermissionName}】已存在!");
        return isUnique;
    }

    /// <summary>
    /// 新增权限
    /// </summary>
    /// <param name="permissionCDto"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<long> CreatePermission(SysPermissionCDto permissionCDto)
    {
        var sysPermission = permissionCDto.Adapt<SysPermission>();

        _ = await CheckPermissionUnique(sysPermission);

        return await AddReturnIdAsync(sysPermission);
    }

    /// <summary>
    /// 批量删除权限
    /// </summary>
    /// <param name="permissionIds"></param>
    /// <returns></returns>
    public async Task<bool> DeletePermissionByIds(long[] permissionIds)
    {
        var permissionList = await QueryAsync(d => permissionIds.Contains(d.BaseId));
        return await RemoveAsync(permissionList);
    }

    /// <summary>
    /// 修改字典数项
    /// </summary>
    /// <param name="permissionCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyPermission(SysPermissionCDto permissionCDto)
    {
        var sysPermission = permissionCDto.Adapt<SysPermission>();

        _ = await CheckPermissionUnique(sysPermission);

        return await UpdateAsync(sysPermission);
    }

    /// <summary>
    /// 查询权限(根据Id)
    /// </summary>
    /// <param name="permissionId"></param>
    /// <returns></returns>
    public async Task<SysPermission> GetPermissionById(long permissionId)
    {
        var key = $"GetPermissionById_{permissionId}";
        if (_appCacheService.Get(key) is SysPermission sysPermission) return sysPermission;
        sysPermission = await FindAsync(d => d.BaseId == permissionId);
        _appCacheService.SetWithMinutes(key, sysPermission, 30);

        return sysPermission;
    }

    /// <summary>
    /// 查询权限列表
    /// </summary>
    /// <param name="permissionWDto"></param>
    /// <returns></returns>
    public async Task<List<SysPermission>> GetPermissionList(SysPermissionWDto permissionWDto)
    {
        var whereExpression = Expressionable.Create<SysPermission>();
        whereExpression.AndIF(permissionWDto.PermissionName.IsNotEmptyOrNull(), u => u.PermissionName.Contains(permissionWDto.PermissionName!));
        whereExpression.AndIF(permissionWDto.PermissionCode.IsNotEmptyOrNull(), u => u.PermissionCode == permissionWDto.PermissionCode);
        whereExpression.AndIF(permissionWDto.PermissionType != null, u => u.PermissionType == permissionWDto.PermissionType);

        return await QueryAsync(whereExpression.ToExpression(), o => o.SortOrder);
    }

    /// <summary>
    /// 查询权限列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysPermission>> GetPermissionPageList(PageWhereDto<SysPermissionWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;

        var whereExpression = Expressionable.Create<SysPermission>();
        whereExpression.AndIF(whereDto.PermissionName.IsNotEmptyOrNull(), u => u.PermissionName.Contains(whereDto.PermissionName!));
        whereExpression.AndIF(whereDto.PermissionCode.IsNotEmptyOrNull(), u => u.PermissionCode == whereDto.PermissionCode);
        whereExpression.AndIF(whereDto.PermissionType != null, u => u.PermissionType == whereDto.PermissionType);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page, o => o.SortOrder);
    }
}