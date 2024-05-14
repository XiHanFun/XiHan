#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IXiHanModule
// Guid:85ec6b99-74ea-418f-a64c-c117dce646e3
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/19 1:46:20
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Modularity;

/// <summary>
/// 曦寒模块化服务配置接口
/// </summary>
public interface IXiHanModule
{
    /// <summary>
    /// 服务配置，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ConfigureServicesAsync(ServiceConfigurationContext context);

    /// <summary>
    /// 服务配置
    /// </summary>
    /// <param name="context"></param>
    void ConfigureServices(ServiceConfigurationContext context);
}