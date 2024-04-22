#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IOnPreApplicationInitialization
// Guid:2448816c-a7bc-4b84-a4ba-c0d8da3a2b11
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:38:15
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Core.Modularity.Contexts;

namespace XiHan.Core.Modularity.Abstracts;

/// <summary>
/// 程序初始化前接口
/// </summary>
public interface IOnPreApplicationInitialization
{
    /// <summary>
    /// 程序初始化前，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task OnPreApplicationInitializationAsync([NotNull] ApplicationInitializationContext context);

    /// <summary>
    /// 程序初始化前
    /// </summary>
    /// <param name="context"></param>
    void OnPreApplicationInitialization([NotNull] ApplicationInitializationContext context);
}