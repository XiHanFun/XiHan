#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2024 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:InitializationException
// Guid:53699c03-8c26-45aa-9143-b5740d290af0
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2024-04-24 下午 03:44:32
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Core.Exceptions;

/// <summary>
/// 初始化异常
/// </summary>
public class InitializationException : Exception
{
    private const string DefaultMessage = "服务器端程序初始化异常！";

    /// <summary>
    /// 构造函数
    /// </summary>
    public InitializationException() : base(DefaultMessage)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    public InitializationException(string? message) : base(DefaultMessage + message)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public InitializationException(string? message, Exception? innerException) : base(DefaultMessage + message, innerException)
    {
    }