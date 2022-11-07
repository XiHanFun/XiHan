// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserAccountRoleRepository
// Guid:6e0d19d9-ad24-42c6-bb4c-9acb64ddcff3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-09 下午 05:18:17
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// IUserAccountRoleRepository
/// </summary>
public interface IUserAccountRoleRepository : IBaseRepository<UserAccountRole>, IScopeDependency
{
}