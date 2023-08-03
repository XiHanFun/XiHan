#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:SysUserService
// Guid:2f0d94cc-ae27-4504-94bf-cc835ad307f9
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 02:04:14
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Mapster;
using SqlSugar;
using XiHan.Infrastructures.Apps;
using XiHan.Infrastructures.Apps.Configs;
using XiHan.Infrastructures.Apps.Services;
using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Roles;
using XiHan.Services.Syses.Users.Dtos;
using XiHan.Utils.Encryptions;
using XiHan.Utils.Exceptions;
using XiHan.Utils.Extensions;

namespace XiHan.Services.Syses.Users.Logic;

/// <summary>
/// 系统用户服务
/// </summary>
[AppService(ServiceType = typeof(ISysUserService), ServiceLifetime = ServiceLifeTimeEnum.Transient)]
public class SysUserService : BaseService<SysUser>, ISysUserService
{
    private static string _secretKey = AppSettings.Syses.Domain.GetValue();
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
    /// 校验账户是否唯一
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    private async Task<bool> GetAccountUnique(SysUser sysUser)
    {
        var isUnique = await IsAnyAsync(u => u.Account == sysUser.Account && !u.IsDeleted);
        if (isUnique) throw new CustomException($"账户【{sysUser.Account}】已存在！");
        return isUnique;
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<long> CreateUser(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();

        _ = await GetAccountUnique(sysUser);

        string password = AppGlobalConstant.SysPassword;
        sysUser.Password = Md5EncryptionHelper.Encrypt(AesEncryptionHelper.Encrypt(password, _secretKey));
        var userId = await AddReturnIdAsync(sysUser);

        // 新增用户角色信息
        sysUser.BaseId = userId;
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
    /// 修改用户账号信息
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserAccountInfo(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();
        return await UpdateAsync(s => new SysUser()
        {
            Account = sysUser.Account,
            NickName = sysUser.NickName,
            AvatarPath = sysUser.AvatarPath,
            Signature = sysUser.Signature,
        }, f => f.BaseId == sysUser.BaseId);
    }

    /// <summary>
    /// 修改用户基本信息
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserBaseInfo(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();
        return await UpdateAsync(s => new SysUser()
        {
            RealName = sysUser.RealName,
            Gender = sysUser.Gender,
            Email = sysUser.Email,
            Phone = sysUser.Phone,
            Birthday = sysUser.Birthday,
            Address = sysUser.Address,
        }, f => f.BaseId == sysUser.BaseId);
    }

    ///// <summary>
    ///// 修改用户角色
    ///// </summary>
    ///// <param name="userCDto"></param>
    ///// <returns></returns>
    //public async Task<bool> ModifyUserRole(SysUserCDto userCDto)
    //{
    //    var sysUser = userCDto.Adapt<SysUser>();
    //    var roleIds = await _sysRoleService.GetUserRoleIdsByUserId(sysUser.BaseId);
    //    var diffArr1 = roleIds.Where(id => !sysUser.SysRoleIds.Contains(id)).ToList();
    //    var diffArr2 = sysUser.SysRoleIds.Where(id => !roleIds.Contains(id)).ToList();

    //    //if (!diffArr1.Any() && !diffArr2.Any()) return true;
    //    // 删除用户与角色关联并重新关联用户与角色
    //    return await _sysUserRoleService.DeleteUserRoleByUserId(sysUser.BaseId) && await _sysUserRoleService.CreateUserRole(sysUser);
    //}

    /// <summary>
    /// 更改用户状态
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserStatus(SysUserCDto userCDto)
    {
        var sysUser = userCDto.Adapt<SysUser>();
        return await UpdateAsync(s => new SysUser()
        {
            StateKey = sysUser.StateKey,
            StateValue = sysUser.StateValue
        }, f => f.BaseId == sysUser.BaseId);
    }

    /// <summary>
    /// 重置用户密码
    /// </summary>
    /// <param name="userPwdMDto"></param>
    /// <returns></returns>
    public async Task<bool> ModifyUserPassword(SysUserPwdMDto userPwdMDto)
    {
        if (await IsAnyAsync(u => !u.IsDeleted && u.BaseId == userPwdMDto.BaseId &&
        u.Password == Md5EncryptionHelper.Encrypt(AesEncryptionHelper.Encrypt(userPwdMDto.OldPassword, _secretKey))))
        {
            return await UpdateAsync(s => new SysUser()
            {
                Password = Md5EncryptionHelper.Encrypt(AesEncryptionHelper.Encrypt(userPwdMDto.NewPassword, _secretKey))
            }, f => f.BaseId == userPwdMDto.BaseId);
        }
        throw new CustomException("重置密码出错，旧密码有误！");
    }

    /// <summary>
    /// 查询用户信息(根据用户主键)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserById(long userId)
    {
        var sysUser = await FindAsync(u => u.BaseId == userId && !u.IsDeleted);
        //sysUser.SysRoles = await _sysRoleService.GetUserRolesByUserId(userId);
        //sysUser.SysRoleIds = sysUser.SysRoles.Select(x => x.BaseId).ToList();

        return sysUser;
    }

    /// <summary>
    /// 查询用户信息(登录获取Token使用)
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByAccount(string account)
    {
        return await FindAsync(u => u.Account == account && !u.IsDeleted);
    }

    /// <summary>
    /// 查询用户信息(登录获取Token使用)
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByEmail(string email)
    {
        return await FindAsync(u => u.Email == email && !u.IsDeleted);
    }

    /// <summary>
    /// 查询用户列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    public async Task<PageDataDto<SysUser>> GetUserList(PageWhereDto<SysUserWDto> pageWhere)
    {
        var whereDto = pageWhere.Where;
        var whereExpression = Expressionable.Create<SysUser>();
        whereExpression.AndIF(whereDto.Account.IsNotEmptyOrNull(), u => u.Account.Contains(whereDto.Account!));
        whereExpression.AndIF(whereDto.NickName.IsNotEmptyOrNull(), u => u.NickName.Contains(whereDto.NickName!));
        whereExpression.AndIF(whereDto.RealName.IsNotEmptyOrNull(), u => u.RealName.Contains(whereDto.RealName!));
        whereExpression.AndIF(whereDto.Gender.IsNotEmptyOrNull(), u => u.Gender == whereDto.Gender);
        whereExpression.AndIF(whereDto.Email.IsNotEmptyOrNull(), u => u.Email == whereDto.Email);
        whereExpression.AndIF(whereDto.Phone.IsNotEmptyOrNull(), u => u.Phone == whereDto.Phone);
        whereExpression.And(u => !u.IsDeleted);

        return await QueryPageAsync(whereExpression.ToExpression(), pageWhere.Page);
    }
}