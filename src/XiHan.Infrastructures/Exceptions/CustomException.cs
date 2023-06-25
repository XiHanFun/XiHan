#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:CustomException
// Guid:661fd4f6-f39d-4e1c-8132-43b39be4a6ce
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-06-25 上午 10:16:41
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using XiHan.Utils.Extensions;

namespace XiHan.Infrastructures.Exceptions;

/// <summary>
/// 自定义异常
/// </summary>
public class CustomException : Exception
{
    /// <summary>
    /// 提示信息
    /// </summary>
    public new string? Message { get; set; }

    /// <summary>
    /// 异常详情
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    public CustomException(string message) : base(message)
    {
        message.WriteLineError();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="exception"></param>
    public CustomException(string message, Exception exception) : base(message, exception)
    {
        message.WriteLineError();
    }
}