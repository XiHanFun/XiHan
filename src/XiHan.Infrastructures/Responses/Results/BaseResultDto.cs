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

using Microsoft.AspNetCore.Mvc.Filters;
using XiHan.Infrastructures.Enums;
using XiHan.Infrastructures.Responses.Validations;
using XiHan.Utils.Extensions;

namespace XiHan.Infrastructures.Responses.Results;

/// <summary>
/// 通用结果实体
/// </summary>
public class BaseResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess { get; set; } = true;

    /// <summary>
    /// 响应码
    /// </summary>
    public HttpResponseCodeEnum Code { get; set; } = HttpResponseCodeEnum.Success;

    /// <summary>
    /// 响应信息
    /// </summary>
    public string? Message { get; set; } = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey();

    /// <summary>
    /// 数据集合
    /// </summary>
    public dynamic? Datas { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    /// <summary>
    /// 继续响应 100
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Continue()
    {
        return new BaseResultDto
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Continue,
            Message = HttpResponseCodeEnum.Continue.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Success()
    {
        return new BaseResultDto
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Success,
            Message = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static BaseResultDto Success(string messageData)
    {
        return new BaseResultDto
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Success,
            Message = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    public static BaseResultDto Success(dynamic datas)
    {
        return new BaseResultDto
        {
            IsSuccess = true,
            Code = HttpResponseCodeEnum.Success,
            Message = HttpResponseCodeEnum.Success.GetEnumDescriptionByKey(),
            Datas = datas
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static BaseResultDto BadRequest(string messageData)
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.BadRequest,
            Message = HttpResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应失败，访问出错 400
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    public static BaseResultDto BadRequest(dynamic datas)
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.BadRequest,
            Message = HttpResponseCodeEnum.BadRequest.GetEnumDescriptionByKey(),
            Datas = datas
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Unauthorized()
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.Unauthorized,
            Message = HttpResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，访问未授权 401
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static BaseResultDto Unauthorized(string messageData)
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.Unauthorized,
            Message = HttpResponseCodeEnum.Unauthorized.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应失败，内容禁止访问 403
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Forbidden()
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.Forbidden,
            Message = HttpResponseCodeEnum.Forbidden.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto NotFound()
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.NotFound,
            Message = HttpResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，数据未找到 404
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static BaseResultDto NotFound(string messageData)
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.NotFound,
            Message = HttpResponseCodeEnum.NotFound.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    ///  响应失败，参数不合法 422
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static BaseResultDto UnprocessableEntity(string messageData)
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.UnprocessableEntity,
            Message = HttpResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应失败，参数不合法 422
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static BaseResultDto UnprocessableEntity(ActionExecutingContext context)
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.UnprocessableEntity,
            Message = HttpResponseCodeEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = new BaseValidationDto(context)
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto InternalServerError()
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.InternalServerError,
            Message = HttpResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应出错，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto InternalServerError(string messageData)
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.InternalServerError,
            Message = HttpResponseCodeEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应出错，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto NotImplemented()
    {
        return new BaseResultDto
        {
            IsSuccess = false,
            Code = HttpResponseCodeEnum.NotImplemented,
            Message = HttpResponseCodeEnum.NotImplemented.GetEnumDescriptionByKey(),
            Datas = null
        };
    }
}