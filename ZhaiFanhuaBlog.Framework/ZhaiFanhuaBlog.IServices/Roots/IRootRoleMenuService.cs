﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootRoleMenuService
// Guid:8aee44e2-4110-4af6-a221-7e12392fe261
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:20:54
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Roots;

namespace ZhaiFanhuaBlog.IServices.Roots;

/// <summary>
/// IRootRoleMenuService
/// </summary>
public interface IRootRoleMenuService : IBaseService<RootRoleMenu>
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootRoleMenu> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="RootRoleMenus"></param>
    /// <returns></returns>
    Task<bool> InitRootRoleMenuAsync(List<RootRoleMenu> RootRoleMenus);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="RootRoleMenu"></param>
    /// <returns></returns>
    Task<bool> CreateRootRoleMenuAsync(RootRoleMenu RootRoleMenu);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteRootRoleMenuAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="RootRoleMenu"></param>
    /// <returns></returns>
    Task<RootRoleMenu> ModifyRootRoleMenuAsync(RootRoleMenu RootRoleMenu);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<RootRoleMenu> FindRootRoleMenuAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<RootRoleMenu>> QueryRootRoleMenuAsync();
}