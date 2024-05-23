#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:OnServiceExposingContext
// Guid:240c42f2-fef9-4bae-b5d6-e3ac16c298cd
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 22:29:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Utils.Verification;

namespace XiHan.Core.DependencyInjection;

/// <summary>
/// 服务暴露时上下文
/// </summary>
public class OnServiceExposingContext : IOnServiceExposingContext
{
    /// <summary>
    /// 实现类型
    /// </summary>
    public Type ImplementationType { get; }

    /// <summary>
    /// 暴露的类型
    /// </summary>
    public List<ServiceIdentifier> ExposedTypes { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="implementationType"></param>
    /// <param name="exposedTypes"></param>
    public OnServiceExposingContext([NotNull] Type implementationType, List<Type> exposedTypes)
    {
        ImplementationType = CheckHelper.NotNull(implementationType, nameof(implementationType));
        ExposedTypes = CheckHelper.NotNull(exposedTypes, nameof(exposedTypes)).ConvertAll(t => new ServiceIdentifier(t));
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="implementationType"></param>
    /// <param name="exposedTypes"></param>
    public OnServiceExposingContext([NotNull] Type implementationType, List<ServiceIdentifier> exposedTypes)
    {
        ImplementationType = CheckHelper.NotNull(implementationType, nameof(implementationType));
        ExposedTypes = CheckHelper.NotNull(exposedTypes, nameof(exposedTypes));
    }
}