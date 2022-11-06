// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogPollRepository
// Guid:dea24ffe-b604-4d55-8e41-dec226eca794
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:11:53
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Repositories.Blogs;

/// <summary>
/// IBlogPollRepository
/// </summary>
public interface IBlogPollRepository : IBaseRepository<BlogPoll>, IScopeDependency
{
}