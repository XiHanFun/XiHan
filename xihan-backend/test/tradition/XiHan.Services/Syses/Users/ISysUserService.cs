#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ISysUserService
// Guid:d3876fef-9f85-4947-bb58-abc7e38b858d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-04-22 上午 12:45:47
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Infrastructures.Responses.Pages;
using XiHan.Models.Syses;
using XiHan.Services.Bases;
using XiHan.Services.Syses.Users.Dtos;

namespace XiHan.Services.Syses.Users;

/// <summary>
/// ISysUserService
/// </summary>
public interface ISysUserService : IBaseService<SysUser>
{
    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    Task<long> CreateUser(SysUserCDto userCDto);

    /// <summary>
    /// 删除用户(假删除)
    /// </summary>
    /// <param name="sysUser"></param>
    /// <returns></returns>
    Task<bool> DeleteUser(SysUser sysUser);

    /// <summary>
    /// 修改用户账号信息
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    Task<bool> ModifyUserAccountInfo(SysUserCDto userCDto);

    /// <summary>
    /// 修改用户基本信息
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    Task<bool> ModifyUserBaseInfo(SysUserCDto userCDto);

    ///// <summary>
    ///// 修改用户角色
    ///// </summary>
    ///// <param name="userCDto"></param>
    ///// <returns></returns>
    //public async Task<bool> ModifyUserRole(SysUserCDto userCDto);

    /// <summary>
    /// 更改用户状态
    /// </summary>
    /// <param name="userCDto"></param>
    /// <returns></returns>
    Task<bool> ModifyUserStatus(SysUserCDto userCDto);

    /// <summary>
    /// 重置用户密码
    /// </summary>
    /// <param name="userPwdMDto"></param>
    /// <returns></returns>
    Task<bool> ModifyUserPassword(SysUserPwdMDto userPwdMDto);

    /// <summary>
    /// 查询用户信息(根据用户主键)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<SysUser> GetUserById(long userId);

    /// <summary>
    /// 查询用户信息(登录获取Token使用)
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<SysUser> GetUserByAccount(string account);

    /// <summary>
    /// 查询用户信息(登录获取Token使用)
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<SysUser> GetUserByEmail(string email);

    /// <summary>
    /// 查询用户列表(根据分页条件)
    /// </summary>
    /// <param name="pageWhere"></param>
    /// <returns></returns>
    Task<PageDataDto<SysUser>> GetUserList(PageWhereDto<SysUserWDto> pageWhere);
}