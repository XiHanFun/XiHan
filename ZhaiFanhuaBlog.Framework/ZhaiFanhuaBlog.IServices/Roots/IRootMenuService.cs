// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootMenuService
// Guid:7df2f5f6-5b13-441a-903c-727e3c6fdc6f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2022-08-05 下午 05:20:31
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Roots;

namespace ZhaiFanhuaBlog.IServices.Roots;

/// <summary>
/// IRootMenuService
/// </summary>
public interface IRootMenuService : IBaseService<RootMenu>
{
    Task<RootMenu> IsExistenceAsync(Guid guid);

    Task<bool> InitRootMenuAsync(List<RootMenu> RootMenus);

    Task<bool> CreateRootMenuAsync(RootMenu RootMenu);

    Task<bool> DeleteRootMenuAsync(Guid guid, Guid deleteId);

    Task<RootMenu> ModifyRootMenuAsync(RootMenu RootMenu);

    Task<RootMenu> FindRootMenuAsync(Guid guid);

    Task<List<RootMenu>> QueryRootMenuAsync();
}