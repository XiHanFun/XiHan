#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// Licensed under the MulanPSL2 License. See LICENSE in the project root for license information.
// FileName:ValidationDto
// Guid:0abbb204-7b98-466b-987c-f10ff997b123
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreatedTime:2022-09-02 上午 12:28:58
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XiHanCore.WebCore.Common.Actions;

/// <summary>
/// 验证实体
/// </summary>
/// <remarks>
/// 构造函数
/// </remarks>
public class ValidationDto(ModelStateDictionary modelState)
{

    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalCount { get; } = modelState.Count;

    /// <summary>
    /// 验证出错字段
    /// </summary>
    public List<ValidationErrorDto>? ValidationErrorDtos { get; } = modelState.Keys
            .SelectMany(key => modelState[key]!.Errors.Select(x => new ValidationErrorDto(key, x.ErrorMessage)))
            .ToList();
}