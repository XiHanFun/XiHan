#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseValidationDto
// Guid:0abbb204-7b98-466b-987c-f10ff997b123
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-02 上午 12:28:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc.Filters;
using XiHan.Utils.Objects;

namespace XiHan.Infrastructure.Contexts.Validations;

/// <summary>
/// 验证实体基类
/// </summary>
public class BaseValidationDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BaseValidationDto(ActionExecutingContext context)
    {
        TotalCount = context.ModelState.Count;
        ValidationErrorDto = context.ModelState.Keys
                .SelectMany(key => context!.ModelState[key]!.Errors
                .Select(x => new BaseValidationErrorDto(key.GetDescription(), x.ErrorMessage)))
                .ToList();
    }

    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// 验证出错字段
    /// </summary>
    public List<BaseValidationErrorDto>? ValidationErrorDto { get; }
}

/// <summary>
/// 验证出错字段实体基类
/// </summary>
public class BaseValidationErrorDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="field"></param>
    /// <param name="message"></param>
    public BaseValidationErrorDto(string? field, string? message)
    {
        Field = field != string.Empty ? field : null;
        Message = message;
    }

    /// <summary>
    /// 字段
    /// </summary>
    public string? Field { get; }

    /// <summary>
    /// 信息
    /// </summary>
    public string? Message { get; }
}