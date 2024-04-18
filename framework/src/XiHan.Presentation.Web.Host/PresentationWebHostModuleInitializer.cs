#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:PresentationWebHostModuleInitializer
// Guid:ed5a2da7-56d9-4b04-b089-be0b8d2c7cd2
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:07:01
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Core.Modularity.Abstracts;

namespace XiHan.Presentation.Web.Host;

/// <summary>
/// 配置网站主机模块初始化
/// </summary>
public class PresentationWebHostModuleInitializer : IModuleInitializer
{
    /// <summary>
    /// 初始化服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void InitializeServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    /// 配置应用程序
    /// </summary>
    public void Initialize(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
    }
}