#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IOnServiceExposingContext
// Guid:8d8a5f02-5c47-4216-9057-3ef3da2c9d9a
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 21:51:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Core.Microsoft.Extensions.DependencyInjection;

namespace XiHan.Core.DependencyInjection.Contexts;

/// <summary>
/// 服务暴露时上下文接口
/// </summary>
public interface IOnServiceExposingContext
{
    /// <summary>
    /// 服务实现类型
    /// </summary>
    Type ImplementationType { get; }

    /// <summary>
    /// 暴露的服务类型
    /// </summary>
    List<ServiceIdentifier> ExposedTypes { get; }
}