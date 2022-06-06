// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogPollService
// Guid:182aa8bc-458b-4c25-8501-67417fc31ec3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogPollService
/// </summary>
public class BlogPollService : BaseService<BlogPoll>, IBlogPollService
{
    private readonly IBlogPollRepository _IBlogPollRepository;

    public BlogPollService(IBlogPollRepository iBlogPollRepository)
    {
        _IBlogPollRepository = iBlogPollRepository;
        _iBaseRepository = iBlogPollRepository;
    }
}