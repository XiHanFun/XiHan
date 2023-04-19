#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseResultDto
// Guid:4abbac7e-e91a-4ad2-a048-9f4c16a43464
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-02-20 下午 08:35:52
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using System.ComponentModel;
using XiHan.Utils.Enums;

namespace XiHan.Infrastructure.Contexts.Results;

/// <summary>
/// 通用结果实体
/// </summary>
public class BaseResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// 状态码
    /// </summary>
    public HttpStatusEnum StatusCode { get; set; } = HttpStatusEnum.Success;

    /// <summary>
    /// 状态信息
    /// </summary>
    public string StatusMessage { get; set; } = HttpStatusEnum.Success.GetEnumDescriptionByKey();

    /// <summary>
    /// 响应码
    /// </summary>
    public HttpResponseEnum ResponseCode { get; set; } = HttpResponseEnum.Ok;

    /// <summary>
    /// 响应信息
    /// </summary>
    public string? ResponseMessage { get; set; } = HttpResponseEnum.Ok.GetEnumDescriptionByKey();

    /// <summary>
    /// 数据集合
    /// </summary>
    public dynamic? Datas { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}

/// <summary>
/// 通用状态标识
/// </summary>
public enum HttpStatusEnum
{
    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Success = 1,

    /// <summary>
    /// 确认继续
    /// </summary>
    [Description("确认继续")]
    ConfirmIsContinue = 2,

    /// <summary>
    /// 错误
    /// </summary>
    [Description("错误")]
    Error = 3,

    /// <summary>
    /// 失败
    /// </summary>
    [Description("失败")]
    Fail = 4
}

/// <summary>
/// 通用响应标识
/// </summary>
public enum HttpResponseEnum
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
    Ok = 200,

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