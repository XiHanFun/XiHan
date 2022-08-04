// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogArticleTagService
// Guid:f9b3a059-9beb-4d04-8329-48b390fb1007
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:29
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogArticleTagService
/// </summary>
public class BlogArticleTagService : BaseService<BlogArticleTag>, IBlogArticleTagService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IBlogArticleTagRepository _IBlogArticleTagRepository;
    private readonly IBlogArticleService _IBlogArticleService;
    private readonly IBlogTagService _IBlogTagService;

    public BlogArticleTagService(IRootStateRepository iRootStateRepository,
        IBlogArticleTagRepository iBlogArticleTagRepository,
        IBlogArticleService iBlogArticleService,
        IBlogTagService iIBlogTagService)
    {
        base._IBaseRepository = iBlogArticleTagRepository;
        _IRootStateRepository = iRootStateRepository;
        _IBlogArticleTagRepository = iBlogArticleTagRepository;
        _IBlogArticleService = iBlogArticleService;
        _IBlogTagService = iIBlogTagService;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogArticleTag> IsExistenceAsync(Guid guid)
    {
        var blogArticleTag = await _IBlogArticleTagRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogArticleTag == null)
            throw new ApplicationException("博客文章标签不存在");
        return blogArticleTag;
    }

    public async Task<bool> InitBlogArticleTagAsync(List<BlogArticleTag> blogArticleTags)
    {
        blogArticleTags.ForEach(blogArticleTag =>
        {
            blogArticleTag.SoftDeleteLock = false;
        });
        var result = await _IBlogArticleTagRepository.CreateBatchAsync(blogArticleTags);
        return result;
    }

    public async Task<bool> CreateBlogArticleTagAsync(BlogArticleTag blogArticleTag)
    {
        await _IBlogArticleService.IsExistenceAsync(blogArticleTag.ArticleId);
        await _IBlogTagService.IsExistenceAsync(blogArticleTag.TagId);
        blogArticleTag.SoftDeleteLock = false;
        var result = await _IBlogArticleTagRepository.CreateAsync(blogArticleTag);
        return result;
    }

    public async Task<bool> DeleteBlogArticleTagAsync(Guid guid, Guid deleteId)
    {
        var blogArticleTag = await IsExistenceAsync(guid);
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        blogArticleTag.SoftDeleteLock = true;
        blogArticleTag.DeleteId = deleteId;
        blogArticleTag.DeleteTime = DateTime.Now;
        blogArticleTag.StateId = rootState.BaseId;
        return await _IBlogArticleTagRepository.UpdateAsync(blogArticleTag);
    }

    public async Task<BlogArticleTag> ModifyBlogArticleTagAsync(BlogArticleTag blogArticleTag)
    {
        await IsExistenceAsync(blogArticleTag.BaseId);
        await _IBlogArticleService.IsExistenceAsync(blogArticleTag.ArticleId);
        await _IBlogTagService.IsExistenceAsync(blogArticleTag.TagId);
        var result = await _IBlogArticleTagRepository.UpdateAsync(blogArticleTag);
        if (result) blogArticleTag = await _IBlogArticleTagRepository.FindAsync(blogArticleTag.BaseId);
        return blogArticleTag;
    }

    public async Task<BlogArticleTag> FindBlogArticleTagAsync(Guid guid)
    {
        var blogArticleTag = await IsExistenceAsync(guid);
        return blogArticleTag;
    }

    public async Task<List<BlogArticleTag>> QueryBlogArticleTagAsync()
    {
        var blogArticleTag = from blogarticletag in await _IBlogArticleTagRepository.QueryListAsync(e => !e.SoftDeleteLock)
                             orderby blogarticletag.CreateTime descending
                             select blogarticletag;
        return blogArticleTag.ToList();
    }
}