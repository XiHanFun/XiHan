#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:RootRoleAuthorityService
// Guid:fa73716d-d139-4da8-9eda-e6aca30bd5d0
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:03:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Repositories.Roots;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Roots;

/// <summary>
/// RootRoleAuthorityService
/// </summary>
public class RootRoleAuthorityService : BaseService<RootRoleAuthority>, IRootRoleAuthorityService
{
    private readonly IRootAuthorityService _IRootAuthorityService;
    private readonly IRootRoleService _IRootRoleService;
    private readonly IRootRoleAuthorityRepository _IRootRoleAuthorityRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iRootAuthorityService"></param>
    /// <param name="iRootRoleService"></param>
    /// <param name="iRootRoleAuthorityRepository"></param>
    public RootRoleAuthorityService(IRootAuthorityService iRootAuthorityService,
        IRootRoleService iRootRoleService,
        IRootRoleAuthorityRepository iRootRoleAuthorityRepository)
    {
        _IBaseRepository = iRootRoleAuthorityRepository;
        _IRootAuthorityService = iRootAuthorityService;
        _IRootRoleService = iRootRoleService;
        _IRootRoleAuthorityRepository = iRootRoleAuthorityRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootRoleAuthority> IsExistenceAsync(Guid guid)
    {
        var rootRoleAuthority = await _IRootRoleAuthorityRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (rootRoleAuthority == null)
            throw new ApplicationException("系统角色权限不存在");
        return rootRoleAuthority;
    }

    /// <summary>
    /// 初始化系统角色权限关联
    /// </summary>
    /// <param name="rootRoleAuthorities"></param>
    /// <returns></returns>
    public async Task<bool> InitRootRoleAuthorityAsync(List<RootRoleAuthority> rootRoleAuthorities)
    {
        rootRoleAuthorities.ForEach(userRoleAuthority =>
        {
            userRoleAuthority.SoftDeleteLock = false;
        });
        var result = await _IRootRoleAuthorityRepository.CreateBatchAsync(rootRoleAuthorities);
        return result;
    }

    /// <summary>
    /// 新增系统角色权限关联
    /// </summary>
    /// <param name="rootRoleAuthority"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateRootRoleAuthorityAsync(RootRoleAuthority rootRoleAuthority)
    {
        await _IRootAuthorityService.IsExistenceAsync(rootRoleAuthority.AuthorityId);
        await _IRootRoleService.IsExistenceAsync(rootRoleAuthority.RoleId);
        if (await _IRootRoleAuthorityRepository.FindAsync(e => e.AuthorityId == rootRoleAuthority.AuthorityId && e.RoleId == rootRoleAuthority.RoleId) != null)
            throw new ApplicationException("系统角色权限已存在");
        rootRoleAuthority.SoftDeleteLock = false;
        var result = await _IRootRoleAuthorityRepository.CreateAsync(rootRoleAuthority);
        return result;
    }

    /// <summary>
    /// 删除系统角色权限关联
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteRootRoleAuthorityAsync(Guid guid, Guid deleteId)
    {
        var rootRoleAuthority = await IsExistenceAsync(guid);
        rootRoleAuthority.SoftDeleteLock = true;
        rootRoleAuthority.DeleteId = deleteId;
        rootRoleAuthority.DeleteTime = DateTime.Now;
        return await _IRootRoleAuthorityRepository.UpdateAsync(rootRoleAuthority);
    }

    /// <summary>
    /// 修改系统角色权限关联
    /// </summary>
    /// <param name="rootRoleAuthority"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<RootRoleAuthority> ModifyRootRoleAuthorityAsync(RootRoleAuthority rootRoleAuthority)
    {
        await IsExistenceAsync(rootRoleAuthority.BaseId);
        await _IRootAuthorityService.IsExistenceAsync(rootRoleAuthority.AuthorityId);
        await _IRootRoleService.IsExistenceAsync(rootRoleAuthority.RoleId);
        if (await _IRootRoleAuthorityRepository.FindAsync(e => e.AuthorityId == rootRoleAuthority.AuthorityId && e.RoleId == rootRoleAuthority.RoleId && e.SoftDeleteLock == false) != null)
            throw new ApplicationException("系统角色权限已存在");
        rootRoleAuthority.ModifyTime = DateTime.Now;
        var result = await _IRootRoleAuthorityRepository.UpdateAsync(rootRoleAuthority);
        if (result) rootRoleAuthority = await _IRootRoleAuthorityRepository.FindAsync(rootRoleAuthority.BaseId);
        return rootRoleAuthority;
    }

    /// <summary>
    /// 查找系统角色权限关联
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<RootRoleAuthority> FindRootRoleAuthorityAsync(Guid guid)
    {
        var rootRoleAuthority = await IsExistenceAsync(guid);
        return rootRoleAuthority;
    }

    /// <summary>
    /// 查询系统角色权限关联
    /// </summary>
    /// <returns></returns>
    public async Task<List<RootRoleAuthority>> QueryRootRoleAuthorityAsync()
    {
        var rootRoleAuthority = from userroleauthority in await _IRootRoleAuthorityRepository.QueryListAsync(e => e.SoftDeleteLock == false)
                                orderby userroleauthority.CreateTime descending
                                select userroleauthority;
        return rootRoleAuthority.ToList();
    }
}