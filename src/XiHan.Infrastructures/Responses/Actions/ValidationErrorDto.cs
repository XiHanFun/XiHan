#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ValidationErrorDto
// Guid:6045ae8c-f410-4019-b2fe-f54ed975557c
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-06-15 上午 03:26:24
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