#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BlogCategoryService
// Guid:bd858f38-c035-4a4b-935c-d0b077a68113
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-04 下午 06:19:00
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// BlogCategoryService
/// </summary>
public class BlogCategoryService : BaseService<PostCategory>, IBlogCategoryService
{
    private readonly IBlogCategoryRepository _IBlogCategoryRepository;
    private readonly IBlogArticleRepository _IBlogArticleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="iBlogCategoryRepository"></param>
    /// <param name="iBlogArticleRepository"></param>
    public BlogCategoryService(IBlogCategoryRepository iBlogCategoryRepository,
        IBlogArticleRepository iBlogArticleRepository)
    {
        base._IBaseRepository = iBlogCategoryRepository;
        _IBlogCategoryRepository = iBlogCategoryRepository;
        _IBlogArticleRepository = iBlogArticleRepository;
    }

    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostCategory> IsExistenceAsync(Guid guid)
    {
        var blogCategory = await _IBlogCategoryRepository.FindAsync(e => e.BaseId == guid && !e.SoftDelete);
        if (blogCategory == null)
        {
            throw new ApplicationException("博客文章分类不存在");
        }
        return blogCategory;
    }

    /// <summary>
    /// 初始化博客分类数据
    /// </summary>
    /// <param name="blogCategories"></param>
    /// <returns></returns>
    public async Task<bool> InitBlogCategoryAsync(List<PostCategory> blogCategories)
    {
        blogCategories.ForEach(blogCategory =>
        {
            blogCategory.SoftDelete = false;
        });
        var result = await _IBlogCategoryRepository.CreateBatchAsync(blogCategories);
        return result;
    }

    /// <summary>
    /// 新增博客分类
    /// </summary>
    /// <param name="blogCategory"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> CreateBlogCategoryAsync(PostCategory blogCategory)
    {
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(e => e.ParentId == blogCategory.ParentId && !e.SoftDelete) == null)
        {
            throw new ApplicationException("父级博客文章分类不存在");
        }
        if (await _IBlogCategoryRepository.FindAsync(e => e.CategoryName == blogCategory.CategoryName && !e.SoftDelete) != null)
        {
            throw new ApplicationException("博客文章分类名称已存在");
        }
        blogCategory.SoftDelete = false;
        var result = await _IBlogCategoryRepository.CreateAsync(blogCategory);
        return result;
    }

    /// <summary>
    /// 删除博客分类
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<bool> DeleteBlogCategoryAsync(Guid guid, Guid deleteId)
    {
        var blogCategory = await IsExistenceAsync(guid);
        if ((await _IBlogCategoryRepository.QueryListAsync(e => e.ParentId == blogCategory.ParentId && !e.SoftDelete)).Count != 0)
        {
            throw new ApplicationException("该博客文章分类下有子博客文章分类，不能删除");
        }
        if ((await QueryListAsync(e => e.CategoryId == blogCategory.BaseId && !e.SoftDelete)).Count != 0)
        {
            throw new ApplicationException("该博客文章分类已有博客文章使用，不能删除");
        }
        blogCategory.SoftDelete = true;
        blogCategory.DeleteId = deleteId;
        blogCategory.DeleteTime = DateTime.Now;
        return await _IBlogCategoryRepository.UpdateAsync(blogCategory);
    }

    /// <summary>
    /// 修改博客分类
    /// </summary>
    /// <param name="blogCategory"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<PostCategory> ModifyBlogCategoryAsync(PostCategory blogCategory)
    {
        await IsExistenceAsync(blogCategory.BaseId);
        if (blogCategory.ParentId != null && await _IBlogCategoryRepository.FindAsync(e => e.ParentId == blogCategory.ParentId && !e.SoftDelete) == null)
        {
            throw new ApplicationException("父级博客文章分类不存在");
        }
        if (await _IBlogCategoryRepository.FindAsync(e => e.CategoryName == blogCategory.CategoryName && !e.SoftDelete) != null)
        {
            throw new ApplicationException("博客文章分类名称已存在");
        }
        var result = await _IBlogCategoryRepository.UpdateAsync(blogCategory);
        if (result)
        {
            blogCategory = await _IBlogCategoryRepository.FindAsync(blogCategory.BaseId);
        }
        return blogCategory;
    }

    /// <summary>
    /// 查找博客分类
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<PostCategory> FindBlogCategoryAsync(Guid guid)
    {
        var blogCategory = await IsExistenceAsync(guid);
        return blogCategory;
    }

    /// <summary>
    /// 查询博客分类
    /// </summary>
    /// <returns></returns>
    public async Task<List<PostCategory>> QueryBlogCategoryAsync()
    {
        var blogCategory = from blogcategory in await _IBlogCategoryRepository.QueryListAsync(e => !e.SoftDelete)
                           orderby blogcategory.CreateTime descending
                           orderby blogcategory.CategoryName descending
                           select blogcategory;
        return blogCategory.ToList();
    }
}