#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseValidationErrorDto
// Guid:ac177ec3-7b1b-4ce2-9c27-b528784fb176
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-09-01 下午 11:42:36
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace ZhaiFanhuaBlog.ViewModels.Bases.Validation;

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