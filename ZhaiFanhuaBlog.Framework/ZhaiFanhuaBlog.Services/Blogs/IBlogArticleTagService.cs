// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogArticleTagService
// Guid:881c7f18-8d3e-2c9c-cf06-9e6762e4f052
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:35:20
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogArticleTagService
/// </summary>
public interface IBlogArticleTagService : IBaseService<BlogArticleTag>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogArticleTag> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogArticleTags"></param>
    /// <returns></returns>
    Task<bool> InitBlogArticleTagAsync(List<BlogArticleTag> blogArticleTags);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogArticleTag"></param>
    /// <returns></returns>
    Task<bool> CreateBlogArticleTagAsync(BlogArticleTag blogArticleTag);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogArticleTagAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogArticleTag"></param>
    /// <returns></returns>
    Task<BlogArticleTag> ModifyBlogArticleTagAsync(BlogArticleTag blogArticleTag);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogArticleTag> FindBlogArticleTagAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<BlogArticleTag>> QueryBlogArticleTagAsync();
}