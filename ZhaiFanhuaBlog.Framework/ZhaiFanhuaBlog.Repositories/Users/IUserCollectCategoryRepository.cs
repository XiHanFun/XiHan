// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserCollectCategoryRepository
// Guid:95d3ea7c-7c29-41a5-97ad-ad02b2c6bb1a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:27:55
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Users;

/// <summary>
/// IUserCollectCategoryRepository
/// </summary>
public interface IUserCollectCategoryRepository : IBaseRepository<UserCollectCategory>, IScopeDependency
{
}