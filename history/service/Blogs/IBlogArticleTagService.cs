#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogArticleTagService
// Guid:881c7f18-8d3e-2c9c-cf06-9e6762e4f052
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:35:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogArticleTagService
/// </summary>
public interface IBlogArticleTagService : IBaseService<PostTag>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostTag> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogArticleTags"></param>
    /// <returns></returns>
    Task<bool> InitBlogArticleTagAsync(List<PostTag> blogArticleTags);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogArticleTag"></param>
    /// <returns></returns>
    Task<bool> CreateBlogArticleTagAsync(PostTag blogArticleTag);

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
    Task<PostTag> ModifyBlogArticleTagAsync(PostTag blogArticleTag);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostTag> FindBlogArticleTagAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<PostTag>> QueryBlogArticleTagAsync();
}