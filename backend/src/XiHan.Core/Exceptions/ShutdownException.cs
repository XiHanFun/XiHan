#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ShutdownException
// Guid:6ce8f6dd-cfa6-4723-bc6e-4f475530a751
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 03:52:16
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Exceptions;

/// <summary>
/// 关闭过程异常
/// </summary>
public class ShutdownException : Exception
{
    private const string DefaultMessage = "服务器端程序关闭过程异常！";

    /// <summary>
    /// 构造函数
    /// </summary>
    public ShutdownException() : base(DefaultMessage)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    public ShutdownException(string? message) : base(DefaultMessage + message)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ShutdownException(string? message, Exception? innerException) : base(DefaultMessage + message, innerException)
    {
    }
}