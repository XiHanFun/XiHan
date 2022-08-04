// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCategoryService
// Guid:bd858f38-c035-4a4b-935c-d0b077a68113
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:00
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCategoryService
/// </summary>
public class BlogCategoryService : BaseService<BlogCategory>, IBlogCategoryService
{
    private readonly IRootStateRepository _IRootStateRepository;
    private readonly IBlogCategoryRepository _IBlogCategoryRepository;
    private readonly IBlogArticleRepository _IBlogArticleRepository;

    public BlogCategoryService(IRootStateRepository iRootStateRepository,
        IBlogCategoryRepository iBlogCategoryRepository,
        IBlogArticleRepository iBlogArticleRepository)
    {
        base._IBaseRepository = iBlogCategoryRepository;
        _IRootStateRepository = iRootStateRepository;
        _IBlogCategoryRepository = iBlogCategoryRepository;
        _IBlogArticleRepository = iBlogArticleRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<BlogCategory> IsExistenceAsync(Guid guid)
    {
        var blogCategory = await _IBlogCategoryRepository.FindAsync(e => e.BaseId == guid && !e.SoftDeleteLock);
        if (blogCategory == null)
            throw new ApplicationException("博客文章分类不存在");
        return blogCategory;
    }

    public async Task<bool> InitBlogCategoryAsync(List<BlogCategory> blogCategories)
    {
        blogCategories.ForEach(blogCategory =>
        {
            blogCategory.SoftDeleteLock = false;
        });
        var result = await _IBlogCategoryRepository.CreateBatchAsync(blogCategories);
        return result;
    }

    public async Task<bool> CreateBlogCategoryAsync(BlogCategory blogCategory)
    {
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(e => e.ParentId == blogCategory.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级博客文章分类不存在");
        if (await _IBlogCategoryRepository.FindAsync(e => e.Name == blogCategory.Name && !e.SoftDeleteLock) != null)
            throw new ApplicationException("博客文章分类名称已存在");
        blogCategory.SoftDeleteLock = false;
        var result = await _IBlogCategoryRepository.CreateAsync(blogCategory);
        return result;
    }

    public async Task<bool> DeleteBlogCategoryAsync(Guid guid, Guid deleteId)
    {
        var blogCategory = await IsExistenceAsync(guid);
        if ((await _IBlogCategoryRepository.QueryListAsync(e => e.ParentId == blogCategory.ParentId && !e.SoftDeleteLock)).Count != 0)
            throw new ApplicationException("该博客文章分类下有子博客文章分类，不能删除");
        if ((await _IBlogArticleRepository.QueryListAsync(e => e.CategoryId == blogCategory.BaseId && !e.SoftDeleteLock)).Count != 0)
            throw new ApplicationException("该博客文章分类已有博客文章使用，不能删除");
        var rootState = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1);
        blogCategory.SoftDeleteLock = true;
        blogCategory.DeleteId = deleteId;
        blogCategory.DeleteTime = DateTime.Now;
        blogCategory.StateId = rootState.BaseId;
        return await _IBlogCategoryRepository.UpdateAsync(blogCategory);
    }

    public async Task<BlogCategory> ModifyBlogCategoryAsync(BlogCategory blogCategory)
    {
        await IsExistenceAsync(blogCategory.BaseId);
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(e => e.ParentId == blogCategory.ParentId && !e.SoftDeleteLock) == null)
            throw new ApplicationException("父级博客文章分类不存在");
        if (await _IBlogCategoryRepository.FindAsync(e => e.Name == blogCategory.Name && !e.SoftDeleteLock) != null)
            throw new ApplicationException("博客文章分类名称已存在");
        var result = await _IBlogCategoryRepository.UpdateAsync(blogCategory);
        if (result) blogCategory = await _IBlogCategoryRepository.FindAsync(blogCategory.BaseId);
        return blogCategory;
    }

    public async Task<BlogCategory> FindBlogCategoryAsync(Guid guid)
    {
        var blogCategory = await IsExistenceAsync(guid);
        return blogCategory;
    }

    public async Task<List<BlogCategory>> QueryBlogCategoryAsync()
    {
        var blogCategory = from blogcategory in await _IBlogCategoryRepository.QueryListAsync(e => !e.SoftDeleteLock)
                           orderby blogcategory.CreateTime descending
                           orderby blogcategory.Name descending
                           select blogcategory;
        return blogCategory.ToList();
    }
}