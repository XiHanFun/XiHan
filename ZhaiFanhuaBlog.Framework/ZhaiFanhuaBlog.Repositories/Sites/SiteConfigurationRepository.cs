// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:SiteConfigurationRepository
// Guid:4fb027f6-e110-4c6b-a713-19b0334e412d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 09:56:03
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.IRepositories.Sites;
using ZhaiFanhuaBlog.Models.Sites;
using ZhaiFanhuaBlog.Repositories.Bases;

namespace ZhaiFanhuaBlog.Repositories.Sites;

/// <summary>
/// SiteConfigurationRepository
/// </summary>
public class SiteConfigurationRepository : BaseRepository<SiteConfiguration>, ISiteConfigurationRepository
{
}