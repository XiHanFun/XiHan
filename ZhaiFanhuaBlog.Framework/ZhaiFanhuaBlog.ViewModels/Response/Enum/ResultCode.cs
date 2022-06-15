// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultCode
// Guid:ddb456f8-1a70-45ba-b968-397a0691dae4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 11:41:35
// ----------------------------------------------------------------

using System.ComponentModel;

namespace ZhaiFanhuaBlog.ViewModels.Response.Enum;

/// <summary>
/// 结果状态码
/// </summary>
public enum ResultCode
{
    [Description("继续操作")]
    Continue = 100,

    [Description("访问成功")]
    OK = 200,

    [Description("访问出错")]
    BadRequest = 400,

    [Description("访问未授权")]
    Unauthorized = 401,

    [Description("内容禁止访问")]
    Forbidden = 403,

    [Description("数据未找到")]
    NotFound = 404,

    [Description("服务器内部错误")]
    InternalServerError = 500,

    [Description("功能未实施")]
    NotImplemented = 501,
}