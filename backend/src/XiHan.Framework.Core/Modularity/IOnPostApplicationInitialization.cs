#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IOnPostApplicationInitialization
// Guid:8da548f0-321c-4938-b111-f01c2f7c343d
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:38:33
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Framework.Core.Application;

namespace XiHan.Framework.Core.Modularity;

/// <summary>
/// 程序初始化后接口
/// </summary>
public interface IOnPostApplicationInitialization
{
    /// <summary>
    /// 程序初始化后，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task OnPostApplicationInitializationAsync([NotNull] ApplicationInitializationContext context);

    /// <summary>
    /// 程序初始化后
    /// </summary>
    /// <param name="context"></param>
    void OnPostApplicationInitialization([NotNull] ApplicationInitializationContext context);
}