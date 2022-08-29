// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootRoleMenuService
// Guid:0c28440b-c5c0-4507-bc91-7b0f0c6f272b
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:35:32
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootRoleMenuService
/// </summary>
public class RootRoleMenuService : BaseService<RootRoleMenu>, IRootRoleMenuService
{
    private readonly IRootRoleMenuRepository _IRootRoleMenuRepository;
    private readonly IRootMenuService _IRootMenuService;
    private readonly IRootRoleService _IRootRoleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootRoleMenuRepository"></param>
    /// <param name="iRootMenuService"></param>
    /// <param name="iRootRoleService"></param>
    public RootRoleMenuService(IRootRoleMenuRepository iRootRoleMenuRepository,
        IRootMenuService iRootMenuService,
        IRootRoleService iRootRoleService
        )
    {
        base._IBaseRepository = iRootRoleMenuRepository;
        _IRootRoleMenuRepository = iRootRoleMenuRepository;
        _IRootMenuService = iRootMenuService;
        _IRootRoleService = iRootRoleService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootRoleMenu> IsExistenceAsync(Guid guid)
    {
        var rootMenuRole = await _IRootRoleMenuRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (rootMenuRole == null)
            throw new ApplicationException("角色菜单不存在");
        return rootMenuRole;
    }

    /// <summary>
    /// 初始化系统角色菜单关联
    /// </summary>
    /// <param name="rootMenuRoles"></param>
    /// <returns></returns>
    public async Task<bool> InitRootRoleMenuAsync(List<RootRoleMenu> rootMenuRoles)
    {
        rootMenuRoles.ForEach(rootMenuRole =>
        {
            rootMenuRole.SoftDeleteLock = false;
        });
        var result = await _IRootRoleMenuRepository.CreateBatchAsync(rootMenuRoles);
        return result;
    }

    /// <summary>
    /// 新增系统角色菜单关联
    /// </summary>
    /// <param name="rootMenuRole"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateRootRoleMenuAsync(RootRoleMenu rootMenuRole)
    {
        await _IRootMenuService.IsExistenceAsync(rootMenuRole.MenuId);
        await _IRootRoleService.IsExistenceAsync(rootMenuRole.RoleId);
        if (await _IRootRoleMenuRepository.FindAsync(e => e.MenuId == rootMenuRole.MenuId && e.RoleId == rootMenuRole.RoleId && !e.SoftDeleteLock) != null)
            throw new ApplicationException("角色菜单已存在");
        rootMenuRole.SoftDeleteLock = false;
        var result = await _IRootRoleMenuRepository.CreateAsync(rootMenuRole);
        return result;
    }

    /// <summary>
    /// 删除系统角色菜单关联
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteRootRoleMenuAsync(Guid guid, Guid deleteId)
    {
        var rootMenuRole = await IsExistenceAsync(guid);
        rootMenuRole.SoftDeleteLock = true;
        rootMenuRole.DeleteId = deleteId;
        rootMenuRole.DeleteTime = DateTime.Now;
        return await _IRootRoleMenuRepository.UpdateAsync(rootMenuRole);
    }

    /// <summary>
    /// 修改系统角色菜单关联
    /// </summary>
    /// <param name="rootMenuRole"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<RootRoleMenu> ModifyRootRoleMenuAsync(RootRoleMenu rootMenuRole)
    {
        await IsExistenceAsync(rootMenuRole.BaseId);
        await _IRootMenuService.IsExistenceAsync(rootMenuRole.MenuId);
        await _IRootRoleService.IsExistenceAsync(rootMenuRole.RoleId);
        if (await _IRootRoleMenuRepository.FindAsync(e => e.MenuId == rootMenuRole.MenuId && e.RoleId == rootMenuRole.RoleId && !e.SoftDeleteLock) != null)
            throw new ApplicationException("角色菜单已存在");
        rootMenuRole.ModifyTime = DateTime.Now;
        var result = await _IRootRoleMenuRepository.UpdateAsync(rootMenuRole);
        if (result) rootMenuRole = await _IRootRoleMenuRepository.FindAsync(rootMenuRole.BaseId);
        return rootMenuRole;
    }

    /// <summary>
    /// 查找系统角色菜单关联
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootRoleMenu> FindRootRoleMenuAsync(Guid guid)
    {
        var rootMenuRole = await IsExistenceAsync(guid);
        return rootMenuRole;
    }

    /// <summary>
    /// 查询系统角色菜单关联
    /// </summary>
    /// <returns></returns>
    public async Task<List<RootRoleMenu>> QueryRootRoleMenuAsync()
    {
        var rootMenuRole = from rootmenurole in await _IRootRoleMenuRepository.QueryListAsync(e => !e.SoftDeleteLock)
                           orderby rootmenurole.CreateTime descending
                           select rootmenurole;
        return rootMenuRole.ToList();
    }
}