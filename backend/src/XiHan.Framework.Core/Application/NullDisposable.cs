#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:NullDisposable
// Guid:7c40618c-8ded-4dd2-91ac-59912c2702aa
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/24 23:11:03
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Framework.Core.Verification;

/// <summary>
/// 空可释放对象
/// </summary>
public sealed class NullDisposable : IDisposable
{
    /// <summary>
    /// 实例
    /// </summary>
    public static NullDisposable Instance { get; } = new NullDisposable();

    /// <summary>
    /// 构造函数
    /// </summary>
    private NullDisposable()
    {
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
    }
}