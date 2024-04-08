#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IModuleInitializer
// Guid:fcd576c5-238c-4d6f-899b-dcff94141e49
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 1:23:45
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace XiHan.Common.Core.Modules.Abstracts;

/// <summary>
/// 模块初始化配置接口
/// </summary>
/// <remarks>
/// 所有项目中实现了 IModuleInitializer 的都会被调用，请在 ConfigureServices 中编写注册自身模块需要的服务，建议一个项目中只放一个实现了 IModuleInitializer 的类
/// </remarks>
public interface IModuleInitializer
{
    /// <summary>
    /// 获取初始化参数
    /// </summary>
    /// <returns></returns>
    IEnumerable<IInitializerOptions> GetInitializerOptions();

    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    void InitializeServices(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    /// 配置应用程序
    /// </summary>
    void Initialize(IApplicationBuilder builder, IWebHostEnvironment environment);
}