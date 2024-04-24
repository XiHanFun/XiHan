#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:IInitLogger
// Guid:0d052e80-bbe5-46f8-b96c-7d85db07ffe6
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 04:05:51
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;

namespace XiHan.Core.Logging;

/// <summary>
/// 初始化日志接口
/// </summary>
public interface IInitLogger<out T> : ILogger<T>
{
    /// <summary>
    /// 日志入口
    /// </summary>
    public List<InitLogEntry> Entries { get; }
}