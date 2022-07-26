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
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(c => c.ParentId == blogCategory.ParentId && c.SoftDeleteLock == false) == null)
            throw new ApplicationException("父级文章分类不存在");
        if (await _IBlogCategoryRepository.FindAsync(ua => ua.Name == blogCategory.Name) != null)
            throw new ApplicationException("文章分类名称已存在");
        blogCategory.SoftDeleteLock = false;
        var result = await _IBlogCategoryRepository.CreateAsync(blogCategory);
        return result;
    }

    public async Task<bool> DeleteBlogCategoryAsync(Guid guid, Guid deleteId)
    {
        var blogCategory = await _IBlogCategoryRepository.FindAsync(c => c.BaseId == guid && c.SoftDeleteLock == false);
        if (blogCategory == null)
            throw new ApplicationException("文章分类不存在");
        if ((await _IBlogCategoryRepository.QueryAsync(c => c.ParentId == blogCategory.ParentId && c.SoftDeleteLock == false)).Count != 0)
            throw new ApplicationException("该文章分类下有子文章分类，不能删除");
        if ((await _IBlogArticleRepository.QueryAsync(e => e.CategoryId == blogCategory.BaseId)).Count != 0)
            throw new ApplicationException("该文章分类已有文章使用，不能删除");
        var rootState = await _IRootStateRepository.FindAsync(r => r.TypeKey == "All" && r.StateKey == -1);
        blogCategory.SoftDeleteLock = true;
        blogCategory.DeleteId = deleteId;
        blogCategory.DeleteTime = DateTime.Now;
        blogCategory.StateId = rootState.BaseId;
        return await _IBlogCategoryRepository.UpdateAsync(blogCategory);
    }

    public async Task<BlogCategory> ModifyBlogCategoryAsync(BlogCategory blogCategory)
    {
        if (await _IBlogCategoryRepository.FindAsync(c => c.BaseId == blogCategory.BaseId && c.SoftDeleteLock == false) == null)
            throw new ApplicationException("文章分类不存在");
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(c => c.ParentId == blogCategory.ParentId && c.SoftDeleteLock == false) == null)
            throw new ApplicationException("父级文章分类不存在");
        if (await _IBlogCategoryRepository.FindAsync(ua => ua.Name == blogCategory.Name) != null)
            throw new ApplicationException("文章分类名称已存在");
        var result = await _IBlogCategoryRepository.UpdateAsync(blogCategory);
        if (result) blogCategory = await _IBlogCategoryRepository.FindAsync(blogCategory.BaseId);
        return blogCategory;
    }

    public async Task<BlogCategory> FindBlogCategoryAsync(Guid guid)
    {
        var blogCategory = await _IBlogCategoryRepository.FindAsync(c => c.BaseId == guid && c.SoftDeleteLock == false);
        if (blogCategory == null)
            throw new ApplicationException("文章分类不存在");
        return blogCategory;
    }

    public async Task<List<BlogCategory>> QueryBlogCategoryAsync()
    {
        var blogCategory = from userauthority in await _IBlogCategoryRepository.QueryAsync(c => c.SoftDeleteLock == false)
                           orderby userauthority.CreateTime descending
                           orderby userauthority.Name descending
                           select userauthority;
        return blogCategory.ToList();
    }
}