#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2023 ZhaiFanhua All Rights Reserved.
// FileName:ResponseCodeEnum
// Guid:1c4a433e-7804-4d50-8947-a001d4b9ee2f
// Author:Administrator
// Email:me@zhaifanhua.com
// CreateTime:2023-04-20 下午 03:20:43
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;

namespace XiHan.Infrastructures.Responses.Results;

/// <summary>
/// 通用响应标识
/// </summary>
public enum ResponseCodeEnum
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
    Success = 200,

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
    /// 需要身份认证验证，或认证参数有误，或认证已过期
    /// </summary>
    [Description("需要身份认证验证，或认证参数有误，或认证已过期")]
    Unauthorized = 401,

    /// <summary>
    /// 访问权限等级不够，或禁止访问
    /// </summary>
    [Description("访问权限等级不够，或禁止访问")]
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
    /// 并发请求过多
    /// </summary>
    [Description("并发请求过多")]
    TooManyRequests = 429,

    /// <summary>
    /// 服务器端程序错误
    /// </summary>
    [Description("服务器端程序错误")]
    InternalServerError = 500,

    /// <summary>
    /// 服务器不支持的请求，或未实现接口，或异常接口
    /// </summary>
    [Description("服务器不支持的请求，或未实现接口，或异常接口")]
    NotImplemented = 501,
}