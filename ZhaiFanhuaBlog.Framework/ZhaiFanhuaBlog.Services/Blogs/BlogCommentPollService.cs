// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCommentPollService
// Guid:33c38140-d18e-4566-9bc0-3778fc44d069
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:53
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCommentPollService
/// </summary>
public class BlogCommentPollService : BaseService<BlogCommentPoll>, IBlogCommentPollService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IBlogCommentPollRepository _IBlogCommentPollRepository;
    private readonly IBlogCommentService _IBlogCommentService;

    public BlogCommentPollService(IRootStateRepository iRootStateRepository,
        IBlogCommentPollRepository iBlogCommentPollRepository,
        IBlogCommentService iBlogCommentService)
    {
        base._IBaseRepository = iBlogCommentPollRepository;
        _IRootStateRepository = iRootStateRepository;
        _IBlogCommentPollRepository = iBlogCommentPollRepository;
        _IBlogCommentService = iBlogCommentService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogCommentPoll> IsExistenceAsync(Guid guid)
    {
        var blogCommentPoll = await _IBlogCommentPollRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogCommentPoll == null)
            throw new ApplicationException("博客文章评论点赞不存在");
        return blogCommentPoll;
    }

    public async Task<bool> InitBlogCommentPollAsync(List<BlogCommentPoll> blogCommentPolls)
    {
        blogCommentPolls.ForEach(blogCommentPoll =>
        {
            blogCommentPoll.SoftDeleteLock = false;
        });
        var result = await _IBlogCommentPollRepository.CreateBatchAsync(blogCommentPolls);
        return result;
    }

    public async Task<bool> CreateBlogCommentPollAsync(BlogCommentPoll blogCommentPoll)
    {
        await _IBlogCommentService.IsExistenceAsync(blogCommentPoll.CommentId);
        blogCommentPoll.SoftDeleteLock = false;
        var result = await _IBlogCommentPollRepository.CreateAsync(blogCommentPoll);
        return result;
    }

    public async Task<bool> DeleteBlogCommentPollAsync(Guid guid, Guid deleteId)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        blogCommentPoll.SoftDeleteLock = true;
        blogCommentPoll.DeleteId = deleteId;
        blogCommentPoll.DeleteTime = DateTime.Now;
        blogCommentPoll.StateId = rootState.BaseId;
        return await _IBlogCommentPollRepository.UpdateAsync(blogCommentPoll);
    }

    public async Task<BlogCommentPoll> ModifyBlogCommentPollAsync(BlogCommentPoll blogCommentPoll)
    {
        await IsExistenceAsync(blogCommentPoll.BaseId);
        await _IBlogCommentService.IsExistenceAsync(blogCommentPoll.CommentId);
        var result = await _IBlogCommentPollRepository.UpdateAsync(blogCommentPoll);
        if (result) blogCommentPoll = await _IBlogCommentPollRepository.FindAsync(blogCommentPoll.BaseId);
        return blogCommentPoll;
    }

    public async Task<BlogCommentPoll> FindBlogCommentPollAsync(Guid guid)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        return blogCommentPoll;
    }

    public async Task<List<BlogCommentPoll>> QueryBlogCommentPollAsync()
    {
        var blogCommentPoll = from blogtag in await _IBlogCommentPollRepository.QueryAsync(e => !e.SoftDeleteLock)
                              orderby blogtag.CreateTime descending
                              select blogtag;
        return blogCommentPoll.ToList();
    }
}