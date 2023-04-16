#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogTagService
// Guid:79d590ff-e314-4508-b1b3-8434eed357c6
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogTagService
/// </summary>
public class BlogTagService : BaseService<PostTag>, IBlogTagService
{
    private readonly IBlogTagRepository _IBlogTagRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogTagRepository"></param>
    public BlogTagService(IBlogTagRepository iBlogTagRepository)
    {
        base._IBaseRepository = iBlogTagRepository;
        _IBlogTagRepository = iBlogTagRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostTag> IsExistenceAsync(Guid guid)
    {
        var blogTag = await _IBlogTagRepository.FindAsync(e => e.BaseId == guid && !e.SoftDelete);
        if (blogTag == null)
            throw new ApplicationException("博客标签不存在");
        return blogTag;
    }

    /// <summary>
    /// 初始化博客标签
    /// </summary>
    /// <param name="blogTags"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogTagAsync(List<PostTag> blogTags)
    {
        blogTags.ForEach(blogTag =>
        {
            blogTag.SoftDelete = false;
        });
        var result = await _IBlogTagRepository.CreateBatchAsync(blogTags);
        return result;
    }

    /// <summary>
    /// 新增博客标签
    /// </summary>
    /// <param name="blogTag"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateBlogTagAsync(PostTag blogTag)
    {
        if (await _IBlogTagRepository.FindAsync(e => e.TagName == blogTag.TagName && !e.SoftDelete) != null)
            throw new ApplicationException("博客标签已存在");
        blogTag.SoftDelete = false;
        var result = await _IBlogTagRepository.CreateAsync(blogTag);
        return result;
    }

    /// <summary>
    /// 删除博客标签
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBlogTagAsync(Guid guid, Guid deleteId)
    {
        var blogTag = await IsExistenceAsync(guid);
        blogTag.SoftDelete = true;
        blogTag.DeleteId = deleteId;
        blogTag.DeleteTime = DateTime.Now;
        return await _IBlogTagRepository.UpdateAsync(blogTag);
    }

    /// <summary>
    /// 修改博客标签
    /// </summary>
    /// <param name="blogTag"></param>
    /// <returns></returns>
    public async Task<PostTag> ModifyBlogTagAsync(PostTag blogTag)
    {
        await IsExistenceAsync(blogTag.BaseId);
        var result = await _IBlogTagRepository.UpdateAsync(blogTag);
        if (result) blogTag = await _IBlogTagRepository.FindAsync(blogTag.BaseId);
        return blogTag;
    }

    /// <summary>
    /// 查找博客标签
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostTag> FindBlogTagAsync(Guid guid)
    {
        var blogTag = await IsExistenceAsync(guid);
        return blogTag;
    }

    /// <summary>
    /// 查询博客标签
    /// </summary>
    /// <returns></returns>
    public async Task<List<PostTag>> QueryBlogTagAsync()
    {
        var blogTag = from blogtag in await _IBlogTagRepository.QueryListAsync(e => !e.SoftDelete)
                      orderby blogtag.CreateTime descending
                      select blogtag;
        return blogTag.ToList();
    }
}