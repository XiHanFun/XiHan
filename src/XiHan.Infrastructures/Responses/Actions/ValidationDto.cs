#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ValidationDto
// Guid:0abbb204-7b98-466b-987c-f10ff997b123
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-02 上午 12:28:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XiHan.Infrastructures.Responses.Actions;

/// <summary>
/// 验证实体
/// </summary>
public class ValidationDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ValidationDto(ModelStateDictionary modelState)
    {
        TotalCount = modelState.Count;
        ValidationErrorDtos = modelState.Keys
            .SelectMany(key => modelState[key]!.Errors
                .Select(x => new ValidationErrorDto(key, x.ErrorMessage)))
            .ToList();
    }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// 验证出错字段
    /// </summary>
    public List<ValidationErrorDto>? ValidationErrorDtos { get; }
}

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