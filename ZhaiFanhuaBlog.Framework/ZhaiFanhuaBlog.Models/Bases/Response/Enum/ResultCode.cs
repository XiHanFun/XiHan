// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:ResultCode
// Guid:ddb456f8-1a70-45ba-b968-397a0691dae4
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 11:41:35
// ----------------------------------------------------------------

using System.ComponentModel;

namespace ZhaiFanhuaBlog.Models.Response.Enum;

/// <summary>
/// 结果状态码
/// </summary>
public enum ResultCode
{
    /// <summary>
    /// 继续请求
    /// </summary>
    [Description("继续请求")]
    Continue = 100,

    /// <summary>
    /// 切换协议
    /// </summary>
    [Description("切换协议")]
    SwitchingProtocols = 101,

    /// <summary>
    /// 请求成功
    /// </summary>
    [Description("请求成功")]
    OK = 200,

    /// <summary>
    /// 请求已被接受，等待资源响应
    /// </summary>
    [Description("请求已被接受，等待资源响应")]
    Created = 201,

    /// <summary>
    /// 返回多条重定向供选择
    /// </summary>
    [Description("返回多条重定向供选择")]
    MultipleChoices = 300,

    /// <summary>
    /// 永久重定向
    /// </summary>
    [Description("永久重定向")]
    MovedPermanently = 301,

    /// <summary>
    /// 请求错误
    /// </summary>
    [Description("请求错误")]
    BadRequest = 400,

    /// <summary>
    /// 需要身份认证验证
    /// </summary>
    [Description("需要身份认证验证")]
    Unauthorized = 401,

    /// <summary>
    /// 禁止访问
    /// </summary>
    [Description("禁止访问")]
    Forbidden = 403,

    /// <summary>
    /// 请求的内容未找到或已删除
    /// </summary>
    [Description("请求的内容未找到或已删除")]
    NotFound = 404,

    /// <summary>
    /// 请求超时
    /// </summary>
    [Description("请求超时")]
    RequestTimeOut = 408,

    /// <summary>
    /// 请求的语义错误
    /// </summary>
    [Description("请求的语义错误")]
    UnprocessableEntity = 422,

    /// <summary>
    /// 服务器端程序错误
    /// </summary>
    [Description("服务器端程序错误")]
    InternalServerError = 500,

    /// <summary>
    /// 服务器不支持的请求方法
    /// </summary>
    [Description("服务器不支持的请求方法")]
    NotImplemented = 501,
}