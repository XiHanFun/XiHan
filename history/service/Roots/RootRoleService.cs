#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootRoleService
// Guid:16ffe501-5310-4ea5-b534-97f826f7c04c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:02:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Roots;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootRoleService
/// </summary>
public class RootRoleService : BaseService<RootRole>, IRootRoleService
{
    private readonly IRootRoleRepository _IRootRoleRepository;
    private readonly IUserAccountRoleRepository _IUserAccountRoleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootRoleRepository"></param>
    /// <param name="iUserAccountRoleRepository"></param>
    public RootRoleService(IRootRoleRepository iRootRoleRepository,
        IUserAccountRoleRepository iUserAccountRoleRepository)
    {
        _IBaseRepository = iRootRoleRepository;
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
        var userRole = await _IRootRoleRepository.FindAsync(e => e.BaseId == guid && !e.SoftDelete);
        if (userRole == null)
            throw new ApplicationException("系统角色不存在");
        return userRole;
    }

    /// <summary>
    /// 初始化系统角色
    /// </summary>
    /// <param name="userRoles"></param>
    /// <returns></returns>
    public async Task<bool> InitRootRoleAsync(List<RootRole> userRoles)
    {
        userRoles.ForEach(userRole =>
        {
            userRole.SoftDelete = false;
        });
        var result = await _IRootRoleRepository.CreateBatchAsync(userRoles);
        return result;
    }

    /// <summary>
    /// 新增系统角色
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateRootRoleAsync(RootRole userRole)
    {
        if (userRole.ParentId != null && await _IRootRoleRepository.FindAsync(e => e.ParentId == userRole.ParentId && !e.SoftDelete) == null)
            throw new ApplicationException("父级系统角色不存在");
        if (await _IRootRoleRepository.FindAsync(e => e.RoleName == userRole.RoleName) != null)
            throw new ApplicationException("系统角色名称已存在");
        userRole.SoftDelete = false;
        var result = await _IRootRoleRepository.CreateAsync(userRole);
        return result;
    }

    /// <summary>
    /// 删除系统角色
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> DeleteRootRoleAsync(Guid guid, Guid deleteId)
    {
        var userRole = await IsExistenceAsync(guid);
        if ((await _IRootRoleRepository.QueryListAsync(e => e.ParentId == userRole.ParentId && !e.SoftDelete)).Count != 0)
            throw new ApplicationException("该系统角色下有子系统角色，不能删除");
        if ((await _IUserAccountRoleRepository.QueryListAsync(e => e.RoleId == userRole.BaseId)).Count != 0)
            throw new ApplicationException("该系统角色已有用户账户使用，不能删除");
        userRole.SoftDelete = true;
        userRole.DeleteId = deleteId;
        userRole.DeleteTime = DateTime.Now;
        return await _IRootRoleRepository.UpdateAsync(userRole);
    }

    /// <summary>
    /// 修改系统角色
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<RootRole> ModifyRootRoleAsync(RootRole userRole)
    {
        await IsExistenceAsync(userRole.BaseId);
        if (userRole.ParentId != null && await _IRootRoleRepository.FindAsync(e => e.ParentId == userRole.ParentId && !e.SoftDelete) == null)
            throw new ApplicationException("父级系统角色不存在");
        if (await _IRootRoleRepository.FindAsync(e => e.RoleName == userRole.RoleName) != null)
            throw new ApplicationException("系统角色名称已存在");
        var result = await _IRootRoleRepository.UpdateAsync(userRole);
        if (result) userRole = await _IRootRoleRepository.FindAsync(userRole.BaseId);
        return userRole;
    }

    /// <summary>
    /// 查找系统角色
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootRole> FindRootRoleAsync(Guid guid)
    {
        var userRole = await IsExistenceAsync(guid);
        return userRole;
    }

    /// <summary>
    /// 查询系统角色
    /// </summary>
    /// <returns></returns>
    public async Task<List<RootRole>> QueryRootRoleAsync()
    {
        var userRole = from userrole in await _IRootRoleRepository.QueryListAsync(e => !e.SoftDelete)
                       orderby userrole.CreateTime descending
                       orderby userrole.RoleName descending
                       select userrole;
        return userRole.ToList();
    }
}