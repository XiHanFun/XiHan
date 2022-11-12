#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootAuthorityService
// Guid:02502f6a-01bf-49ba-857a-7fc267bd04dc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 10:50:02
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootAuthorityService
/// </summary>
public class RootAuthorityService : BaseService<RootAuthority>, IRootAuthorityService
{
    private readonly IRootAuthorityRepository _IRootAuthorityRepository;
    private readonly IRootRoleAuthorityRepository _IRootRoleAuthorityRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootAuthorityRepository"></param>
    /// <param name="iRootRoleAuthorityRepository"></param>
    public RootAuthorityService(IRootAuthorityRepository iRootAuthorityRepository,
        IRootRoleAuthorityRepository iRootRoleAuthorityRepository)
    {
        _IBaseRepository = iRootAuthorityRepository;
        _IRootAuthorityRepository = iRootAuthorityRepository;
        _IRootRoleAuthorityRepository = iRootRoleAuthorityRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootAuthority> IsExistenceAsync(Guid guid)
    {
        var rootAuthority = await _IRootAuthorityRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (rootAuthority == null)
            throw new ApplicationException("系统权限不存在");
        return rootAuthority;
    }

    /// <summary>
    /// 初始化系统权限
    /// </summary>
    /// <param name="userAuthorities"></param>
    /// <returns></returns>
    public async Task<bool> InitRootAuthorityAsync(List<RootAuthority> userAuthorities)
    {
        userAuthorities.ForEach(rootAuthority =>
        {
            rootAuthority.SoftDeleteLock = false;
        });
        var result = await _IRootAuthorityRepository.CreateBatchAsync(userAuthorities);
        return result;
    }

    /// <summary>
    /// 新增系统权限
    /// </summary>
    /// <param name="rootAuthority"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateRootAuthorityAsync(RootAuthority rootAuthority)
    {
        if (rootAuthority.ParentId != null && await _IRootAuthorityRepository.FindAsync(e => e.ParentId == rootAuthority.ParentId && e.SoftDeleteLock == false) == null)
            throw new ApplicationException("父级系统权限不存在");
        if (await _IRootAuthorityRepository.FindAsync(e => e.Name == rootAuthority.Name) != null)
            throw new ApplicationException("系统权限名称已存在");
        rootAuthority.SoftDeleteLock = false;
        var result = await _IRootAuthorityRepository.CreateAsync(rootAuthority);
        return result;
    }

    /// <summary>
    /// 删除系统权限
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> DeleteRootAuthorityAsync(Guid guid, Guid deleteId)
    {
        var rootAuthority = await IsExistenceAsync(guid);
        if ((await _IRootAuthorityRepository.QueryListAsync(e => e.ParentId == rootAuthority.ParentId && e.SoftDeleteLock == false)).Count != 0)
            throw new ApplicationException("该系统权限下有子系统权限，不能删除");
        if ((await _IRootRoleAuthorityRepository.QueryListAsync(e => e.AuthorityId == rootAuthority.BaseId)).Count != 0)
            throw new ApplicationException("该系统权限已有系统角色使用，不能删除");
        rootAuthority.SoftDeleteLock = true;
        rootAuthority.DeleteId = deleteId;
        rootAuthority.DeleteTime = DateTime.Now;
        return await _IRootAuthorityRepository.UpdateAsync(rootAuthority);
    }

    /// <summary>
    /// 修改系统权限
    /// </summary>
    /// <param name="rootAuthority"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<RootAuthority> ModifyRootAuthorityAsync(RootAuthority rootAuthority)
    {
        await IsExistenceAsync(rootAuthority.BaseId);
        if (rootAuthority.ParentId != null && await _IRootAuthorityRepository.FindAsync(e => e.ParentId == rootAuthority.ParentId && e.SoftDeleteLock == false) == null)
            throw new ApplicationException("父级系统权限不存在");
        if (await _IRootAuthorityRepository.FindAsync(e => e.Name == rootAuthority.Name) != null)
            throw new ApplicationException("系统权限名称已存在");
        rootAuthority.ModifyTime = DateTime.Now;
        var result = await _IRootAuthorityRepository.UpdateAsync(rootAuthority);
        if (result) rootAuthority = await _IRootAuthorityRepository.FindAsync(rootAuthority.BaseId);
        return rootAuthority;
    }

    /// <summary>
    /// 查找系统权限
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootAuthority> FindRootAuthorityAsync(Guid guid)
    {
        var rootAuthority = await IsExistenceAsync(guid);
        return rootAuthority;
    }

    /// <summary>
    /// 查询系统权限
    /// </summary>
    /// <returns></returns>
    public async Task<List<RootAuthority>> QueryRootAuthorityAsync()
    {
        var rootAuthority = from userauthority in await _IRootAuthorityRepository.QueryListAsync(e => e.SoftDeleteLock == false)
                            orderby userauthority.CreateTime descending
                            orderby userauthority.Name descending
                            select userauthority;
        return rootAuthority.ToList();
    }
}