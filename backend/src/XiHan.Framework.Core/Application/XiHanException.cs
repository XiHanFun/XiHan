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

namespace XiHan.Framework.Core.Application;

/// <summary>
/// 曦寒自定义异常，为特定异常抛出的基本异常类型
/// </summary>
public class XiHanException : Exception
{
    private const string DefaultMessage = "曦寒自定义异常！";

    /// <summary>
    /// 构造函数
    /// </summary>
    public XiHanException() : base(DefaultMessage)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    public XiHanException(string? message) : base(DefaultMessage + message)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public XiHanException(string? message, Exception? innerException) : base(DefaultMessage + message, innerException)
    {
    }
}