#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ValidationErrorDto
// Guid:8155c039-09fb-4dd9-83e1-708f3fde0e3b
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-07-18 下午 04:20:05
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Actions;

/// <summary>
/// 验证出错字段实体
/// </summary>
public class ValidationErrorDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="property"></param>
    /// <param name="message"></param>
    public ValidationErrorDto(string? property, string? message)
    {
        Property = property;
        Message = message;
    }

    /// <summary>
    /// 属性
    /// </summary>
    public string? Property { get; }

    /// <summary>
    /// 信息
    /// </summary>
    public string? Message { get; }
}