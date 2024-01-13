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

namespace XiHan.WebCore.Common.Actions;

/// <summary>
/// 验证出错字段实体
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
/// <param name="property"></param>
/// <param name="message"></param>
public class ValidationErrorDto(string? property, string? message)
{

    /// <summary>
    /// 属性
    /// </summary>
    public string? Property { get; } = property;

    /// <summary>
    /// 信息
    /// </summary>
    public string? Message { get; } = message;
}