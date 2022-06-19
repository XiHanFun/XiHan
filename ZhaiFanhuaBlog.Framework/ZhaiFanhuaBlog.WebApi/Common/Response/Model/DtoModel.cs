// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:DtoModel
// Guid:f5a8be81-8e6c-4c3d-92a4-1fedd51f1ecc
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-06-17 上午 03:17:37
// ----------------------------------------------------------------

using ZhaiFanhuaBlog.WebApi.Common.Response.Enum;

namespace ZhaiFanhuaBlog.WebApi.Common.Response.Model;

/// <summary>
/// 验证数据类
/// </summary>
public class DtoModel
{
    /// <summary>
    /// 数据总数
    /// </summary>
    public int DataCount { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<dynamic>? Data { get; set; }

    /// <summary>
    /// 返回状态
    /// </summary>
    public ReturnStatus ReturnStatus { get; set; }
}

/// <summary>
/// 验证字段
/// </summary>
public class ValidationModel
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="field"></param>
    /// <param name="message"></param>
    public ValidationModel(string? field, string? message)
    {
        Field = field;
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