// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserAuthorityRepository
// Guid:498edbce-445c-4609-83b3-613527673216
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:27:27
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IRepositories.Users;

/// <summary>
/// IUserAuthorityRepository
/// </summary>
public interface IUserAuthorityRepository : IBaseRepository<UserAuthority>
{
}