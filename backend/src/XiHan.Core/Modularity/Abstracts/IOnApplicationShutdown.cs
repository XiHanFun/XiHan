#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IOnApplicationShutdown
// Guid:10ddc08c-aa9b-4679-89a1-1eef1870cb15
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:38:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Core.Modularity.Contexts;

namespace XiHan.Core.Modularity.Abstracts;

/// <summary>
/// 程序关闭时接口
/// </summary>
public interface IOnApplicationShutdown
{
    /// <summary>
    /// 程序关闭时，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task OnApplicationShutdownAsync([NotNull] ApplicationShutdownContext context);

    /// <summary>
    /// 程序关闭时
    /// </summary>
    /// <param name="context"></param>
    void OnApplicationShutdown([NotNull] ApplicationShutdownContext context);
}