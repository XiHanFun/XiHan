// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogPollService
// Guid:616bdfe4-38be-9148-ec73-ed849755304d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:25:24
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Services.Bases;
using ZhaiFanhuaBlog.Utils.Services;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogPollService
/// </summary>
public interface IBlogPollService : IBaseService<BlogPoll>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogPoll> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogPolls"></param>
    /// <returns></returns>
    Task<bool> InitBlogPollAsync(List<BlogPoll> blogPolls);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogPoll"></param>
    /// <returns></returns>
    Task<bool> CreateBlogPollAsync(BlogPoll blogPoll);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogPollAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogPoll"></param>
    /// <returns></returns>
    Task<BlogPoll> ModifyBlogPollAsync(BlogPoll blogPoll);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogPoll> FindBlogPollAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<BlogPoll>> QueryBlogPollAsync();
}