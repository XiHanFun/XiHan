#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PresentationWebHostModule
// Guid:ed5a2da7-56d9-4b04-b089-be0b8d2c7cd2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:07:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Domain.Core.Modules.Abstracts;

namespace XiHan.Presentation.Web.Host.Modules;

/// <summary>
/// 配置网站主机模块
/// </summary>
public class PresentationWebHostModule : IModule
{
    /// <summary>
    /// 应用配置
    /// </summary>
    /// <param name="app"></param>
    public void Configure(IApplicationBuilder app)
    {
    }

    ///<summary>
    /// 服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }
}