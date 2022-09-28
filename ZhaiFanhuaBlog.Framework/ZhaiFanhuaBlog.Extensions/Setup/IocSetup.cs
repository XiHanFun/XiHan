// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:IocSetup
// Guid:2340e05b-ffd7-4a19-84bc-c3f73517b696
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-30 上午 02:48:50
// ----------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using ZhaiFanhuaBlog.IRepositories.Blogs;
using ZhaiFanhuaBlog.IRepositories.Roots;
using ZhaiFanhuaBlog.IRepositories.Sites;
using ZhaiFanhuaBlog.IRepositories.Users;
using ZhaiFanhuaBlog.IServices.Blogs;
using ZhaiFanhuaBlog.IServices.Roots;
using ZhaiFanhuaBlog.IServices.Sites;
using ZhaiFanhuaBlog.IServices.Users;
using ZhaiFanhuaBlog.Repositories.Blogs;
using ZhaiFanhuaBlog.Repositories.Roots;
using ZhaiFanhuaBlog.Repositories.Sites;
using ZhaiFanhuaBlog.Repositories.Users;
using ZhaiFanhuaBlog.Services.Blogs;
using ZhaiFanhuaBlog.Services.Roots;
using ZhaiFanhuaBlog.Services.Sites;
using ZhaiFanhuaBlog.Services.Users;

namespace ZhaiFanhuaBlog.Setups;

/// <summary>
/// IocSetup
/// </summary>
public static class IocSetup
{
    /// <summary>
    /// IOC服务扩展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddIocSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // =========================Site=========================
        // Repository
        services.AddScoped<ISiteConfigurationRepository, SiteConfigurationRepository>();
        services.AddScoped<ISiteLogRepository, SiteLogRepository>();
        services.AddScoped<ISiteSkinRepository, SiteSkinRepository>();
        // Service
        services.AddScoped<ISiteConfigurationService, SiteConfigurationService>();
        services.AddScoped<ISiteLogService, SiteLogService>();
        services.AddScoped<ISiteSkinService, SiteSkinService>();

        // =========================Root=========================
        // Repository
        services.AddScoped<IRootAuthorityRepository, RootAuthorityRepository>();
        services.AddScoped<IRootRoleRepository, RootRoleRepository>();
        services.AddScoped<IRootRoleAuthorityRepository, RootRoleAuthorityRepository>();
        services.AddScoped<IRootMenuRepository, RootMenuRepository>();
        services.AddScoped<IRootRoleMenuRepository, RootRoleMenuRepository>();
        services.AddScoped<IRootAnnouncementRepository, RootAnnouncementRepository>();
        services.AddScoped<IRootAuditRepository, RootAuditRepository>();
        services.AddScoped<IRootAuditCategoryRepository, RootAuditCategoryRepository>();
        services.AddScoped<IRootFriendlyLinkRepository, RootFriendlyLinkRepository>();
        services.AddScoped<IRootStateRepository, RootStateRepository>();
        // Service
        services.AddScoped<IRootAuthorityService, RootAuthorityService>();
        services.AddScoped<IRootRoleService, RootRoleService>();
        services.AddScoped<IRootRoleAuthorityService, RootRoleAuthorityService>();
        services.AddScoped<IRootMenuService, RootMenuService>();
        services.AddScoped<IRootRoleMenuService, RootRoleMenuService>();
        services.AddScoped<IRootAnnouncementService, RootAnnouncementService>();
        services.AddScoped<IRootAuditService, RootAuditService>();
        services.AddScoped<IRootAuditCategoryService, RootAuditCategoryService>();
        services.AddScoped<IRootFriendlyLinkService, RootFriendlyLinkService>();
        services.AddScoped<IRootStateService, RootStateService>();

        // =========================User=========================
        // Repository
        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        services.AddScoped<IUserAccountRoleRepository, UserAccountRoleRepository>();
        services.AddScoped<IUserCollectCategoryRepository, UserCollectCategoryRepository>();
        services.AddScoped<IUserCollectRepository, UserCollectRepository>();
        services.AddScoped<IUserFollowRepository, UserFollowRepository>();
        services.AddScoped<IUserLoginRepository, UserLoginRepository>();
        services.AddScoped<IUserNoticeRepository, UserNoticeRepository>();
        services.AddScoped<IUserOauthRepository, UserOauthRepository>();
        services.AddScoped<IUserStatisticRepository, UserStatisticRepository>();
        // Service
        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddScoped<IUserAccountRoleService, UserAccountRoleService>();
        services.AddScoped<IUserCollectCategoryService, UserCollectCategoryService>();
        services.AddScoped<IUserCollectService, UserCollectService>();
        services.AddScoped<IUserFollowService, UserFollowService>();
        services.AddScoped<IUserLoginService, UserLoginService>();
        services.AddScoped<IUserNoticeService, UserNoticeService>();
        services.AddScoped<IUserOauthService, UserOauthService>();
        services.AddScoped<IUserStatisticService, UserStatisticService>();

        // =========================Blog=========================
        // Repository
        services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
        services.AddScoped<IBlogArticleRepository, BlogArticleRepository>();
        services.AddScoped<IBlogTagRepository, BlogTagRepository>();
        services.AddScoped<IBlogArticleTagRepository, BlogArticleTagRepository>();
        services.AddScoped<IBlogPollRepository, BlogPollRepository>();
        services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
        services.AddScoped<IBlogCommentPollRepository, BlogCommentPollRepository>();
        // Service
        services.AddScoped<IBlogCategoryService, BlogCategoryService>();
        services.AddScoped<IBlogArticleService, BlogArticleService>();
        services.AddScoped<IBlogTagService, BlogTagService>();
        services.AddScoped<IBlogArticleTagService, BlogArticleTagService>();
        services.AddScoped<IBlogPollService, BlogPollService>();
        services.AddScoped<IBlogCommentService, BlogCommentService>();
        services.AddScoped<IBlogCommentPollService, BlogCommentPollService>();

        return services;
    }
}