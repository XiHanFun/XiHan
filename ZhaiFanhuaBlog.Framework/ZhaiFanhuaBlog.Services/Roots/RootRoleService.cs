// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootRoleService
// Guid:16ffe501-5310-4ea5-b534-97f826f7c04c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:02:45
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootRoleService
/// </summary>
public class RootRoleService : BaseService<RootRole>, IRootRoleService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IRootRoleRepository _IRootRoleRepository;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    public RootRoleService(IRootStateRepository iRootStateRepository,
        IRootRoleRepository iRootRoleRepository,
        IUserAccountRoleRepository iUserAccountRoleRepository)
    {
        _IBaseRepository = iRootRoleRepository;
        _IRootStateRepository = iRootStateRepository;
        _IRootRoleRepository = iRootRoleRepository;
        _IUserAccountRoleRepository = iUserAccountRoleRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootRole> IsExistenceAsync(Guid guid)
    {
        var userRole = await _IRootRoleRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (userRole == null)
            throw new ApplicationException("系统角色不存在");
        return userRole;
    }

    public async Task<bool> InitRootRoleAsync(List<RootRole> userRoles)
    {
        userRoles.ForEach(userRole =>
        {
            userRole.SoftDeleteLock = false;
        });
        var result = await _IRootRoleRepository.CreateBatchAsync(userRoles);
        return result;
    }

    public async Task<bool> CreateRootRoleAsync(RootRole userRole)
    {
        if (userRole.ParentId != null && await _IRootRoleRepository.FindAsync(e => e.ParentId == userRole.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级系统角色不存在");
        if (await _IRootRoleRepository.FindAsync(e => e.Name == userRole.Name) != null)
            throw new ApplicationException("系统角色名称已存在");
        userRole.SoftDeleteLock = false;
        var result = await _IRootRoleRepository.CreateAsync(userRole);
        return result;
    }

    public async Task<bool> DeleteRootRoleAsync(Guid guid, Guid deleteId)
    {
        var userRole = await IsExistenceAsync(guid);
        if ((await _IRootRoleRepository.QueryListAsync(e => e.ParentId == userRole.ParentId && !e.SoftDeleteLock)).Count != 0)
            throw new ApplicationException("该系统角色下有子系统角色，不能删除");
        if ((await _IUserAccountRoleRepository.QueryListAsync(e => e.RoleId == userRole.BaseId)).Count != 0)
            throw new ApplicationException("该系统角色已有用户账户使用，不能删除");
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        userRole.SoftDeleteLock = true;
        userRole.DeleteId = deleteId;
        userRole.DeleteTime = DateTime.Now;
        userRole.StateId = rootState.BaseId;
        return await _IRootRoleRepository.UpdateAsync(userRole);
    }

    public async Task<RootRole> ModifyRootRoleAsync(RootRole userRole)
    {
        await IsExistenceAsync(userRole.BaseId);
        if (userRole.ParentId != null && await _IRootRoleRepository.FindAsync(e => e.ParentId == userRole.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级系统角色不存在");
        if (await _IRootRoleRepository.FindAsync(e => e.Name == userRole.Name) != null)
            throw new ApplicationException("系统角色名称已存在");
        var result = await _IRootRoleRepository.UpdateAsync(userRole);
        if (result) userRole = await _IRootRoleRepository.FindAsync(userRole.BaseId);
        return userRole;
    }

    public async Task<RootRole> FindRootRoleAsync(Guid guid)
    {
        var userRole = await IsExistenceAsync(guid);
        return userRole;
    }

    public async Task<List<RootRole>> QueryRootRoleAsync()
    {
        var userRole = from userrole in await _IRootRoleRepository.QueryListAsync(e => !e.SoftDeleteLock)
                       orderby userrole.CreateTime descending
                       orderby userrole.Name descending
                       select userrole;
        return userRole.ToList();
    }
}