// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserLoginRepository
// Guid:e3a5e32d-b502-4624-afe0-5706cb1bf2a1
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:28:58
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IRepositories.Users;

/// <summary>
/// IUserLoginRepository
/// </summary>
public interface IUserLoginRepository : IBaseRepository<UserLogin>
{
}