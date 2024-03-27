#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:CustomException
// Guid:661fd4f6-f39d-4e1c-8132-43b39be4a6ce
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2023-06-25 上午 10:16:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Common.Utilities.Extensions;

namespace XiHan.Common.Utilities.Exceptions;

/// <summary>
/// 自定义异常
/// </summary>
public class CustomException : Exception
{
    private const string DefaultMessage = "服务器端程序错误!";

    /// <summary>
    /// 构造函数
    /// </summary>
    public CustomException()
    {
        var messageConsole = DefaultMessage;
        ConsoleWriteLine(messageConsole);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    public CustomException(string? message) : base(message)
    {
        var messageConsole = DefaultMessage + message;
        ConsoleWriteLine(messageConsole);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public CustomException(string? message, Exception? innerException) : base(message, innerException)
    {
        var messageConsole = DefaultMessage + message;
        ConsoleWriteLine(messageConsole);
    }

    /// <summary>
    /// 控制台输出错误信息
    /// </summary>
    /// <param name="message"></param>
    public void ConsoleWriteLine(string message)
    {
        message.WriteLineError();
    }
}