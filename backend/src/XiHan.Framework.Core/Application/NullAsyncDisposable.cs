#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:NullAsyncDisposable
// Guid:bdca5661-4b04-4f62-aa5f-bddb486c0072
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/27 0:27:55
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Verification;

/// <summary>
/// 空可异步释放对象
/// </summary>
public sealed class NullAsyncDisposable : IAsyncDisposable
{
    /// <summary>
    /// 实例
    /// </summary>
    public static NullAsyncDisposable Instance { get; } = new NullAsyncDisposable();

    /// <summary>
    /// 构造函数
    /// </summary>
    private NullAsyncDisposable()
    {
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <returns></returns>
    public ValueTask DisposeAsync()
    {
        return default;
    }
}