// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:CustomAutoMapperExtension
// Guid:4960fc12-c08b-426e-abf1-efaf35db4d9f
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:57:37
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.Models.Blogs;
using ZhaiFanhuaBlog.Models.Roots;
using ZhaiFanhuaBlog.Models.Users;
using ZhaiFanhuaBlog.Utils.Formats;
using ZhaiFanhuaBlog.ViewModels.Blogs;
using ZhaiFanhuaBlog.ViewModels.Roots;
using ZhaiFanhuaBlog.ViewModels.Users;

namespace ZhaiFanhuaBlog.WebApi.Common.Extensions.DependencyInjection;

/// <summary>
/// CustomAutoMapperExtension
/// </summary>
public static class CustomAutoMapperExtension
{
    /// <summary>
    /// AutoMapper服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
    {
        // 创建具体的映射对象
        services.AddAutoMapper(mapper =>
        {
            // Root
            mapper.CreateMap<RootRole, CRootRoleDto>().ReverseMap();
            mapper.CreateMap<RootRole, RRootRoleDto>().ReverseMap();
            mapper.CreateMap<RootAuthority, CRootAuthorityDto>().ReverseMap();
            mapper.CreateMap<RootAuthority, RRootAuthorityDto>().ReverseMap();
            mapper.CreateMap<RootAuthority, CRootAuthorityDto>().ReverseMap();
            mapper.CreateMap<RootAuthority, RRootAuthorityDto>().ReverseMap();
            mapper.CreateMap<RootRoleAuthority, CRootRoleAuthorityDto>().ReverseMap();
            mapper.CreateMap<RootRoleAuthority, RRootRoleAuthorityDto>().ReverseMap();

            // User
            mapper.CreateMap<UserAccount, CUserAccountDto>().ReverseMap();
            mapper.CreateMap<UserAccount, RUserAccountDto>()
            .ForMember(u => u.RegisterIp, d => d.MapFrom(o => o.RegisterIp == null ? string.Empty : IpFormatHelper.FormatByteToString(o.RegisterIp)))
            .ReverseMap();
            mapper.CreateMap<UserAccountRole, CUserAccountRoleDto>().ReverseMap();
            mapper.CreateMap<UserAccountRole, RUserAccountRoleDto>().ReverseMap();

            // Blog
            mapper.CreateMap<BlogCategory, CBlogCategoryDto>().ReverseMap();
            mapper.CreateMap<BlogCategory, RBlogCategoryDto>().ReverseMap();
            mapper.CreateMap<BlogArticle, CBlogArticleDto>().ReverseMap();
            mapper.CreateMap<BlogArticle, RBlogArticleDto>().ReverseMap();
            mapper.CreateMap<BlogTag, CBlogTagDto>().ReverseMap();
            mapper.CreateMap<BlogTag, RBlogTagDto>().ReverseMap();
            mapper.CreateMap<BlogArticleTag, CBlogArticleTagDto>().ReverseMap();
            mapper.CreateMap<BlogArticleTag, RBlogArticleTagDto>().ReverseMap();
            mapper.CreateMap<BlogPoll, CBlogPollDto>().ReverseMap();
            mapper.CreateMap<BlogPoll, RBlogPollDto>().ReverseMap();
            mapper.CreateMap<BlogComment, CBlogCommentDto>().ReverseMap();
            mapper.CreateMap<BlogComment, RBlogCommentDto>()
            .ForMember(u => u.CommentIp, d => d.MapFrom(o => o.CommentIp == null ? string.Empty : IpFormatHelper.FormatByteToString(o.CommentIp)))
            .ReverseMap();
            mapper.CreateMap<BlogCommentPoll, CBlogCommentPollDto>().ReverseMap();
            mapper.CreateMap<BlogCommentPoll, RBlogCommentPollDto>().ReverseMap();
        });
        return services;
    }
}