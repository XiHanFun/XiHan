// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IRootAuthorityService
// Guid:afebb9aa-504e-42b0-fb43-8fc584cbb4d1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:30:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Roots;

namespace ZhaiFanhuaBlog.IServices.Roots;

public interface IRootAuthorityService : IBaseService<RootAuthority>
{
    Task<RootAuthority> IsExistenceAsync(Guid guid);

    Task<bool> InitRootAuthorityAsync(List<RootAuthority> userAuthorities);

    Task<bool> CreateRootAuthorityAsync(RootAuthority rootAuthority);

    Task<bool> DeleteRootAuthorityAsync(Guid guid, Guid deleteId);

    Task<RootAuthority> ModifyRootAuthorityAsync(RootAuthority rootAuthority);

    Task<RootAuthority> FindRootAuthorityAsync(Guid guid);

    Task<List<RootAuthority>> QueryRootAuthorityAsync();
}