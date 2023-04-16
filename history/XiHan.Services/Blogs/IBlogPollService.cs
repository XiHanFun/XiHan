#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogPollService
// Guid:616bdfe4-38be-9148-ec73-ed849755304d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:25:24
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using ZhaiFanhuaBlog.Infrastructure.Services;
using ZhaiFanhuaBlog.Models.Posts;
using ZhaiFanhuaBlog.Services.Bases;

namespace ZhaiFanhuaBlog.Services.Blogs;

/// <summary>
/// IBlogPollService
/// </summary>
public interface IBlogPollService : IBaseService<PostPoll>, IScopeDependency
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostPoll> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogPolls"></param>
    /// <returns></returns>
    Task<bool> InitBlogPollAsync(List<PostPoll> blogPolls);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogPoll"></param>
    /// <returns></returns>
    Task<bool> CreateBlogPollAsync(PostPoll blogPoll);

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
    Task<PostPoll> ModifyBlogPollAsync(PostPoll blogPoll);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<PostPoll> FindBlogPollAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<PostPoll>> QueryBlogPollAsync();
}