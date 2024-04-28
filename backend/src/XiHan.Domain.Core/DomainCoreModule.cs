#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:DomainCoreModule
// Guid:a982f4a9-d098-431b-a9a7-83e324ac822d
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/3/28 2:19:56
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Modularity;

namespace XiHan.Domain.Core;

/// <summary>
/// 领域核心模块
/// </summary>
public partial class DomainCoreModule : XiHanModule
{
    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="context"></param>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}