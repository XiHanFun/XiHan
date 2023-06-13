#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:BaseValidationErrorDto
// Guid:77697732-a6c4-42d5-9c80-5e88dfeb1232
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2023-04-22 上午 01:14:06
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

namespace XiHan.Infrastructures.Responses.Validations;

/// <summary>
/// 验证出错字段实体基类
/// </summary>
public class BaseValidationErrorDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="field"></param>
    /// <param name="label"></param>
    /// <param name="message"></param>
    public BaseValidationErrorDto(string? field, string? label, string? message)
    {
        Field = field;
        Label = label;
        Message = message;
    }

    /// <summary>
    /// 字段
    /// </summary>
    public string? Field { get; }

    /// <summary>
    /// 标签(注释说明)
    /// </summary>
    public string? Label { get; }

    /// <summary>
    /// 信息
    /// </summary>
    public string? Message { get; }
}