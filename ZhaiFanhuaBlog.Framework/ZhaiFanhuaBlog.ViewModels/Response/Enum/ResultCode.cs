// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultCode
// Guid:ddb456f8-1a70-45ba-b968-397a0691dae4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 11:41:35
// ----------------------------------------------------------------

namespace ZhaiFanhuaBlog.ViewModels.Response.Enum;

/// <summary>
/// 结果状态码
/// </summary>
public enum ResultCode
{
    /// <summary>
    /// 继续操作
    /// </summary>
    Continue = 100,

    /// <summary>
    /// 访问成功
    /// </summary>
    OK = 200,

    /// <summary>
    /// 访问出错
    /// </summary>
    BadRequest = 400,

    /// <summary>
    /// 访问未授权
    /// </summary>
    Unauthorized = 401,

    /// <summary>
    /// 内容禁止访问
    /// </summary>
    Forbidden = 403,

    /// <summary>
    /// 数据未找到
    /// </summary>
    NotFound = 404,

    /// <summary>
    /// 服务器内部错误
    /// </summary>
    InternalServerError = 500,

    /// <summary>
    /// 功能未实施
    /// </summary>
    NotImplemented = 501,
}