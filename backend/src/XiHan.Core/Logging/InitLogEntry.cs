#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InitLogEntry
// Guid:b9fd048c-b37d-480a-bfcf-d897f3d3fa6a
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 04:06:34
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;

namespace XiHan.Core.Logging;

/// <summary>
/// 初始化日志入口
/// </summary>
public class InitLogEntry
{
    /// <summary>
    /// 日志等级
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// 事件标识符
    /// </summary>
    public EventId EventId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public object State { get; set; } = default!;

    /// <summary>
    /// 异常
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// 格式化器
    /// </summary>
    public Func<object, Exception?, string> Formatter { get; set; } = default!;

    /// <summary>
    /// 异常消息
    /// </summary>
    public string Message => Formatter(State, Exception);
}