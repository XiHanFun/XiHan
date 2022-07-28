// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogPollService
// Guid:182aa8bc-458b-4c25-8501-67417fc31ec3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:36
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogPollService
/// </summary>
public class BlogPollService : BaseService<BlogPoll>, IBlogPollService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IBlogPollRepository _IBlogPollRepository;
    private readonly IBlogArticleService _IBlogArticleService;

    public BlogPollService(IRootStateRepository iRootStateRepository,
        IBlogPollRepository iBlogPollRepository,
        IBlogArticleService iBlogArticleService)
    {
        base._IBaseRepository = iBlogPollRepository;
        _IRootStateRepository = iRootStateRepository;
        _IBlogPollRepository = iBlogPollRepository;
        _IBlogArticleService = iBlogArticleService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogPoll> IsExistenceAsync(Guid guid)
    {
        var blogCommentPoll = await _IBlogPollRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogCommentPoll == null)
            throw new ApplicationException("博客文章点赞不存在");
        return blogCommentPoll;
    }

    public async Task<bool> InitBlogPollAsync(List<BlogPoll> blogCommentPolls)
    {
        blogCommentPolls.ForEach(blogCommentPoll =>
        {
            blogCommentPoll.SoftDeleteLock = false;
        });
        var result = await _IBlogPollRepository.CreateBatchAsync(blogCommentPolls);
        return result;
    }

    public async Task<bool> CreateBlogPollAsync(BlogPoll blogCommentPoll)
    {
        await _IBlogArticleService.IsExistenceAsync(blogCommentPoll.ArticleId);
        blogCommentPoll.SoftDeleteLock = false;
        var result = await _IBlogPollRepository.CreateAsync(blogCommentPoll);
        return result;
    }

    public async Task<bool> DeleteBlogPollAsync(Guid guid, Guid deleteId)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        blogCommentPoll.SoftDeleteLock = true;
        blogCommentPoll.DeleteId = deleteId;
        blogCommentPoll.DeleteTime = DateTime.Now;
        blogCommentPoll.StateId = rootState.BaseId;
        return await _IBlogPollRepository.UpdateAsync(blogCommentPoll);
    }

    public async Task<BlogPoll> ModifyBlogPollAsync(BlogPoll blogCommentPoll)
    {
        await IsExistenceAsync(blogCommentPoll.BaseId);
        await _IBlogArticleService.IsExistenceAsync(blogCommentPoll.ArticleId);
        var result = await _IBlogPollRepository.UpdateAsync(blogCommentPoll);
        if (result) blogCommentPoll = await _IBlogPollRepository.FindAsync(blogCommentPoll.BaseId);
        return blogCommentPoll;
    }

    public async Task<BlogPoll> FindBlogPollAsync(Guid guid)
    {
        var blogCommentPoll = await IsExistenceAsync(guid);
        return blogCommentPoll;
    }

    public async Task<List<BlogPoll>> QueryBlogPollAsync()
    {
        var blogCommentPoll = from blogtag in await _IBlogPollRepository.QueryAsync(e => !e.SoftDeleteLock)
                              orderby blogtag.CreateTime descending
                              select blogtag;
        return blogCommentPoll.ToList();
    }
}