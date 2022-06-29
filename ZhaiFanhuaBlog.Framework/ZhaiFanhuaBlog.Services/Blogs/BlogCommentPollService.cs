// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentPollService
// Guid:33c38140-d18e-4566-9bc0-3778fc44d069
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:53
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCommentPollService
/// </summary>
public class BlogCommentPollService : BaseService<BlogCommentPoll>, IBlogCommentPollService
{
    private readonly IBlogCommentPollRepository _IBlogCommentPollRepository;

    public BlogCommentPollService(IBlogCommentPollRepository iBlogCommentPollRepository)
    {
        _IBlogCommentPollRepository = iBlogCommentPollRepository;
        base._IBaseRepository = iBlogCommentPollRepository;
    }
}