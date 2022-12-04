#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:AutoMapperProfile
// Guid:406c1b6d-00cb-4d8a-b7ee-3abd0a6c0c76
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-29 上午 12:13:27
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using AutoMapper;
using ZhaiFanhuaBlog.Extensions.Common.Auth;

namespace ZhaiFanhuaBlog.Extensions.Common.AutoMapper;

/// <summary>
/// AutoMapperProfile
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// 配置构造函数，用来创建关系映射
    /// </summary>
    public AutoMapperProfile()
    {
        //// Root
        //CreateMap<RootRole, CRootRoleDto>().ReverseMap();
        //CreateMap<RootRole, RRootRoleDto>().ReverseMap();
        //CreateMap<RootAuthority, CRootAuthorityDto>().ReverseMap();
        //CreateMap<RootAuthority, RRootAuthorityDto>().ReverseMap();
        //CreateMap<RootAuthority, CRootAuthorityDto>().ReverseMap();
        //CreateMap<RootAuthority, RRootAuthorityDto>().ReverseMap();
        //CreateMap<RootRoleAuthority, CRootRoleAuthorityDto>().ReverseMap();
        //CreateMap<RootRoleAuthority, RRootRoleAuthorityDto>().ReverseMap();

        //// User
        //CreateMap<UserAccount, CUserAccountDto>().ReverseMap();
        //CreateMap<UserAccount, RUserAccountDto>().ReverseMap();
        //CreateMap<UserAccountRole, CUserAccountRoleDto>().ReverseMap();
        //CreateMap<UserAccountRole, RUserAccountRoleDto>().ReverseMap();

        //// Blog
        //CreateMap<BlogCategory, CBlogCategoryDto>().ReverseMap();
        //CreateMap<BlogCategory, RBlogCategoryDto>().ReverseMap();
        //CreateMap<BlogArticle, CBlogArticleDto>().ReverseMap();
        //CreateMap<BlogArticle, RBlogArticleDto>().ReverseMap();
        //CreateMap<BlogTag, CBlogTagDto>().ReverseMap();
        //CreateMap<BlogTag, RBlogTagDto>().ReverseMap();
        //CreateMap<BlogArticleTag, CBlogArticleTagDto>().ReverseMap();
        //CreateMap<BlogArticleTag, RBlogArticleTagDto>().ReverseMap();
        //CreateMap<BlogPoll, CBlogPollDto>().ReverseMap();
        //CreateMap<BlogPoll, RBlogPollDto>().ReverseMap();
        //CreateMap<BlogComment, CBlogCommentDto>().ReverseMap();
        //CreateMap<BlogComment, RBlogCommentDto>().ReverseMap();
        //CreateMap<BlogCommentPoll, CBlogCommentPollDto>().ReverseMap();
        //CreateMap<BlogCommentPoll, RBlogCommentPollDto>().ReverseMap();

        //// Sys
        //CreateMap<SysConfiguration, CSysConfigurationDto>().ReverseMap();

        //// Jwt
        //CreateMap<RUserAccountDto, TokenModel>()
        //    .ForMember(dest => dest.UserId, sourse => sourse.MapFrom(src => src.BaseId))
        //    .ForMember(dest => dest.RootRoles, sourse => sourse.MapFrom(src => string.Join(',', src.RootRoles == null ? string.Empty : src.RootRoles.Select(r => r.RoleName).ToList())))
        //    .ReverseMap();
    }
}