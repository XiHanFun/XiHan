// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCategoryRepository
// Guid:c0b3f2a5-74fb-4d09-9b66-d9b867f941fd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:44:29
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Blogs;

/// <summary>
/// BlogCategoryRepository
/// </summary>
public class BlogCategoryRepository : BaseRepository<BlogCategory>, IBlogCategoryRepository
{
}