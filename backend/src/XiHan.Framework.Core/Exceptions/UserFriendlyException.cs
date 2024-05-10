#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:UserFriendlyException
// Guid:53c25c7c-73a4-4394-ba9e-1de4e84d8616
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2024/4/23 0:46:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.Extensions.Logging;
using XiHan.Framework.Core.Exceptions.Abstracts;

namespace XiHan.Framework.Core.Exceptions;

/// <summary>
/// 用户友好异常
/// </summary>
public class UserFriendlyException : BusinessException, IUserFriendlyException
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="code"></param>
    /// <param name="details"></param>
    /// <param name="innerException"></param>
    /// <param name="logLevel"></param>
    public UserFriendlyException(string message, string? code = null, string? details = null,
        Exception? innerException = null, LogLevel logLevel = LogLevel.Warning)
        : base(code, message, details, innerException, logLevel)
    {
        Details = details;
    }
}