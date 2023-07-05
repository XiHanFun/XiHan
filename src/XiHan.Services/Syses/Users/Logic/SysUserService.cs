﻿#region <<版权版本注释>>

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
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Exceptions;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Roles;
using XiHan.Services.Syses.Users.Dtos;
using XiHan.Utils.Encryptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Users.Logic;

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
    /// 校验用户名称是否唯一
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    private async Task<bool> GetUserNameUnique(string userName)
    {
        var isUnique = await IsAnyAsync(u => u.UserName == userName && !u.IsDeleted);
        if (isUnique) throw new CustomException($"用户名【{userName}】已存在！");
        return isUnique;
    }

    /// <summary>
    /// 校验角色是否允许操作
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    private bool GetUserIsAdmin(SysUser sysUser)
    {
        var isAdmin = sysUser.IsAdmin();
        if (!isAdmin) throw new CustomException("不允许操作超级管理员角色！");
        return isAdmin;
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<long> CreateUser(SysUser sysUser)
    {
        var secretKey = AppSettings.Syses.Domain.GetValue();
        sysUser.Password = Md5EncryptionHelper.Encrypt(AesEncryptionHelper.Encrypt(sysUser.Password, secretKey));
        _ = await GetUserNameUnique(sysUser.UserName);
        var userId = await AddReturnIdAsync(sysUser);

        // 新增用户角色信息
        sysUser.BaseId = userId;
        await _sysUserRoleService.CreateUserRole(sysUser);
        return userId;
    }

    /// <summary>
    /// 删除用户(假删除)
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUser(SysUser sysUser)
    {
        var user = await FindAsync(u => u.BaseId == sysUser.BaseId && !u.IsDeleted);
        // 删除用户角色
        await _sysUserRoleService.DeleteUserRoleByUserId(sysUser.BaseId);
        return await SoftRemoveAsync(user);
    }

    /// <summary>
    /// 修改用户登陆信息
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserLoginInfo(SysUser sysUser)
    {
        return await UpdateAsync(s => new SysUser()
        {
            LastLoginIp = sysUser.LastLoginIp,
            LastLoginTime = DateTime.Now
        }, f => f.BaseId == sysUser.BaseId);
    }

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserInfo(SysUser sysUser)
    {
        return await UpdateAsync(s => new SysUser()
        {
            NickName = sysUser.NickName,
            Signature = sysUser.Signature,
            Gender = sysUser.Gender,
            Address = sysUser.Address,
            Birthday = sysUser.Birthday
        }, f => f.BaseId == sysUser.BaseId);
    }

    /// <summary>
    /// 修改用户角色
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserRole(SysUser sysUser)
    {
        var roleIds = await _sysRoleService.GetUserRoleIdsByUserId(sysUser.BaseId);
        var diffArr1 = roleIds.Where(id => !sysUser.SysRoleIds.Contains(id)).ToList();
        var diffArr2 = sysUser.SysRoleIds.Where(id => !roleIds.Contains(id)).ToList();

        if (!diffArr1.Any() && !diffArr2.Any()) return true;
        // 删除用户与角色关联并重新关联用户与角色
        return await _sysUserRoleService.DeleteUserRoleByUserId(sysUser.BaseId) && await _sysUserRoleService.CreateUserRole(sysUser);
    }

    /// <summary>
    /// 更改用户状态
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    public async Task<bool> ChangeUserStatus(SysUser sysUser)
    {
        return await UpdateAsync(s => new SysUser()
        {
            StateKey = sysUser.StateKey,
            StateValue = sysUser.StateValue
        }, f => f.BaseId == sysUser.BaseId);
    }

    /// <summary>
    /// 重置用户密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<bool> ResetUserPassword(long userId, string password)
    {
        var secretKey = AppSettings.Syses.Domain.GetValue();
        return await UpdateAsync(s => new SysUser()
        {
            Password = Md5EncryptionHelper.Encrypt(AesEncryptionHelper.Encrypt(password, secretKey))
        }, f => f.BaseId == userId);
    }

    /// <summary>
    /// 查询用户信息
    /// </summary>
    /// <param name="dictId"></param>
    /// <returns></returns>
    public async Task<SysUser> GetDictTypeById(long dictId)
    {
        return await FindAsync(f => f.BaseId == dictId);
    }

    /// <summary>
    /// 查询用户列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysUser>> GetUserList(PageWhereDto<SysUserWhereDto> pageWhere)
    {
        var whereDto = pageWhere.Where;
        var whereExpression = Expressionable.Create<SysUser>();
        whereExpression.AndIF(whereDto.UserName.IsNotEmptyOrNull(), u => u.UserName.Contains(whereDto.UserName!));
        whereExpression.AndIF(whereDto.Email.IsNotEmptyOrNull(), u => u.Email == whereDto.Email);
        whereExpression.AndIF(whereDto.Phone.IsNotEmptyOrNull(), u => u.Phone == whereDto.Phone);
        whereExpression.AndIF(whereDto.NickName.IsNotEmptyOrNull(), u => u.UserName.Contains(whereDto.NickName!));
        whereExpression.AndIF(whereDto.Gender.IsNotEmptyOrNull(), u => u.Gender == whereDto.Gender);
        whereExpression.And(u => !u.IsDeleted);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page);
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
        sysUser.SysRoles = await _sysRoleService.GetUserRolesByUserId(userId);
        sysUser.SysRoleIds = sysUser.SysRoles.Select(x => x.BaseId).ToList();

        return sysUser;
    }
}