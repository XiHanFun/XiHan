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
using XiHan.Infrastructure.Contexts.Validations;
using XiHan.Infrastructure.Contexts.Results;
using XiHan.Infrastructure.Enums;
using XiHan.Utils.Enums;

namespace XiHan.Infrastructure.Contexts;

/// <summary>
/// 通用响应实体
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
            Code = ResponseEnum.Continue,
            Message = ResponseEnum.Continue.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.Ok,
            Message = ResponseEnum.Ok.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.Ok,
            Message = ResponseEnum.Ok.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.Ok,
            Message = ResponseEnum.Ok.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.BadRequest,
            Message = ResponseEnum.BadRequest.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.BadRequest,
            Message = ResponseEnum.BadRequest.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.Unauthorized,
            Message = ResponseEnum.Unauthorized.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.Unauthorized,
            Message = ResponseEnum.Unauthorized.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.Forbidden,
            Message = ResponseEnum.Forbidden.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.NotFound,
            Message = ResponseEnum.NotFound.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.NotFound,
            Message = ResponseEnum.NotFound.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.UnprocessableEntity,
            Message = ResponseEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.UnprocessableEntity,
            Message = ResponseEnum.UnprocessableEntity.GetEnumDescriptionByKey(),
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
            Code = ResponseEnum.InternalServerError,
            Message = ResponseEnum.InternalServerError.GetEnumDescriptionByKey(),
            Datas = null
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
            Code = ResponseEnum.NotImplemented,
            Message = ResponseEnum.NotImplemented.GetEnumDescriptionByKey(),
            Datas = null
        };
    }
}