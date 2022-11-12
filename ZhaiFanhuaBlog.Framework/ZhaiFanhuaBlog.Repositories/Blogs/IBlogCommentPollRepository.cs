#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogCommentPollRepository
// Guid:554d6f2e-07ea-419d-974b-b9cd6f5c5364
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:11:12
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Core.Services;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Blogs;

/// <summary>
/// IBlogCommentPollRepository
/// </summary>
public interface IBlogCommentPollRepository : IBaseRepository<BlogCommentPoll>, IScopeDependency
{
}