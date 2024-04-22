﻿#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:LoggerExtensions
// Guid:2e55d660-6e76-4ef1-a2b3-c670c2ee8e2c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/23 1:14:54
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;
using System.Text;
using XiHan.Core.Exceptions.Extensions;
using XiHan.Core.Exceptions.Handling.Abstracts;
using XiHan.Core.System.Collections.Generic.Extensions;

namespace XiHan.Core.Microsoft.Extensions.Logging;

/// <summary>
/// 日志扩展
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logLevel"></param>
    /// <param name="message"></param>
    public static void LogWithLevel(this ILogger logger, LogLevel logLevel, string message)
    {
        switch (logLevel)
        {
            case LogLevel.Critical:
                logger.LogCritical("{message}", message);
                break;

            case LogLevel.Error:
                logger.LogError("{message}", message);
                break;

            case LogLevel.Warning:
                logger.LogWarning("{message}", message);
                break;

            case LogLevel.Information:
                logger.LogInformation("{message}", message);
                break;

            case LogLevel.Trace:
                logger.LogTrace("{message}", message);
                break;

            // LogLevel.Debug || LogLevel.None
            default:
                logger.LogDebug("{message}", message);
                break;
        }
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="logLevel"></param>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public static void LogWithLevel(this ILogger logger, LogLevel logLevel, string message, Exception exception)
    {
        switch (logLevel)
        {
            case LogLevel.Critical:
                logger.LogCritical("{exception}{message}", exception, message);
                break;

            case LogLevel.Error:
                logger.LogError("{exception}{message}", exception, message);
                break;

            case LogLevel.Warning:
                logger.LogWarning("{exception}{message}", exception, message);
                break;

            case LogLevel.Information:
                logger.LogInformation("{exception}{message}", exception, message);
                break;

            case LogLevel.Trace:
                logger.LogTrace("{exception}{message}", exception, message);
                break;

            // LogLevel.Debug || LogLevel.None
            default:
                logger.LogDebug("{exception}{message}", exception, message);
                break;
        }
    }

    /// <summary>
    /// 记录异常
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="ex"></param>
    /// <param name="level"></param>
    public static void LogException(this ILogger logger, Exception ex, LogLevel? level = null)
    {
        var selectedLevel = level ?? ex.GetLogLevel();

        logger.LogWithLevel(selectedLevel, ex.Message, ex);
        LogKnownProperties(logger, ex, selectedLevel);
        LogSelfLogging(logger, ex);
        LogData(logger, ex, selectedLevel);
    }

    /// <summary>
    /// 记录已知异常属性
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="exception"></param>
    /// <param name="logLevel"></param>
    private static void LogKnownProperties(ILogger logger, Exception exception, LogLevel logLevel)
    {
        if (exception is IHasErrorCode exceptionWithErrorCode)
        {
            logger.LogWithLevel(logLevel, "异常代码:" + exceptionWithErrorCode.Code);
        }

        if (exception is IHasErrorDetails exceptionWithErrorDetails)
        {
            logger.LogWithLevel(logLevel, "异常详情:" + exceptionWithErrorDetails.Details);
        }
    }

    /// <summary>
    /// 记录异常数据
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="exception"></param>
    /// <param name="logLevel"></param>
    private static void LogData(ILogger logger, Exception exception, LogLevel logLevel)
    {
        if (exception.Data.Count <= 0)
        {
            return;
        }

        var exceptionData = new StringBuilder();
        exceptionData.AppendLine("---------- 异常数据 ----------");
        foreach (var key in exception.Data.Keys)
        {
            exceptionData.AppendLine($"{key} = {exception.Data[key]}");
        }

        logger.LogWithLevel(logLevel, exceptionData.ToString());
    }

    /// <summary>
    /// 记录自身日志
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="exception"></param>
    private static void LogSelfLogging(ILogger logger, Exception exception)
    {
        var loggingExceptions = new List<IExceptionWithSelfLogging>();

        if (exception is IExceptionWithSelfLogging logging)
        {
            loggingExceptions.Add(logging);
        }
        else if (exception is AggregateException aggException && aggException.InnerException != null)
        {
            if (aggException.InnerException is IExceptionWithSelfLogging selfLogging)
            {
                loggingExceptions.Add(selfLogging);
            }

            foreach (var innerException in aggException.InnerExceptions)
            {
                if (innerException is IExceptionWithSelfLogging withSelfLogging)
                {
                    loggingExceptions.AddIfNotContains(withSelfLogging);
                }
            }
        }

        foreach (var ex in loggingExceptions)
        {
            ex.Log(logger);
        }
    }
}