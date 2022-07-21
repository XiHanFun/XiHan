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
        var state = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 1);
        blogCategories.ForEach(blogCategory =>
        {
            blogCategory.SoftDeleteLock = false;
            blogCategory.StateGuid = state.BaseId;
        });
        var result = await _IBlogCategoryRepository.CreateBatchAsync(blogCategories);
        return result;
    }

    public async Task<bool> CreateBlogCategoryAsync(BlogCategory blogCategory)
    {
        var state = await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == 1);
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(blogCategory.ParentId) == null)
            throw new ApplicationException("父级分类不存在");
        if (await _IBlogCategoryRepository.FindAsync(ua => ua.Name == blogCategory.Name) != null)
            throw new ApplicationException("分类名称已存在");
        blogCategory.SoftDeleteLock = false;
        blogCategory.StateGuid = state.BaseId;
        var result = await _IBlogCategoryRepository.CreateAsync(blogCategory);
        return result;
    }

    public async Task<bool> DeleteBlogCategoryAsync(Guid guid)
    {
        var blogCategory = await _IBlogCategoryRepository.FindAsync(guid);
        if (blogCategory == null)
            throw new ApplicationException("分类不存在");
        if ((await _IBlogCategoryRepository.QueryAsync(e => e.ParentId == guid)).Count != 0)
            throw new ApplicationException("该分类下有子分类，不能删除");
        if ((await _IBlogArticleRepository.QueryAsync(e => e.CategoryId == blogCategory.BaseId)).Count != 0)
            throw new ApplicationException("该分类已有文章使用，不能删除");
        if (blogCategory.SoftDeleteLock)
        {
            blogCategory.StateGuid = (await _IRootStateRepository.FindAsync(e => e.TypeKey == "All" && e.StateKey == -1)).BaseId;
            blogCategory.DeleteTime = DateTime.Now;
            return await _IBlogCategoryRepository.UpdateAsync(blogCategory);
        }
        else
        {
            return await _IBlogCategoryRepository.DeleteAsync(guid);
        }
    }

    public async Task<BlogCategory> ModifyBlogCategoryAsync(BlogCategory blogCategory)
    {
        if (await _IBlogCategoryRepository.FindAsync(blogCategory.BaseId) == null)
            throw new ApplicationException("分类不存在");
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(blogCategory.ParentId) == null)
            throw new ApplicationException("父级分类不存在");
        if (await _IBlogCategoryRepository.FindAsync(ua => ua.Name == blogCategory.Name) != null)
            throw new ApplicationException("分类名称已存在");
        blogCategory.ModifyTime = DateTime.Now;
        var result = await _IBlogCategoryRepository.UpdateAsync(blogCategory);
        if (result) blogCategory = await _IBlogCategoryRepository.FindAsync(blogCategory.BaseId);
        return blogCategory;
    }

    public async Task<BlogCategory> FindBlogCategoryAsync(Guid guid)
    {
        var blogCategory = await _IBlogCategoryRepository.FindAsync(guid);
        return blogCategory;
    }

    public async Task<List<BlogCategory>> QueryBlogCategoriesAsync()
    {
        var blogCategory = from userauthority in await _IBlogCategoryRepository.QueryAsync()
                           join rootstate in await _IRootStateRepository.QueryAsync() on userauthority.StateGuid equals rootstate.BaseId
                           where rootstate.StateKey == 1
                           orderby userauthority.ParentId descending
                           orderby userauthority.CreateTime descending
                           orderby userauthority.Name descending
                           select userauthority;
        return blogCategory.ToList();
    }
}