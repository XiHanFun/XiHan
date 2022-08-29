﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserAccountRoleService
// Guid:1442d253-c415-4808-930e-6eb3f0417fc1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 下午 05:21:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IServices.Users;

/// <summary>
/// IUserAccountRoleService
/// </summary>
public interface IUserAccountRoleService : IBaseService<UserAccountRole>
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<UserAccountRole> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="userAccountRoles"></param>
    /// <returns></returns>
    Task<bool> InitUserAccountRoleAsync(List<UserAccountRole> userAccountRoles);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="userAccountRole"></param>
    /// <returns></returns>
    Task<bool> CreateUserAccountRoleAsync(UserAccountRole userAccountRole);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteUserAccountRoleAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="userAccountRole"></param>
    /// <returns></returns>
    Task<UserAccountRole> ModifyUserAccountRoleAsync(UserAccountRole userAccountRole);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<UserAccountRole> FindUserAccountRoleAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<UserAccountRole>> QueryUserAccountRoleAsync();
}