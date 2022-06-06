// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserOauthRepository
// Guid:b91eb870-7139-4f96-9edd-88af93254058
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:29:32
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Bases;
using ZhaiFanhuaBlog.Models.Users;

namespace ZhaiFanhuaBlog.IRepositories.Users;

/// <summary>
/// IUserOauthRepository
/// </summary>
public interface IUserOauthRepository : IBaseRepository<UserOauth>
{
}