#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InfrastructurePersistenceModuleInitializer
// Guid:baa046da-681d-478e-bd3e-eead2f3dcbe8
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 9:36:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XiHan.Common.Core.Modules.Abstracts;
using XiHan.Domain.Core.Repositories.Abstracts;
using XiHan.Infrastructure.Persistence.Repositories;

namespace XiHan.Infrastructure.Persistence;

/// <summary>
/// 基础设施数据持久化模块初始化
/// </summary>
public class InfrastructurePersistenceModuleInitializer : IModuleInitializer
{
    /// <summary>
    /// 初始化服务配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //因为是泛型，所以不能用AddScoped<IBaseRepository<>,typeof(BaseRepository<>>这种方式注册
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }

    /// <summary>
    /// 配置应用程序
    /// </summary>
    public void Configure(IApplicationBuilder builder, IWebHostEnvironment environment)
    {
    }
}