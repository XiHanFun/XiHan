// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogTagService
// Guid:79d590ff-e314-4508-b1b3-8434eed357c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogTagService
/// </summary>
public class BlogTagService : BaseService<BlogTag>, IBlogTagService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IBlogTagRepository _IBlogTagRepository;

    public BlogTagService(IRootStateRepository iRootStateRepository, IBlogTagRepository iBlogTagRepository)
    {
        base._IBaseRepository = iBlogTagRepository;
        _IBlogTagRepository = iBlogTagRepository;
        _IRootStateRepository = iRootStateRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogTag> IsExistenceAsync(Guid guid)
    {
        var blogTag = await _IBlogTagRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogTag == null)
            throw new ApplicationException("博客标签不存在");
        return blogTag;
    }

    public async Task<bool> InitBlogTagAsync(List<BlogTag> blogTags)
    {
        blogTags.ForEach(blogTag =>
        {
            blogTag.SoftDeleteLock = false;
        });
        var result = await _IBlogTagRepository.CreateBatchAsync(blogTags);
        return result;
    }

    public async Task<bool> CreateBlogTagAsync(BlogTag blogTag)
    {
        if (await _IBlogTagRepository.FindAsync(e => e.TagName == blogTag.TagName && !e.SoftDeleteLock) != null)
            throw new ApplicationException("博客标签已存在");
        blogTag.SoftDeleteLock = false;
        var result = await _IBlogTagRepository.CreateAsync(blogTag);
        return result;
    }

    public async Task<bool> DeleteBlogTagAsync(Guid guid, Guid deleteId)
    {
        var blogTag = await IsExistenceAsync(guid);
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        blogTag.SoftDeleteLock = true;
        blogTag.DeleteId = deleteId;
        blogTag.DeleteTime = DateTime.Now;
        blogTag.StateId = rootState.BaseId;
        return await _IBlogTagRepository.UpdateAsync(blogTag);
    }

    public async Task<BlogTag> ModifyBlogTagAsync(BlogTag blogTag)
    {
        await IsExistenceAsync(blogTag.BaseId);
        var result = await _IBlogTagRepository.UpdateAsync(blogTag);
        if (result) blogTag = await _IBlogTagRepository.FindAsync(blogTag.BaseId);
        return blogTag;
    }

    public async Task<BlogTag> FindBlogTagAsync(Guid guid)
    {
        var blogTag = await IsExistenceAsync(guid);
        return blogTag;
    }

    public async Task<List<BlogTag>> QueryBlogTagAsync()
    {
        var blogTag = from blogtag in await _IBlogTagRepository.QueryListAsync(e => !e.SoftDeleteLock)
                      orderby blogtag.CreateTime descending
                      select blogtag;
        return blogTag.ToList();
    }
}