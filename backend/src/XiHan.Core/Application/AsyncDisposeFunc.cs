#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:AsyncDisposeFunc
// Guid:1f22699a-f25b-48ea-8e56-db441c8abff7
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/26 23:51:49
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using JetBrains.Annotations;
using XiHan.Utils.Verification;

namespace XiHan.Core.Application;

/// <summary>
/// 异步释放函数
/// </summary>
public class AsyncDisposeFunc : IAsyncDisposable
{
    private readonly Func<Task> _func;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="func">此对象在执行 DisposeAsync 时要执行的函数</param>
    public AsyncDisposeFunc([NotNull] Func<Task> func)
    {
        CheckHelper.NotNull(func, nameof(func));

        _func = func;
    }

    /// <summary>
    /// 释放
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await _func();
    }
}