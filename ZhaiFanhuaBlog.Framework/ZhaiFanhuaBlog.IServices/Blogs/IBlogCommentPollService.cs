﻿// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IBlogCommentPollService
// Guid:704d0706-ea77-c6ed-17c8-93d7ad94eb77
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2021-12-28 下午 11:10:45
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IServices.Bases;
using ZhaiFanhuaBlog.Models.Blogs;

namespace ZhaiFanhuaBlog.IServices.Blogs;

/// <summary>
/// IBlogCommentPollService
/// </summary>
public interface IBlogCommentPollService : IBaseService<BlogCommentPoll>
{
    /// <summary>
    /// 检验是否存在
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogCommentPoll> IsExistenceAsync(Guid guid);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="blogCommentPolls"></param>
    /// <returns></returns>
    Task<bool> InitBlogCommentPollAsync(List<BlogCommentPoll> blogCommentPolls);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    Task<bool> CreateBlogCommentPollAsync(BlogCommentPoll blogCommentPoll);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="deleteId"></param>
    /// <returns></returns>
    Task<bool> DeleteBlogCommentPollAsync(Guid guid, Guid deleteId);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="blogCommentPoll"></param>
    /// <returns></returns>
    Task<BlogCommentPoll> ModifyBlogCommentPollAsync(BlogCommentPoll blogCommentPoll);

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<BlogCommentPoll> FindBlogCommentPollAsync(Guid guid);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<BlogCommentPoll>> QueryBlogCommentPollAsync();
}