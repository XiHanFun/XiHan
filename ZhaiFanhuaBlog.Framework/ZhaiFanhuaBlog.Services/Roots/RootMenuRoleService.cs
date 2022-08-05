// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootMenuRoleService
// Guid:0c28440b-c5c0-4507-bc91-7b0f0c6f272b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:35:32
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootMenuRoleService
/// </summary>
public class RootMenuRoleService : BaseService<RootMenuRole>, IRootMenuRoleService
{
    private readonly IRootMenuRoleRepository _IRootMenuRoleRepository;
    private readonly IRootMenuService _IRootMenuService;
    private readonly IUserRoleService _IUserRoleService;

    public RootMenuRoleService(IRootMenuRoleRepository iRootMenuRoleRepository,
        IRootMenuService iRootMenuService,
        IUserRoleService iUserRoleService
        )
    {
        base._IBaseRepository = iRootMenuRoleRepository;
        _IRootMenuRoleRepository = iRootMenuRoleRepository;
        _IRootMenuService = iRootMenuService;
        _IUserRoleService = iUserRoleService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootMenuRole> IsExistenceAsync(Guid guid)
    {
        var rootMenuRole = await _IRootMenuRoleRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (rootMenuRole == null)
            throw new ApplicationException("角色菜单不存在");
        return rootMenuRole;
    }

    public async Task<bool> InitRootMenuRoleAsync(List<RootMenuRole> rootMenuRoles)
    {
        rootMenuRoles.ForEach(rootMenuRole =>
        {
            rootMenuRole.SoftDeleteLock = false;
        });
        var result = await _IRootMenuRoleRepository.CreateBatchAsync(rootMenuRoles);
        return result;
    }

    public async Task<bool> CreateRootMenuRoleAsync(RootMenuRole rootMenuRole)
    {
        await _IRootMenuService.IsExistenceAsync(rootMenuRole.MenuId);
        await _IUserRoleService.IsExistenceAsync(rootMenuRole.RoleId);
        if (await _IRootMenuRoleRepository.FindAsync(e => e.MenuId == rootMenuRole.MenuId && e.RoleId == rootMenuRole.RoleId && !e.SoftDeleteLock) != null)
            throw new ApplicationException("角色菜单已存在");
        rootMenuRole.SoftDeleteLock = false;
        var result = await _IRootMenuRoleRepository.CreateAsync(rootMenuRole);
        return result;
    }

    public async Task<bool> DeleteRootMenuRoleAsync(Guid guid, Guid deleteId)
    {
        var rootMenuRole = await IsExistenceAsync(guid);
        rootMenuRole.SoftDeleteLock = true;
        rootMenuRole.DeleteId = deleteId;
        rootMenuRole.DeleteTime = DateTime.Now;
        return await _IRootMenuRoleRepository.UpdateAsync(rootMenuRole);
    }

    public async Task<RootMenuRole> ModifyRootMenuRoleAsync(RootMenuRole rootMenuRole)
    {
        await IsExistenceAsync(rootMenuRole.BaseId);
        await _IRootMenuService.IsExistenceAsync(rootMenuRole.MenuId);
        await _IUserRoleService.IsExistenceAsync(rootMenuRole.RoleId);
        if (await _IRootMenuRoleRepository.FindAsync(e => e.MenuId == rootMenuRole.MenuId && e.RoleId == rootMenuRole.RoleId && !e.SoftDeleteLock) != null)
            throw new ApplicationException("角色菜单已存在");
        rootMenuRole.ModifyTime = DateTime.Now;
        var result = await _IRootMenuRoleRepository.UpdateAsync(rootMenuRole);
        if (result) rootMenuRole = await _IRootMenuRoleRepository.FindAsync(rootMenuRole.BaseId);
        return rootMenuRole;
    }

    public async Task<RootMenuRole> FindRootMenuRoleAsync(Guid guid)
    {
        var rootMenuRole = await IsExistenceAsync(guid);
        return rootMenuRole;
    }

    public async Task<List<RootMenuRole>> QueryRootMenuRoleAsync()
    {
        var rootMenuRole = from rootmenurole in await _IRootMenuRoleRepository.QueryListAsync(e => !e.SoftDeleteLock)
                           orderby rootmenurole.CreateTime descending
                           select rootmenurole;
        return rootMenuRole.ToList();
    }
}