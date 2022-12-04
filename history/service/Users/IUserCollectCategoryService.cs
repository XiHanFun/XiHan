#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IUserCollectCategoryService
// Guid:cdc22524-3914-82f3-a3fa-b03cff003c33
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-01-06 下午 10:32:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Users;

/// <summary>
/// IUserCollectCategoryService
/// </summary>
public interface IUserCollectCategoryService : IBaseService<UserCollectCategory>, IScopeDependency
{
}