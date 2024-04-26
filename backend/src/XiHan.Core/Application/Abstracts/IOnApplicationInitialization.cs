#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IOnApplicationInitialization
// Guid:7dd7a9b3-1fae-46e8-9b99-81892242441e
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-22 上午 10:38:25
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;

namespace XiHan.Core.Application.Abstracts;

/// <summary>
/// 程序初始化接口
/// </summary>
public interface IOnApplicationInitialization
{
    /// <summary>
    /// 程序初始化，异步
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task OnApplicationInitializationAsync([NotNull] ApplicationInitializationContext context);

    /// <summary>
    /// 程序初始化
    /// </summary>
    /// <param name="context"></param>
    void OnApplicationInitialization([NotNull] ApplicationInitializationContext context);
}