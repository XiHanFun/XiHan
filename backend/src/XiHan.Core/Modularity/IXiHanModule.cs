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
/// IXiHanModule
/// </summary>
public interface IXiHanModule
{
    Task ConfigureServicesAsync(ServiceConfigurationContext context);

    void ConfigureServices(ServiceConfigurationContext context);
}