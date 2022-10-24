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
    private readonly IBlogArticleTagRepository _IBlogArticleTagRepository;
    private readonly IBlogArticleService _IBlogArticleService;
    private readonly IBlogTagService _IBlogTagService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogArticleTagRepository"></param>
    /// <param name="iBlogArticleService"></param>
    /// <param name="iIBlogTagService"></param>
    public BlogArticleTagService(IBlogArticleTagRepository iBlogArticleTagRepository,
        IBlogArticleService iBlogArticleService,
        IBlogTagService iIBlogTagService)
    {
        base._IBaseRepository = iBlogArticleTagRepository;
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

    /// <summary>
    /// 初始化博客文章标签
    /// </summary>
    /// <param name="blogArticleTags"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogArticleTagAsync(List<BlogArticleTag> blogArticleTags)
    {
        blogArticleTags.ForEach(blogArticleTag =>
        {
            blogArticleTag.SoftDeleteLock = false;
        });
        var result = await _IBlogArticleTagRepository.CreateBatchAsync(blogArticleTags);
        return result;
    }

    /// <summary>
    /// 新增博客文章标签
    /// </summary>
    /// <param name="blogArticleTag"></param>
    /// <returns></returns>
    public async Task<bool> CreateBlogArticleTagAsync(BlogArticleTag blogArticleTag)
    {
        await _IBlogArticleService.IsExistenceAsync(blogArticleTag.ArticleId);
        await _IBlogTagService.IsExistenceAsync(blogArticleTag.TagId);
        blogArticleTag.SoftDeleteLock = false;
        var result = await _IBlogArticleTagRepository.CreateAsync(blogArticleTag);
        return result;
    }

    /// <summary>
    /// 删除博客文章标签
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogArticleTagAsync(Guid guid, Guid deleteId)
    {
        var blogArticleTag = await IsExistenceAsync(guid);
        blogArticleTag.SoftDeleteLock = true;
        blogArticleTag.DeleteId = deleteId;
        blogArticleTag.DeleteTime = DateTime.Now;
        return await _IBlogArticleTagRepository.UpdateAsync(blogArticleTag);
    }

    /// <summary>
    /// 修改博客文章标签
    /// </summary>
    /// <param name="blogArticleTag"></param>
    /// <returns></returns>
    public async Task<BlogArticleTag> ModifyBlogArticleTagAsync(BlogArticleTag blogArticleTag)
    {
        await IsExistenceAsync(blogArticleTag.BaseId);
        await _IBlogArticleService.IsExistenceAsync(blogArticleTag.ArticleId);
        await _IBlogTagService.IsExistenceAsync(blogArticleTag.TagId);
        var result = await _IBlogArticleTagRepository.UpdateAsync(blogArticleTag);
        if (result) blogArticleTag = await _IBlogArticleTagRepository.FindAsync(blogArticleTag.BaseId);
        return blogArticleTag;
    }

    /// <summary>
    /// 查找博客文章标签
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogArticleTag> FindBlogArticleTagAsync(Guid guid)
    {
        var blogArticleTag = await IsExistenceAsync(guid);
        return blogArticleTag;
    }

    /// <summary>
    /// 根据文章Id查寻所有标签
    /// </summary>
    /// <param name="articleGuid"></param>
    /// <returns></returns>
    public async Task<List<BlogTag>> QueryBlogArticleTagAsync(Guid articleGuid)
    {
        var blogArticleTag = from blogarticletag in await _IBlogArticleTagRepository.QueryListAsync(e => !e.SoftDeleteLock && e.ArticleId == articleGuid)
                             join blogtag in await _IBlogTagService.QueryListAsync(e => !e.SoftDeleteLock) on blogarticletag.TagId equals blogtag.BaseId
                             orderby blogarticletag.CreateTime descending
                             select new BlogTag
                             {
                                 TagName = blogtag.TagName,
                             };
        return blogArticleTag.ToList();
    }

    /// <summary>
    /// 查询所有博客文章标签
    /// </summary>
    /// <returns></returns>
    public async Task<List<BlogArticleTag>> QueryBlogArticleTagAsync()
    {
        var blogArticleTag = from blogarticletag in await _IBlogArticleTagRepository.QueryListAsync(e => !e.SoftDeleteLock)
                             orderby blogarticletag.CreateTime descending
                             select blogarticletag;
        return blogArticleTag.ToList();
    }
}