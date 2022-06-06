// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogPollRepository
// Guid:eb08749e-05c6-4541-8d76-36ad1657b192
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:45:33
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Blogs;

/// <summary>
/// BlogPollRepository
/// </summary>
public class BlogPollRepository : BaseRepository<BlogPoll>, IBlogPollRepository
{
}