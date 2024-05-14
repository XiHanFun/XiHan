#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:BusinessException
// Guid:46555702-493f-49fc-a0e7-374a4084f651
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/23 0:50:17
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;
using XiHan.Core.Exceptions.Abstracts;
using XiHan.Core.Exceptions.Handling.Abstracts;

namespace XiHan.Core.Exceptions;

/// <summary>
/// 业务异常
/// </summary>
public class BusinessException : Exception, IBusinessException, IHasErrorCode, IHasErrorDetails, IHasLogLevel
{
    /// <summary>
    /// 异常代码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 异常详情
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// 日志级别
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="details"></param>
    /// <param name="innerException"></param>
    /// <param name="logLevel"></param>
    public BusinessException(string? code = null, string? message = null, string? details = null,
        Exception? innerException = null, LogLevel logLevel = LogLevel.Warning)
        : base(message, innerException)
    {
        Code = code;
        Details = details;
        LogLevel = logLevel;
    }

    /// <summary>
    /// 写入数据
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public BusinessException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }
}