#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:SysUserService
// Guid:2f0d94cc-ae27-4504-94bf-cc835ad307f9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 02:04:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using SqlSugar;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Dtos;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Logic;

/// <summary>
/// 系统用户服务
/// </summary>
[AppService(ServiceType = typeof(ISysUserService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysUserService : BaseService<SysUser>, ISysUserService
{
    private readonly ISysUserRoleService _sysUserRoleService;
    private readonly ISysRoleService _sysRoleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public SysUserService(ISysUserRoleService sysUserRoleService, ISysRoleService sysRoleService)
    {
        _sysUserRoleService = sysUserRoleService;
        _sysRoleService = sysRoleService;
    }

    /// <summary>
    /// 查询用户列表(根据分页条件)
    /// </summary>
    /// <param name="whereDto"></param>
    /// <param name="basePageDto"></param>
    /// <returns></returns>
    public async Task<BasePageDataDto<SysUser>> GetUserList(SysUserWhereDto whereDto, BasePageDto basePageDto)
    {
        var exp = Expressionable.Create<SysUser>();
        exp.AndIF(whereDto.UserName.IsNotEmptyOrNull(), u => u.UserName.Contains(whereDto.UserName!));
        exp.AndIF(whereDto.Email.IsNotEmptyOrNull(), u => u.Email == whereDto.Email);
        exp.AndIF(whereDto.Phone.IsNotEmptyOrNull(), u => u.Phone == whereDto.Phone);
        exp.AndIF(whereDto.NickName.IsNotEmptyOrNull(), u => u.UserName.Contains(whereDto.NickName!));
        exp.AndIF(whereDto.Gender.IsNotEmptyOrNull(), u => u.Gender == whereDto.Gender);
        exp.And(u => !u.IsDeleted);

        var result = await QueryPageDataDtoAsync(exp.ToExpression(), basePageDto);

        return result;
    }

    /// <summary>
    /// 查询用户(根据用户主键)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<SysUser?> GetUserById(long userId)
    {
        var exp = Expressionable.Create<SysUser>();
        exp.And(u => !u.IsDeleted);

        var sysUser = await FindAsync(exp.ToExpression());
        if (sysUser != null)
        {
            sysUser.SysRoles = await _sysRoleService.GetUserRolesByUserId(userId);
            sysUser.SysRoleIds = sysUser.SysRoles.Select(x => x.BaseId).ToList();
        }

        return sysUser;
    }

    /// <summary>
    /// 校验用户名称是否唯一
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task<bool> GetUserNameUnique(string userName)
    {
        int count = await CountAsync(u => u.UserName == userName && !u.IsDeleted);
        if (count > 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<long> CreateUser(SysUser sysUser)
    {
        long userId = await AddReturnIdAsync(sysUser);
        sysUser.BaseId = userId;

        //新增用户角色信息
        await _sysUserRoleService.CreateUserRole(sysUser);

        return userId;
    }

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUser(SysUser sysUser)
    {
        var roleIds = await _sysRoleService.GetUserRoleIdsByUserId(sysUser.BaseId);

        var diffArr = roleIds.Where(id => !(sysUser.SysRoleIds).Contains(id)).ToList();
        var diffArr2 = sysUser.SysRoleIds.Where(id => !roleIds.Contains(id)).ToList();

        if (diffArr.Any() || diffArr2.Any())
        {
            // 删除用户与角色关联
            await _sysUserRoleService.DeleteUserRoleByUserId(sysUser.BaseId);
            // 新增用户与角色关联
            await _sysUserRoleService.CreateUserRole(sysUser);
        }
        return await UpdateAsync(sysUser);
    }
}