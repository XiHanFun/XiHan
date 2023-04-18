#region <<版权版本注释>>

// ----------------------------------------------------------------
// Copyright ©2022 ZhaiFanhua All Rights Reserved.
// FileName:BaseResponseDto
// Guid:3874a6ae-1ebc-49ab-b4a3-55e76dc68945
// Author:zhaifanhua
// Email:me@zhaifanhua.com
// CreateTime:2022-05-08 下午 11:46:50
// ----------------------------------------------------------------

#endregion <<版权版本注释>>

using Microsoft.AspNetCore.Mvc.Filters;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Infrastructure.Contexts.Validations;
using XiHan.Infrastructure.Enums.Https;
using XiHan.Utils.Enums;

namespace XiHan.Infrastructure.Contexts;

/// <summary>
/// 通用响应实体基类
/// </summary>
public static class BaseResponseDto
{
    /// <summary>
    /// 继续响应 100
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Continue()
    {
        return new BaseResultDto
        {
            Success = true,
            Code = HttpResponseEnum.Continue,
            Message = HttpResponseEnum.Continue.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto Ok()
    {
        return new BaseResultDto
        {
            Success = true,
            Code = HttpResponseEnum.Ok,
            Message = HttpResponseEnum.Ok.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应成功 200
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public static BaseResultDto Ok(string messageData)
    {
        return new BaseResultDto
        {
            Success = true,
            Code = HttpResponseEnum.Ok,
            Message = HttpResponseEnum.Ok.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应成功，返回通用数据 200
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    public static BaseResultDto Ok(dynamic datas)
    {
        return new BaseResultDto
        {
            Success = true,
            Code = HttpResponseEnum.Ok,
            Message = HttpResponseEnum.Ok.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.BadRequest,
            Message = HttpResponseEnum.BadRequest.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.BadRequest,
            Message = HttpResponseEnum.BadRequest.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.Unauthorized,
            Message = HttpResponseEnum.Unauthorized.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.Unauthorized,
            Message = HttpResponseEnum.Unauthorized.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.Forbidden,
            Message = HttpResponseEnum.Forbidden.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.NotFound,
            Message = HttpResponseEnum.NotFound.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.NotFound,
            Message = HttpResponseEnum.NotFound.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.UnprocessableEntity,
            Message = HttpResponseEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
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
            Success = false,
            Code = HttpResponseEnum.UnprocessableEntity,
            Message = HttpResponseEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
            Datas = new BaseValidationDto(context)
        };
    }

    /// <summary>
    /// 响应失败，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto InternalServerError()
    {
        return new BaseResultDto
        {
            Success = false,
            Code = HttpResponseEnum.InternalServerError,
            Message = HttpResponseEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = null
        };
    }

    /// <summary>
    /// 响应失败，服务器内部错误 500
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto InternalServerError(string messageData)
    {
        return new BaseResultDto
        {
            Success = false,
            Code = HttpResponseEnum.InternalServerError,
            Message = HttpResponseEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = messageData
        };
    }

    /// <summary>
    /// 响应失败，功能未实施 501
    /// </summary>
    /// <returns></returns>
    public static BaseResultDto NotImplemented()
    {
        return new BaseResultDto
        {
            Success = false,
            Code = HttpResponseEnum.NotImplemented,
            Message = HttpResponseEnum.NotImplemented.GetEnumDescriptionByKey(),
            Datas = null
        };
    }
}