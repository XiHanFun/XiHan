#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:XiHanCoreStringLocalizerFactoryExtensions
// Guid:2a4ae727-ec65-4a6f-b273-a9ce42ca7508
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/5/7 1:04:11
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Localization;

namespace XiHan.Framework.Core.Microsoft.Extensions.Localization;

/// <summary>
/// 本地化器工厂扩展方法
/// </summary>
public static class XiHanCoreStringLocalizerFactoryExtensions
{
    /// <summary>
    /// 创建指定类型的本地化器
    /// </summary>
    /// <typeparam name="TResource"></typeparam>
    /// <param name="localizerFactory"></param>
    /// <returns></returns>
    public static IStringLocalizer Create<TResource>(this IStringLocalizerFactory localizerFactory)
    {
        return localizerFactory.Create(typeof(TResource));
    }
}